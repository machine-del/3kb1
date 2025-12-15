namespace Lesson.Responses.TestResult
{
    public class TestResultResponse : BaseResponse
    {
        public int Id { get; set; }
        public bool Passed { get; set; }
        public int TestId { get; set; }
        public int AttemptId { get; set; }
        public int StudentId { get; set; }
        public int BestScore { get; set; }
    }
}
