using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class CreateQnAModel
    {
        public Uri SourceFile;
        public List<Model.Card> CardPack;
        // public List<String> CardPack;

        public CreateQnAModel()
        {
            this.CardPack = new List<Card>();
        }

        // public void AddCard(String title)
        // {
        //     this.CardPack.Add(new Card() { Title = title });
        // }

        public void AddCard(String title, IQuestion question, IAnswer answer)
        {
            this.CardPack.Add(new Card() { Title = title, Question = question, Answer = answer });
        }
    }
}
