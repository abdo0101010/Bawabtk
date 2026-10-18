namespace Bawabtk.REPR.EndPoints.Students.Read
{
    public class StudentResponse
    {
        public Guid StudentUserGuid { get; set; }      // _UserId
        public Guid StudentProfileGuid { get; set; }   // _StudentProfileId
        public Guid ParentProfileGuid { get; set; }    // _ParentProfileId
        public string StudentName { get; set; } = string.Empty;
        public string UnifiedBarcode { get; set; } = string.Empty;
        public string GradeLevel { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
