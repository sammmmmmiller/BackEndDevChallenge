using BackEndDevChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEndDevChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly CalculatorContext _context;

        public CalculatorController(ILogger<CalculatorController> logger, CalculatorContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Add")]
        public ActionResult<int> Add(int input1, int input2, string? username = "Legacy")
        {
            try {
                var result = input1 + input2;
                SaveMathProblem(username, input1, input2, result, MathOperationType.Addition);
                return result;
            } catch (Exception exception) {
                SaveError(username, ErrorType.InternalServerError, exception.Message, input1, input2);
                return StatusCode(500, "An internal server error occurred.");
            }
            
        }

        [HttpGet("Subtract")]
        public ActionResult<int> Subtract(int input1, int input2, string? username = "Legacy")
        {
            try {
                var result = input1 - input2;
                SaveMathProblem(username, input1, input2, result, MathOperationType.Subtraction);
                return result;
            } catch (Exception exception) {
                SaveError(username, ErrorType.InternalServerError, exception.Message, input1, input2);
                return StatusCode(500, "An internal server error occurred.");            
            }

        }

        [HttpGet("Multiply")]
        public ActionResult<int> Multiply(int input1, int input2, string? username = "Legacy")
        {
            try {
                var result = input1 * input2;
                SaveMathProblem(username, input1, input2, result, MathOperationType.Multiplication);
                return result;
            } catch (Exception exception) {
                SaveError(username, ErrorType.InternalServerError, exception.Message, input1, input2);
                return StatusCode(500, "An internal server error occurred.");      
            }
        }

        [HttpGet("Divide")]
        public ActionResult<int> Divide(int input1, int input2, string? username = "Legacy")
        {
            try {
                if (input2 == 0) {
                    SaveError(username, ErrorType.DivisionByZero, "Cannot divide by 0.", input1, input2);
                    return BadRequest("Cannot divide by 0.");
                }
                var result = input1 / input2;
                SaveMathProblem(username, input1, input2, result, MathOperationType.Division);
                return result;
            } catch (Exception exception) {
                SaveError(username, ErrorType.InternalServerError, exception.Message, input1, input2);
                return StatusCode(500, "An internal server error occurred.");
            }
            
        }

        [HttpGet("Report")]
        public ActionResult<Report> Report()
        {
            UserReport[] userReports = _context.MathProblems
                .GroupBy(mp => mp.Username)
                .Select(group => new UserReport
                {
                    Username = group.Key,
                    NumAPICalls = group.Count(),
                    NumErrors = _context.ErrorLogs.Count(e => e.Username == group.Key)
                })
                .ToArray();
            ErrorReport[] errorReports = _context.ErrorLogs
                .GroupBy(el => new { el.ErrorType, el.ErrorMessage })
                .Select(group => new ErrorReport
                {
                    ErrorType = group.Key.ErrorType,
                    ErrorMessage = group.Key.ErrorMessage,
                    Quantity = group.Count()
                })
                .ToArray();
            int mostCommonAnswer = _context.MathProblems
                .GroupBy(mp => mp.Result)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault();

            Report report = new Report
            {
                UserReports = userReports,
                ErrorReports = errorReports,
                MostCommonAnswer = mostCommonAnswer
            };

            return report;
            
        }

        private void SaveMathProblem(string username, int input1, int input2, int result, MathOperationType operationType)
        {
            var mathProblem = new MathProblem
            {
                Username = username,
                Input1 = input1,
                Input2 = input2,
                Result = result,
                OperationType = operationType
            };

            _context.MathProblems.Add(mathProblem);
            _context.SaveChanges();
        }

        private void SaveError(string username, ErrorType errorType, string errorMessage, int input1, int input2) { 
            ErrorLog errorLog = new ErrorLog {
                Username = username,
                ErrorType = errorType,
                ErrorMessage = errorMessage,
                Input1 = input1,
                Input2 = input2,
                Timestamp = DateTime.UtcNow
            };

            _context.ErrorLogs.Add(errorLog);
            _context.SaveChanges();
        }
    }
}
