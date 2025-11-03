namespace Lesson.Requests.Group
{
    public class UpdateGroupRequest
    {
        public string Name { get; set; }
        public int DirectionId { get; set; }
        public int CourseId { get; set; }
        public int ProjectId { get; set; }
    }
}
