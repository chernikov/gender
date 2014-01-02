using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class EventGenerate
    {
        public static string[] AdjectiveMale = {"Международный", "Европейский", "Азиатский", "Североамериканский", "Германский", "Восточноевропейский", 
                                               "Варшавский", "Российский" };
        public static string[] AdjectiveFemale = {"Международная", "Европейская", "Азиатская", "Североамериканская", "Германская", "Восточноевропейская", 
                                               "Варшавская", "Российская" };
        public static string[] AdjectiveMiddle ={"Международное", "Европейское", "Азиатское", "Североамериканское", "Германское", "Восточноевропейское", 
                                               "Варшавское", "Российское" };

        public static string[] NounMale = { "распад", "союз" };
        public static string[] NounFemale = { "встреча", "битва", "пандемия", "депрессия", "революция" };
        public static string[] NounMiddle = { "событие", "рандеву", "завдение", "создание", "развитие", "подписание" };

        public static string[] Subjective = {"экономики", "политики", "философии", "физики", "Африки", "по проблемам китов", 
                                            "дизайна", "рисования", "физической культуры", "спорта", "финансов", "Объединенных наций", "джентльменов", "мелиораторов", "миллионеров",
                                            "здоровья"};

        private static Random rand = new Random((int)DateTime.Now.Ticks);

        public static string GetRandom()
        {
            var all = NounMale.Union(NounFemale).Union(NounMiddle);
            var mainWord = all.OrderBy(p => Guid.NewGuid()).FirstOrDefault();

            if (NounMale.Contains(mainWord))
            {
                return string.Format("{0} {1} {2}", AdjectiveMale[rand.Next(AdjectiveMale.Count())], mainWord, Subjective[rand.Next(Subjective.Count())]);
            }
            if (NounFemale.Contains(mainWord))
            {
                return string.Format("{0} {1} {2}", AdjectiveFemale[rand.Next(AdjectiveFemale.Count())], mainWord, Subjective[rand.Next(Subjective.Count())]);
            }
            if (NounMiddle.Contains(mainWord))
            {
                return string.Format("{0} {1} {2}", AdjectiveMiddle[rand.Next(AdjectiveMiddle.Count())], mainWord, Subjective[rand.Next(Subjective.Count())]);
            }
            return string.Empty;
        }

    }
}
