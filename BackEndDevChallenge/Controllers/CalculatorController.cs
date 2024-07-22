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
        public ActionResult<int> Add(int input1, int input2)
        {
            var result = input1 + input2;
            SaveMathProblem(input1, input2, result, MathOperationType.Addition);
            return result;
        }

        [HttpGet("Subtract")]
        public ActionResult<int> Subtract(int input1, int input2)
        {
            var result = input1 - input2;
            SaveMathProblem(input1, input2, result, MathOperationType.Subtraction);
            return result;
        }

        private void SaveMathProblem(int input1, int input2, int result, MathOperationType operationType)
        {
            var mathProblem = new MathProblem
            {
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
