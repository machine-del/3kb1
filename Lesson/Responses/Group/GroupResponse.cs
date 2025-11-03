using Lesson.Responses.Course;
using Lesson.Responses.Direction;
using Lesson.Responses.Project;

namespace Lesson.Responses.Group
{
    public class GroupResponse : BaseResponse
    {
        public DirectionResponse Direction { get; set; }
        public CourseResponse Course { get; set; }
        public ProjectResponse Project { get; set; }

    }
}
