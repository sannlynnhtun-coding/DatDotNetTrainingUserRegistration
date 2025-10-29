namespace DatDotNetTrainingUserRegistration.MvcApp2.Services
{
    public static class DevCode
    {
        public static string GetClaim(this IHttpContextAccessor HttpContextAccessor, string type)
        {
            return HttpContextAccessor.HttpContext.User.Claims
                     .FirstOrDefault(x => x.Type == type)?.Value;
        }
    }
}
