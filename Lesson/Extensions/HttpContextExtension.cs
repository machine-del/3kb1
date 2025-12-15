using Lesson.Constants;

namespace Lesson.Extensions
{
    public static class HttpContextExtension
    {
        public static int TryGetUserId(this HttpContext context)
        {
            var studentIdValue = context.User.Claims.FirstOrDefault(x => x.Type == TestingPlatformClaimTypes.StudentId)?.Value;

            if (!int.TryParse(studentIdValue, out var studentId))
                throw new InvalidOperationException("Данные о пользователе пусты");

            return studentId;
        }
    }
}
