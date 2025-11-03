namespace Lesson.Requests.Group
{
    public class CreateGroupRequest
    {
        public string Name { get; set; }
        public int DirectionId { get; set; }
        public int CourseId { get; set; }
        public int ProjectId { get; set; }
    }
}
