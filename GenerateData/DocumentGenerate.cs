using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class DocumentGenerate
    {
        public static string[] AdjectiveMale = {"Высший", "Начальный", "Государственный", "Международный", "Европейский", "Азиатский", "Североамериканский", "Германский", "Восточноевропейский", 
                                               "Еврейский", "Таможенный", "Внутренний", "Внешний", "Российский", "Московский", "Киевский", "Спортивный", "Специальный"};
        public static string[] AdjectiveFemale = {"Высшая", "Начальная", "Государственная", "Международная", "Европейская", "Азиатская", "Североамериканская", "Британская", "Восточноевропейская", 
                                               "Еврейская", "Таможенная", "Внутренняя", "Внешняя", "Российская", "Московская", "Киевская", "Спортивная", "Специальная"};
        public static string[] AdjectiveMiddle = {"Высшее", "Начальное", "Государственное", "Международное", "Европейское", "Азиатское", "Североамериканское", "Британское", "Восточноевропейское", 
                                               "Еврейское", "Таможенное", "Внутреннее", "Внешнее", "Российское", "Московское", "Киевское", "Спортивное", "Специальное"};

        public static string[] NounMale = {"документ", "закон"};
        public static string[] NounFemale = {"бумага"};
        public static string[] NounMiddle = { "предписание", "заявление", "постановление", "распоряжение", "свидетельство", "решение" };

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
                return string.Format("{0} {1} {2} от {3}", AdjectiveMale[rand.Next(AdjectiveMale.Count())], mainWord, Subjective[rand.Next(Subjective.Count())], DateGenerate.GetRandom().ToString("dd.MM.yyyy"));
            }
            if (NounFemale.Contains(mainWord))
            {
                return string.Format("{0} {1} {2} от {3}", AdjectiveFemale[rand.Next(AdjectiveFemale.Count())], mainWord, Subjective[rand.Next(Subjective.Count())], DateGenerate.GetRandom().ToString("dd.MM.yyyy"));
            }
            if (NounMiddle.Contains(mainWord))
            {
                return string.Format("{0} {1} {2} от {3}", AdjectiveMiddle[rand.Next(AdjectiveMiddle.Count())], mainWord, Subjective[rand.Next(Subjective.Count())], DateGenerate.GetRandom().ToString("dd.MM.yyyy"));
            }
            return string.Empty;
        }

    }
}
