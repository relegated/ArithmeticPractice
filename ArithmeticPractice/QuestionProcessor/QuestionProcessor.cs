using ArithmeticPractice.Enums;
using ArithmeticPractice.Models;

namespace ArithmeticPractice.QuestionProcessor
{
    public class QuestionProcessor
    {
        public void ProcessQuestion(ArithmeticQuestion question, int response)
        {
            question.Response = response;
            question.QuestionScore = (response == question.Result
                                        ? EQuestionScore.Correct
                                        : EQuestionScore.Incorrect);
        }
    }
}
