using ArithmeticPractice.Enums;
using ArithmeticPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArithmeticPractice.Generator
{
    public class ArithmeticPracticeQuestionGenerator
    {
        private readonly PracticeSession practiceSession;

        public ArithmeticPracticeQuestionGenerator(PracticeSession practiceSession)
        {
            this.practiceSession = practiceSession;
        }

        public IEnumerable<ArithmeticQuestion> GenerateQuestions()
        {
            var questionList = new List<ArithmeticQuestion>();

            for (var i = 0; i < practiceSession.NumberOfQuestions; i++)
            {
                questionList.Add(CreateNewQuestion());
            }

            return questionList;
        }

        private ArithmeticQuestion CreateNewQuestion()
        {
            var arithmeticOperator = GetRandomOperatorFromSelections();
            var firstOperand = -1;
            var secondOperand = -1;
            var result = -1;
            var random = new Random();

            switch (arithmeticOperator)
            {
                case EArithmeticOperator.Add:
                    firstOperand = random.Next(0, 10);
                    secondOperand = random.Next(0, 10);
                    result = firstOperand + secondOperand;
                    break;
                case EArithmeticOperator.Subtract:
                    firstOperand = random.Next(0, 10);
                    secondOperand = random.Next(0, firstOperand);
                    result = firstOperand - secondOperand;
                    break;
                case EArithmeticOperator.Multiply:
                    firstOperand = random.Next(0, 10);
                    secondOperand = random.Next(0, 10);
                    result = firstOperand * secondOperand;
                    break;
                case EArithmeticOperator.Divide:
                    result = random.Next(1, 10);
                    secondOperand = random.Next(1, result);
                    firstOperand = result * secondOperand;
                    break;
                case EArithmeticOperator.Exponent:
                    break;
                case EArithmeticOperator.Logarthim:
                    break;
                case EArithmeticOperator.Undefined:
                default:
                    break;
            }

            return new ArithmeticQuestion()
            {
                FirstOperand = firstOperand,
                SecondOperand = secondOperand,
                Result = result,
                Operator = arithmeticOperator,
            };
        }

        private EArithmeticOperator GetRandomOperatorFromSelections()
        {
            var listOfOperators = practiceSession.ArithmeticOperators.ToList();
            var random = new Random();
            return listOfOperators[random.Next(listOfOperators.Count())];
        }
    }
}
