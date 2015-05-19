using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

namespace LearningCard.Model
{
    public class LanguageFactory
    {
        static public AnswerLotofTextModel DefaultAnswerLotofTextModel()
        {
            return new AnswerLotofTextModel(GlobalLanguage.Instance.GetDict()["NewAnswerLotofText"]);
        }

        static public AnswerExactTextModel DefaultAnswerExactTextModel()
        {
            return new AnswerExactTextModel(GlobalLanguage.Instance.GetDict()["NewAnswerExactText"]);
        }

        static public QuestionTextModel DefaultQuestionTextModel()
        {
            return new QuestionTextModel(GlobalLanguage.Instance.GetDict()["NewQuestionSimpleText"]);
        }

        static public QuestionPictureModel DefaultQuestionPictureModel(Uri imgeSrc)
        {
            return new QuestionPictureModel(imgeSrc, GlobalLanguage.Instance.GetDict()["NewQuestionPictureTitle"]);
        }

        static public TippMix DefaultTippMix()
        {
            return new TippMix() { IsChecked = false, TippText = GlobalLanguage.Instance.GetDict()["NewAnswerTippMixNewTipp"] };
        }

    }
}
