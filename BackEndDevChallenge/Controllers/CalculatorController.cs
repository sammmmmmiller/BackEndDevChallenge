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
            var result = input1 + input2;
            SaveMathProblem(username, input1, input2, result, MathOperationType.Addition);
            return result;
        }

        [HttpGet("Subtract")]
        public ActionResult<int> Subtract(int input1, int input2, string? username = "Legacy")
        {
            var result = input1 - input2;
            SaveMathProblem(username, input1, input2, result, MathOperationType.Subtraction);
            return result;
        }

        [HttpGet("Multiply")]
        public ActionResult<int> Multiply(int input1, int input2, string? username = "Legacy")
        {
            var result = input1 * input2;
            SaveMathProblem(username, input1, input2, result, MathOperationType.Multiplication);
            return result;
        }

        [HttpGet("Divide")]
        public ActionResult<int> Divide(int input1, int input2, string? username = "Legacy")
        {
            if (input2 == 0) {
                return BadRequest("Cannot divide by 0.");
            }
            var result = input1 / input2;
            SaveMathProblem(username, input1, input2, result, MathOperationType.Division);
            return result;
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
    }
}
