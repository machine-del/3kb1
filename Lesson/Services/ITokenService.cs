using Lesson.Responses.Auth;

namespace Lesson.Settings
{
    public interface ITokenService
    {
        string CreateAccessToken(AuthResponse authResponse);
        string CreateRefreshToken();
    }
}
