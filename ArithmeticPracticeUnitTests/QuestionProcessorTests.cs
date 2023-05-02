using ArithmeticPractice.Enums;
using ArithmeticPractice.Models;
using ArithmeticPractice.QuestionProcessor;
using NUnit.Framework;
using System;

namespace ArithmeticPracticeUnitTests
{
    [TestFixture]
    public class QuestionProcessorTests
    {
        private QuestionProcessor Processor;

        [SetUp] 
        public void SetUp() 
        { 
            Processor = new QuestionProcessor();
        }

        [TestCase(5, 4, EArithmeticOperator.Add, 9, 9, EQuestionScore.Correct)]
        [TestCase(5, 4, EArithmeticOperator.Add, 9, 10, EQuestionScore.Incorrect)]
        [TestCase(5, 4, EArithmeticOperator.Subtract, 1, 1, EQuestionScore.Correct)]
        [TestCase(5, 4, EArithmeticOperator.Subtract, 1, 0, EQuestionScore.Incorrect)]
        [TestCase(5, 4, EArithmeticOperator.Multiply, 20, 20, EQuestionScore.Correct)]
        [TestCase(5, 4, EArithmeticOperator.Multiply, 20, 25, EQuestionScore.Incorrect)]
        [TestCase(8, 4, EArithmeticOperator.Divide, 2, 2, EQuestionScore.Correct)]
        [TestCase(8, 4, EArithmeticOperator.Divide, 2, 1, EQuestionScore.Incorrect)]
        public void ProcessQuestion_SetsScore(int firstOperand, int secondOperand, EArithmeticOperator arithmeticOperator, int actualResult, int inputResult, EQuestionScore expectedScore)
        {
            var question = GivenAQuestion(firstOperand, secondOperand, arithmeticOperator, actualResult);

            Processor.ProcessQuestion(question, inputResult);

            Assert.That(question.QuestionScore, Is.EqualTo(expectedScore));
        }

        private ArithmeticQuestion GivenAQuestion(int firstOperand, int secondOperand, EArithmeticOperator arithmeticOperator, int result)
        {
            return new ArithmeticQuestion()
            {
                FirstOperand = firstOperand,
                SecondOperand = secondOperand,
                Operator = arithmeticOperator,
                Result = result,
            };
        }
    }
}
