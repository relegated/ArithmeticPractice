using ArithmeticPractice.Enums;

namespace ArithmeticPractice.Models
{
    public class ArithmeticQuestion
    {
        private const int Undefined = -1;

        public int FirstOperand { get; set; } = Undefined;
        public int SecondOperand { get; set; } = Undefined;

        public int Result { get; set; } = Undefined;

        public int Response { get; set; } = Undefined;

        public EArithmeticOperator Operator { get; set; } = EArithmeticOperator.Undefined;

        public EQuestionScore QuestionScore { get; set; } = EQuestionScore.Undefined;
        
    }
}
