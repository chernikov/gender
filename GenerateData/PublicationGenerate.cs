using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class PublicationGenerate
    {
        public static string[] AdjectiveMale = {"Высший", "Начальный", "Государственный", "Международный", "Европейский", "Азиатский", "Североамериканский", "Германский", "Восточноевропейский", 
                                               "Еврейский", "Таможенный", "Внутренний", "Внешний", "Российский", "Московский", "Киевский", "Спортивный", "Специальный"};
        public static string[] AdjectiveFemale = {"Высшая", "Начальная", "Государственная", "Международная", "Европейская", "Азиатская", "Североамериканская", "Британская", "Восточноевропейская", 
                                               "Еврейская", "Таможенная", "Внутренняя", "Внешняя", "Российская", "Московская", "Киевская", "Спортивная", "Специальная"};
        public static string[] AdjectiveMiddle = {"Высшее", "Начальное", "Государственное", "Международное", "Европейское", "Азиатское", "Североамериканское", "Британское", "Восточноевропейское", 
                                               "Еврейское", "Таможенное", "Внутреннее", "Внешнее", "Российское", "Московское", "Киевское", "Спортивное", "Специальное"};

        public static string[] NounMale = {"пункт", "параграф", "реферат", "оттиск", "очерк", "раздел", "некролог", "атрикул" };
        public static string[] NounFemale = { "рецензия", "критика", "статья", "статья-обзор", "книга",  };
        public static string[] NounMiddle = {"послесловие", "предословие"};

        public static string[] Subjective = {"экономики", "политики", "философии", "физики", "Африки", "по проблемам китов", "г. Москвы", "г. Киева",
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
