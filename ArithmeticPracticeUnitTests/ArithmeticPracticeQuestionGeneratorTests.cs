using ArithmeticPractice.Enums;
using ArithmeticPractice.Generator;
using ArithmeticPractice.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArithmeticPracticeUnitTests
{
    [TestFixture]
    public class ArithmeticPracticeQuestionGeneratorTests
    {
        private ArithmeticPracticeQuestionGenerator practiceQuestionGenerator;

        [Test]
        public void CanCreatePracticeQuestionGenerator()
        {
            var model = GivenAPracticeSessionModel(1, 1, new EArithmeticOperator[] { EArithmeticOperator.Add });
            
            WhenPracticeQuestionGeneratorIsConstructed(model);

            Assert.That(practiceQuestionGenerator, Is.Not.Null);
        }

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(10)]
        [TestCase(50)]
        public void GenerateQuestions_GeneratesTheAppropriateNumberOfQuestions(int numberOfQuestions)
        {
            var model = GivenAPracticeSessionModel(10, numberOfQuestions, new EArithmeticOperator[] { EArithmeticOperator.Add });
            GivenAPracticeQuestionGeneratorIsConstructed(model);

            var questions = WhenQuestionsAreGenerated();

            ThenTheAppropriateNumberofQuestionsAreGenerated(questions, numberOfQuestions);
        }

        [TestCase(new EArithmeticOperator[] { EArithmeticOperator.Add })]
        [TestCase(new EArithmeticOperator[] { EArithmeticOperator.Subtract })]
        [TestCase(new EArithmeticOperator[] { EArithmeticOperator.Multiply, EArithmeticOperator.Divide })]
        [TestCase(new EArithmeticOperator[] { EArithmeticOperator.Subtract, EArithmeticOperator.Multiply, EArithmeticOperator.Divide, EArithmeticOperator.Exponent, EArithmeticOperator.Logarthim })]
        public void GenerateQuestions_GeneratesQuestionsOnlyUsingProvidedOperators(IEnumerable<EArithmeticOperator> operatorSelections)
        {
            var model = GivenAPracticeSessionModel(10, 10, operatorSelections);
            GivenAPracticeQuestionGeneratorIsConstructed(model);

            var questions = WhenQuestionsAreGenerated();

            ThenUnselectedOperatorsDoNotAppearInGeneratedQuestions(questions, operatorSelections);
        }

        [Test]
        public void GenerateQuestions_GeneratesValidAdditionQuestions()
        {
            var model = GivenAPracticeSessionModel(20, 100, new EArithmeticOperator[] { EArithmeticOperator.Add });
            GivenAPracticeQuestionGeneratorIsConstructed(model);

            var questions = WhenQuestionsAreGenerated();

            ThenGeneratedAdditionQuestionsAreValidFacts(questions);
        }

        [Test]
        public void GenerateQuestions_GeneratesValidSubtractionQuestions()
        {
            var model = GivenAPracticeSessionModel(20, 100, new EArithmeticOperator[] { EArithmeticOperator.Subtract });
            GivenAPracticeQuestionGeneratorIsConstructed(model);

            var questions = WhenQuestionsAreGenerated();

            ThenGeneratedSubtractionQuestionsAreValidFacts(questions);
        }

        [Test]
        public void GenerateQuestions_GeneratesValidMultiplicationQuestions()
        {
            var model = GivenAPracticeSessionModel(20, 100, new EArithmeticOperator[] { EArithmeticOperator.Multiply });
            GivenAPracticeQuestionGeneratorIsConstructed(model);

            var questions = WhenQuestionsAreGenerated();

            ThenGeneratedMultiplicationQuestionsAreValidFacts(questions);
        }

        [Test]
        public void GenerateQuestions_GeneratesValidDivisionQuestions()
        {
            var model = GivenAPracticeSessionModel(20, 100, new EArithmeticOperator[] { EArithmeticOperator.Divide });
            GivenAPracticeQuestionGeneratorIsConstructed(model);

            var questions = WhenQuestionsAreGenerated();

            ThenGeneratedDivisionQuestionsAreValidFacts(questions);
        }

        [Test]
        public void GenerateQuestions_InitializesResponsesAndScores()
        {
            var model = GivenAPracticeSessionModel(10, 10, new EArithmeticOperator[] {EArithmeticOperator.Add });
            GivenAPracticeQuestionGeneratorIsConstructed(model);

            var questions = WhenQuestionsAreGenerated();

            ThenResponsesAndScoresAreProperlyInitialized(questions);
        }

        private void ThenResponsesAndScoresAreProperlyInitialized(IEnumerable<ArithmeticQuestion> questions)
        {
            foreach (var question in questions)
            {
                Assert.That(question.Response, Is.EqualTo(-1));
                Assert.That(question.QuestionScore, Is.EqualTo(EQuestionScore.Undefined));
            }
        }

        private void ThenGeneratedDivisionQuestionsAreValidFacts(IEnumerable<ArithmeticQuestion> questions)
        {
            foreach (var question in questions)
            {
                Assert.That(question.Result, Is.GreaterThanOrEqualTo(1));
                Assert.That(question.Result, Is.LessThanOrEqualTo(10));
                Assert.That(question.SecondOperand, Is.GreaterThanOrEqualTo(1));
                Assert.That(question.SecondOperand, Is.LessThanOrEqualTo(10));
                Assert.That(question.FirstOperand, Is.GreaterThanOrEqualTo(question.SecondOperand));
                Assert.That(question.FirstOperand, Is.EqualTo(question.Result * question.SecondOperand));
            }
        }

        private void ThenGeneratedMultiplicationQuestionsAreValidFacts(IEnumerable<ArithmeticQuestion> questions)
        {
            foreach (var question in questions)
            {
                Assert.That(question.FirstOperand, Is.GreaterThanOrEqualTo(0));
                Assert.That(question.FirstOperand, Is.LessThanOrEqualTo(10));
                Assert.That(question.SecondOperand, Is.GreaterThanOrEqualTo(0));
                Assert.That(question.SecondOperand, Is.LessThanOrEqualTo(10));
                Assert.That(question.Result, Is.EqualTo(question.FirstOperand * question.SecondOperand));
            }
        }

        private void ThenGeneratedSubtractionQuestionsAreValidFacts(IEnumerable<ArithmeticQuestion> questions)
        {
            foreach (var question in questions)
            {
                Assert.That(question.FirstOperand, Is.GreaterThanOrEqualTo(0));
                Assert.That(question.FirstOperand, Is.LessThanOrEqualTo(10));
                Assert.That(question.SecondOperand, Is.GreaterThanOrEqualTo(0));
                Assert.That(question.SecondOperand, Is.LessThanOrEqualTo(10));
                Assert.That(question.FirstOperand, Is.GreaterThanOrEqualTo(question.SecondOperand));
                Assert.That(question.Result, Is.EqualTo(question.FirstOperand - question.SecondOperand));
            }
        }

        private void ThenGeneratedAdditionQuestionsAreValidFacts(IEnumerable<ArithmeticQuestion> questions)
        {
            foreach (var question in questions)
            {
                Assert.That(question.FirstOperand, Is.GreaterThanOrEqualTo(0));
                Assert.That(question.FirstOperand, Is.LessThanOrEqualTo(10));
                Assert.That(question.SecondOperand, Is.GreaterThanOrEqualTo(0));
                Assert.That(question.SecondOperand, Is.LessThanOrEqualTo(10));
                Assert.That(question.Result, Is.EqualTo(question.FirstOperand + question.SecondOperand));
            }
        }

        private void ThenUnselectedOperatorsDoNotAppearInGeneratedQuestions(IEnumerable<ArithmeticQuestion> questions, IEnumerable<EArithmeticOperator> operatorSelections)
        {
            var numberOfQuestionsWithUnselectedOperators = questions.Count(q => operatorSelections.Contains(q.Operator) == false);
            Assert.That(numberOfQuestionsWithUnselectedOperators, Is.EqualTo(0));
        }

        private void ThenTheAppropriateNumberofQuestionsAreGenerated(IEnumerable<ArithmeticQuestion> questions, int numberOfQuestions)
        {
            Assert.That(questions.Count, Is.EqualTo(numberOfQuestions));
        }

        private IEnumerable<ArithmeticQuestion> WhenQuestionsAreGenerated()
        {
            return practiceQuestionGenerator.GenerateQuestions();
        }

        private void GivenAPracticeQuestionGeneratorIsConstructed(PracticeSession model)
        {
            practiceQuestionGenerator = new ArithmeticPracticeQuestionGenerator(model);
        }

        private PracticeSession GivenAPracticeSessionModel(int numberOfMinutes, int numberOfQuestions, IEnumerable<EArithmeticOperator> operators)
        {
            return new PracticeSession(numberOfMinutes, numberOfQuestions, operators);
        }

        private void WhenPracticeQuestionGeneratorIsConstructed(PracticeSession model)
        {
            practiceQuestionGenerator = new ArithmeticPracticeQuestionGenerator(model);
        }
    }
}
