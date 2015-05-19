using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;
using System.IO;
using System.Runtime.Serialization.Json;
using System.IO.Compression;

namespace LearningCard.Model
{
    class ErrorWrappers
    {
        static public void SaveCardPackToFile(CardPack deck)
        {
            try
            {
                CardPack.SaveCardPackToFile(deck);
            }
            catch (ArgumentException)
            {
                System.Windows.Forms.MessageBox.Show(GlobalLanguage.Instance.GetDict()["CouldNotSaveCardPack"],
                    GlobalLanguage.Instance.GetDict()["SaveError"],
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
        }

        static public void ImportCardPack(String sourceFile)
        {
            try
            {
                CardPack.ImportCardPack(sourceFile);
            }
            catch (InvalidDataException)
            {
                System.Windows.Forms.MessageBox.Show(GlobalLanguage.Instance.GetDict()["InvalidDataException"],
                        GlobalLanguage.Instance.GetDict()["InvalidDataException"], System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}
