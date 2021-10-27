namespace _12thMorning.Services
{
    public class TokenProvider
    {
        public string XsrfToken { get; set; }
    }

    public class InitialApplicationState
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
