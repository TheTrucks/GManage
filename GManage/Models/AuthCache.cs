namespace GManage.Models
{
    public interface IAuthenticationCache
    {
        bool CheckUser(string Login, string Session);
        void AddOrUpdateUser(string Login, string Session, DateTime ExpirationTime);
    }

    public class AuthCache : IAuthenticationCache
    {
        private Dictionary<string, Tuple<string, DateTime>> UsersCache = new();

        public void AddOrUpdateUser(string Login, string Session, DateTime ExpirationTime)
        {
            UsersCache[Login] = new Tuple<string, DateTime>(Session, ExpirationTime);
        }

        public bool CheckUser(string Login, string Session)
        {
            return UsersCache.ContainsKey(Login) && 
                UsersCache[Login].Item1 == Session && 
                UsersCache[Login].Item2 > DateTime.UtcNow;
        }
    }
}
