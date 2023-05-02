using ArithmeticPractice.Enums;
using ArithmeticPractice.Interfaces;
using ArithmeticPractice.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ArithmeticPracticeUnitTests
{
    [TestFixture]
    public class ArithmeticPracticeViewModelTests
    {
        private ArithmeticPracticeViewModel viewModel;
        private Mock<ISessionTimer> sessionTimer;

        [SetUp]
        public void Setup()
        {
            sessionTimer = new Mock<ISessionTimer>();
            viewModel = new ArithmeticPracticeViewModel(sessionTimer.Object);
        }

        [TestCase(1, 1, new EArithmeticOperator[] {EArithmeticOperator.Add})]
        [TestCase(1, 2, new EArithmeticOperator[] { EArithmeticOperator.Add, EArithmeticOperator.Subtract })]
        [TestCase(10, 50, new EArithmeticOperator[] { EArithmeticOperator.Multiply, EArithmeticOperator.Divide })]
        [TestCase(15, 100, new EArithmeticOperator[] { EArithmeticOperator.Add, EArithmeticOperator.Subtract, EArithmeticOperator.Multiply, EArithmeticOperator.Divide, EArithmeticOperator.Exponent })]
        public void GeneratePracticeSession_GeneratesPracticeSession(int numberOfMinutes, int numberOfQuestions, IEnumerable<EArithmeticOperator> arithmeticOperators)
        {
            WhenPracticeSessionIsGenerated(numberOfMinutes, numberOfQuestions, arithmeticOperators);
            ThenPracticeSessionIsGeneratedWithTheCorrectNumberOfMinutesQuestionsAndOperators(numberOfMinutes, numberOfQuestions, arithmeticOperators);
        }

        [Test]
        public void StartPracticeSession_BeginsTimer()
        {
            GivenAOneMinutePracticeSessionIsGenerated();

            WhenPracticeSessionIsStarted();

            ThenTimerIsStarted();
        }

        [Test]
        public void GeneratePracticeSession_SetsSessionState()
        {
            GivenAOneMinutePracticeSessionIsGenerated();
            GivenPracticeSessionIsStarted();
            GivenPracticeSessionIsEnded();

            WhenPracticeSessionIsGenerated(1, 1, new EArithmeticOperator[] { EArithmeticOperator.Add });

            ThenPracticeSessionStateIsSetTo(EPracticeSessionState.Generated);
        }

        [Test]
        public void StartPracticeSession_SetsSessionState()
        {
            GivenAOneMinutePracticeSessionIsGenerated();
            GivenPracticeSessionIsStarted();
            GivenPracticeSessionIsEnded();
            GivenAOneMinutePracticeSessionIsGenerated();
            
            WhenPracticeSessionIsStarted();

            ThenPracticeSessionStateIsSetTo(EPracticeSessionState.Started);
        }

        [Test]
        public void EndPracticeSession_SetsSessionState()
        {
            GivenAOneMinutePracticeSessionIsGenerated();
            GivenPracticeSessionIsStarted();
            
            WhenPracticeSessionIsEnded();

            ThenPracticeSessionStateIsSetTo(EPracticeSessionState.Ended);
        }

        private void WhenPracticeSessionIsEnded()
        {
            viewModel.EndPracticeSession();
        }

        private void ThenPracticeSessionStateIsSetTo(EPracticeSessionState sessionState)
        {
            Assert.That(viewModel.CurrentSessionState, Is.EqualTo(sessionState));
        }

        private void GivenPracticeSessionIsEnded()
        {
            viewModel.EndPracticeSession();
        }

        private void GivenPracticeSessionIsStarted()
        {
            viewModel.StartPracticeSession();
        }

        private void ThenTimerIsStarted()
        {
            sessionTimer.Verify(m => m.Start(1), Times.Once);
        }

        private void WhenPracticeSessionIsStarted()
        {
            viewModel.StartPracticeSession();
        }

        private void GivenAOneMinutePracticeSessionIsGenerated()
        {
            viewModel.GeneratePracticeSession(1, 1, new EArithmeticOperator[] { EArithmeticOperator.Add });
        }

        private void ThenPracticeSessionIsGeneratedWithTheCorrectNumberOfMinutesQuestionsAndOperators(int numberOfMinutes, int numberOfQuestions, IEnumerable<EArithmeticOperator> arithmeticOperators)
        {
            Assert.That(viewModel.CurrentPracticeSession.NumberOfMinutes, Is.EqualTo(numberOfMinutes));
            Assert.That(viewModel.CurrentPracticeSession.NumberOfQuestions, Is.EqualTo(numberOfQuestions));
            Assert.That(viewModel.CurrentPracticeSession.ArithmeticOperators, Is.EqualTo(arithmeticOperators));
        }

        private void WhenPracticeSessionIsGenerated(int numberOfMinutes, int numberOfQuestions, IEnumerable<EArithmeticOperator> arithmeticOperators)
        {
            viewModel.GeneratePracticeSession(numberOfMinutes, numberOfQuestions, arithmeticOperators);
        }
    }
}
