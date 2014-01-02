using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class EmailGenerate
    {
        public static string[] EmailDomains = { "@mail.ru", "@gmail.com", "@list.ru", "@bk.ru", "@yandex.ru" };


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

            var domainIndex = rand.Next(EmailDomains.Count());

            var domain = EmailDomains[domainIndex];
            var translitName = Translit.Translate(Name);
            var translitSurname = Translit.Translate(Surname);

            switch (typeOfGenerate)
            {
                case TypeOfGenerate.NameDotSurname:
                    return string.Format("{0}.{1}{2}", translitName, translitSurname, domain);
                case TypeOfGenerate.AttrDotSurname:
                    return string.Format("{0}.{1}{2}", translitName.Substring(0, 1).ToUpper(), translitSurname, domain);
                case TypeOfGenerate.NameSurname:
                    return string.Format("{0}{1}{2}", translitName, translitSurname, domain);
                case TypeOfGenerate.NameDotSurnameYear:
                    return string.Format("{0}.{1}{2}{3}", translitName, translitSurname, year, domain);
                case TypeOfGenerate.AttrDotSurnameYear:
                    return string.Format("{0}.{1}{2}{3}", translitName.Substring(0, 1).ToUpper(), translitSurname, year, domain);
                default:
                    return string.Format("{0}{1}{2}{3}", translitName, translitSurname, year, domain);
            }
        }

        public static string GetRandomOrganization(string Name)
        {
            var typeOfGenerate = (TypeOfGenerate)((rand.Next() + 1) % 7);

            var year = (rand.Next() % 90) + 1910;

            var domainIndex = rand.Next(EmailDomains.Count());

            var domain = EmailDomains[domainIndex];
            var names = Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Count() > 2)
            {
                var translitName = Translit.Translate(names[1]);
                return string.Format("{0}{1}", translitName, domain);
            }
            else
            {
                var translitName = Translit.Translate(Name.Replace(" ", ""));
                return string.Format("{0}{1}", translitName, domain);
            }
        }            
            
    }
}
