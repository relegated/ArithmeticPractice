using ArithmeticPractice.Enums;
using System.Collections.Generic;

namespace ArithmeticPractice.Models
{
    public class PracticeSession
    {
        public PracticeSession(int numberOfMinutes, int numberOfQuestions, IEnumerable<EArithmeticOperator> arithmeticOperators)
        {
            NumberOfMinutes = numberOfMinutes;
            NumberOfQuestions = numberOfQuestions;
            ArithmeticOperators = arithmeticOperators;
        }

        public int NumberOfMinutes { get; }
        public int NumberOfQuestions { get; }
        public IEnumerable<EArithmeticOperator> ArithmeticOperators { get; }
    }
}
