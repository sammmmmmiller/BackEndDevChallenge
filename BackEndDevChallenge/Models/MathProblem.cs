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
        Subtraction
    }
}
