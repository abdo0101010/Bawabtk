namespace CleanArchitecture.Shared.Contract.ExamImplemention
{
    public class ExamOperationContract
    {
        public Guid CourseId { get; set; } 
        public string Title { get; set; } = string.Empty;
        public decimal MaxScore { get; set; }
        public decimal PassingScore { get; set; }
        public decimal LowGradeAlertThreshold { get; set; }
        public DateTimeOffset ExamDate { get; set; }
        public bool IsOnline { get; set; }
        public ExamOperationContract(Guid courseid,string title , decimal maxscore,decimal passingscore,decimal lowgradealertthreshold,DateTimeOffset examdate,bool isonline)
        {
            CourseId = courseid;
            Title = title;
            MaxScore = maxscore;
            PassingScore = passingscore;
            LowGradeAlertThreshold = lowgradealertthreshold;
            ExamDate = examdate;
            IsOnline = isonline;
        }
    }
}
