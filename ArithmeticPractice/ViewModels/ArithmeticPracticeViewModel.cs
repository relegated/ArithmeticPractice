using ArithmeticPractice.Enums;
using ArithmeticPractice.Interfaces;
using ArithmeticPractice.Models;
using System;
using System.Collections.Generic;

namespace ArithmeticPractice.ViewModels
{
    public class ArithmeticPracticeViewModel
    {
        private readonly ISessionTimer sessionTimer;

        public PracticeSession CurrentPracticeSession { get; private set; }
        public EPracticeSessionState CurrentSessionState { get; set; } = EPracticeSessionState.Undefined;

        public ArithmeticPracticeViewModel()
        {
           //needed for designer -- DO NOT USE
        }

        public ArithmeticPracticeViewModel(ISessionTimer sessionTimer)
        {
            this.sessionTimer = sessionTimer;
        }

        public void GeneratePracticeSession(int numberOfMinutes, int numberOfQuestions, IEnumerable<EArithmeticOperator> arithmeticOperators)
        {
            CurrentPracticeSession = new PracticeSession(numberOfMinutes, numberOfQuestions, arithmeticOperators);
            CurrentSessionState = EPracticeSessionState.Generated;
        }

        public void StartPracticeSession()
        {
            sessionTimer.Start(CurrentPracticeSession.NumberOfMinutes);
            CurrentSessionState = EPracticeSessionState.Started;
        }

        public void EndPracticeSession()
        {
            CurrentSessionState = EPracticeSessionState.Ended;
        }
    }
}
