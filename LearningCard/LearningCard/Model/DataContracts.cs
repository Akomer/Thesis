// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Runtime.Serialization;

// namespace LearningCard.Model
// {
//     [DataContract]
//     internal class Card
//     {
//         [DataMember]
//         internal String Title;

//         [DataMember]
//         internal IQuestion Question;

//         [DataMember]
//         internal IAnswer Answer;

//     }

//     [DataContract]
//     internal class QuestionTextModel : IQuestion
//     {
//         [DataMember]
//         internal String Text;
//     }

//     [DataContract]
//     internal class QuestionPictureModel : IQuestion
//     {
//         [DataMember]
//         internal Uri ImageSRC;
//         [DataMember]
//         internal String Text;
//     }

//     [DataContract]
//     internal class AnswerTextModel : IAnswer
//     {
//         [DataMember]
//         internal String Text;
//     }
// }
