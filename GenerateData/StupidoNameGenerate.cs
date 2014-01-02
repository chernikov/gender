using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class StupidoNameGenerate
    {
        public static string[] AdjectiveMale = {"Высший", "Начальный", "Зеленый", "Круглый", "Легкий", "Свежий", "Воздушный", "Заграничный", "Свадебный", "Родной", "Властный", "Режимный", "Красочный", "Восстановленный", "Любимый", "Осторожный"};
        public static string[] AdjectiveFemale = { "Высшая", "Начальная", "Зеленая", "Круглая", "Легкая", "Свежая", "Воздушная", "Заграничная", "Свадебная", "Родная", "Властная", "Режимная", "Красочная", "Восстановленная", "Любимая", "Осторожная" };
        public static string[] AdjectiveMiddle = { "Высшее", "Начальное", "Зеленое", "Круглое", "Легкое", "Свежее", "Воздушное", "Заграничное", "Свадебное", "Родное", "Властное", "Режимное", "Красочное", "Восстановленное", "Любимое", "Осторожное" };

        public static string[] NounMale = {"кот", "конь", "закон", "разворот", "самолет", "поезд", "шар", "майонез", "воздух", "паспорт", "торт", "объект", "закат", "белок", "заяц", "водитель"};
        public static string[] NounFemale = {"кошка", "лошадь", "марионетка", "дуга", "стюардесса", "корзина", "вагонетка", "молния", "дыня", "тревога", "таможня", "конфета", "ночь", "белка", "лиса", "сова"};
        public static string[] NounMiddle = { "олово", "зарево", "колесо", "окно", "крыло", "облако" };

        
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        public static string GetRandom()
        {
            var all = NounMale.Union(NounFemale).Union(NounMiddle);
            var mainWord = all.OrderBy(p => Guid.NewGuid()).FirstOrDefault();

            if (NounMale.Contains(mainWord))
            {
                return string.Format("{0} {1}", AdjectiveMale[rand.Next(AdjectiveMale.Count())], mainWord);
            }
            if (NounFemale.Contains(mainWord))
            {
                return string.Format("{0} {1}", AdjectiveFemale[rand.Next(AdjectiveFemale.Count())], mainWord);
            }
            if (NounMiddle.Contains(mainWord))
            {
                return string.Format("{0} {1}", AdjectiveMiddle[rand.Next(AdjectiveMiddle.Count())], mainWord);
            }
            return string.Empty;
        }

    }
}
