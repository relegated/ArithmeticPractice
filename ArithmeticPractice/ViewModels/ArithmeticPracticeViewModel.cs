using ArithmeticPractice.Enums;
using ArithmeticPractice.Interfaces;
using ArithmeticPractice.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArithmeticPractice.ViewModels
{
    public class ArithmeticPracticeViewModel : INotifyPropertyChanged
    {
        private readonly ISessionTimer sessionTimer;
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public PracticeSession CurrentPracticeSession { get; private set; }
        public EPracticeSessionState CurrentSessionState { get; set; } = EPracticeSessionState.Undefined;

        private int numberOfQuestions;
        public int NumberOfQuestions { get => numberOfQuestions; set => SetProperty(ref numberOfQuestions, value); }


        private int numberOfMinutes;
        public int NumberOfMinutes { get => numberOfMinutes; set => SetProperty(ref numberOfMinutes, value); }


        private bool? isAdditionChecked;
        public bool? IsAdditionChecked { get => isAdditionChecked; set => SetProperty(ref isAdditionChecked, value); }


        private bool? isSubtractionChecked;
        public bool? IsSubtractionChecked { get => isSubtractionChecked; set => SetProperty(ref isSubtractionChecked, value); }

        private bool? isMultiplicationChecked;
        public bool? IsMultiplicationChecked { get => isMultiplicationChecked; set => SetProperty(ref isMultiplicationChecked, value); }

        private bool? isDivisionChecked;
        public bool? IsDivisionChecked { get => isDivisionChecked; set => SetProperty(ref isDivisionChecked, value); }

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

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }


    }
}
