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
            this.CardPack.Add(new Card() { Title = "T1" });
            this.CardPack.Add(new Card() { Title = "T2" });
            this.CardPack.Add(new Card() { Title = "T3" });
        }

        public void AddCard(String title)
        {
            this.CardPack.Add(new Card() { Title = title });
        }

        //public void AddCard(String title, IQuestion question, IAnswer answer)
        //{
        //    this.CardPack.Add(new Card() { Title = title, Question = question, Answer = answer });
        //}
    }
}
