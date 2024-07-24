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
                SaveError(username, MathOperationType.Addition, exception.Message, input1, input2);
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
                SaveError(username, MathOperationType.Subtraction, exception.Message, input1, input2);
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
                SaveError(username, MathOperationType.Multiplication, exception.Message, input1, input2);
                return StatusCode(500, "An internal server error occurred.");      
            }
        }

        [HttpGet("Divide")]
        public ActionResult<int> Divide(int input1, int input2, string? username = "Legacy")
        {
            try {
                if (input2 == 0) {
                    SaveError(username, MathOperationType.Division, "Cannot divide by 0.", input1, input2);
                    return BadRequest("Cannot divide by 0.");
                }
                var result = input1 / input2;
                SaveMathProblem(username, input1, input2, result, MathOperationType.Division);
                return result;
            } catch (Exception exception) {
                SaveError(username, MathOperationType.Division, exception.Message, input1, input2);
                return StatusCode(500, "An internal server error occurred.");
            }
            
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

        private void SaveError(string username, MathOperationType operationType, string errorMessage, int input1, int input2) { 
            ErrorLog errorLog = new ErrorLog {
                Username = username,
                ErrorType = operationType,
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
