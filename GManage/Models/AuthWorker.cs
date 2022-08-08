using Microsoft.EntityFrameworkCore;

namespace GManage.Models
{
    public interface IAuthenticationWorker
    {
        public Task<bool> Authenticate(string Login, string Session, int ExpirationMins);
    }

    public class AuthWorker : IAuthenticationWorker
    {
        private readonly IAuthenticationCache _authCache;
        private readonly IServiceScopeFactory _scopeFactory;

        public AuthWorker(IAuthenticationCache authCache, IServiceScopeFactory scopeFactory)
        {
            _authCache = authCache;
            _scopeFactory = scopeFactory;
        }

        public async Task<bool> Authenticate(string Login, string Session, int ExpirationMins)
        {
            if (_authCache.CheckUser(Login, Session))
                return true;
            using (var scope = _scopeFactory.CreateScope())
            {
                var AuthDbContext = scope.ServiceProvider.GetService<AuthDbContext>();
                if (AuthDbContext == null)
                    throw new Exception("Error getting scoped service AuthDbContext");
                var TryGetSession = await AuthDbContext.Sessions
                    .Where(x => x.User.Login == Login && x.SessionId == Session)
                    .FirstOrDefaultAsync();
                var DTNow = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

                if (TryGetSession != null && TryGetSession.LastAccessed.AddMinutes(ExpirationMins) > DTNow)
                {
                    _authCache.AddOrUpdateUser(Login, Session, DTNow);
                    TryGetSession.LastAccessed = DTNow;
                    await AuthDbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

    }
}
