
namespace MusiciansBlog.API.Authentication.Providers
{
    public class CookieProvider : ICookieProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public CookieProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <inheritdoc />
        public void AppendTokenToCookie(string key, string tokenValue, DateTime tokenExpiry)
        {
            _accessor.HttpContext.Response.Cookies.Append(key, tokenValue, new CookieOptions
            {
                Expires = tokenExpiry,
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
