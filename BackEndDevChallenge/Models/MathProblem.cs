namespace BackEndDevChallenge.Models
{
    public class MathProblem
    {
        public string Username {get; set;}
        public int Id { get; set; }
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int Result { get; set; }
        public MathOperationType OperationType { get; set; }

    }
    public enum MathOperationType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
    public class ErrorLog
    {
        public int ID {get; set;}
        public string Username {get; set;}
        public MathOperationType ErrorType {get; set;}
        public string ErrorMessage {get; set;}
        public int Input1 {get; set;}
        public int Input2 {get; set;}
        public DateTime Timestamp {get; set;}
    }
    public class UserReport
    {
        public string Username {get; set;}
        public int NumAPICalls {get; set;}
        public int NumErrors {get; set;}
    }

    public class Report {
        public int ID {get; set;}
        public UserReport[] UserReports {get; set;}
        public int MostCommonAnswer {get; set;}
    }
}
