using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class SkypeGenerate
    {
        public enum TypeOfGenerate
        {
            NameDotSurname = 0x01,
            AttrDotSurname = 0x02,
            NameSurname = 0x03,
            NameDotSurnameYear = 0x04,
            AttrDotSurnameYear = 0x05,
            NameSurnameYear = 0x06,

        }
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        public static string GetRandom(string Name, string Surname)
        {
            var typeOfGenerate = (TypeOfGenerate)((rand.Next() + 1) % 7);

            var year = (rand.Next() % 90) + 1910;

        
            var translitName = Translit.Translate(Name);
            var translitSurname = Translit.Translate(Surname);

            switch (typeOfGenerate)
            {
                case TypeOfGenerate.NameDotSurname:
                    return string.Format("{0}.{1}", translitName, translitSurname);
                case TypeOfGenerate.AttrDotSurname:
                    return string.Format("{0}.{1}", translitName.Substring(0, 1).ToUpper(), translitSurname);
                case TypeOfGenerate.NameSurname:
                    return string.Format("{0}{1}", translitName, translitSurname);
                case TypeOfGenerate.NameDotSurnameYear:
                    return string.Format("{0}.{1}{2}", translitName, translitSurname, year);
                case TypeOfGenerate.AttrDotSurnameYear:
                    return string.Format("{0}.{1}{2}", translitName.Substring(0, 1).ToUpper(), translitSurname, year);
                default:
                    return string.Format("{0}{1}{2}", translitName, translitSurname, year);
            }
        }
    }
}
