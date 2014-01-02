using gender.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class LinkGenerate
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

        public static string[] DomenArr = { "yandex.ru", "vk.com", "google.ru", "google.com", "mail.ru",
            "youtube.com", "odnoklassniki.ru", "facebook.com", "wikipedia.org", "liveinternet.ru",
            "livejournal.com", "avito.ru", "rambler.ru", "rutracker.org", "ucoz.ru",
            "twitter.com", "blogspot.ru", "sberbank.ru", "qtrax.com", "habrahabr.ru",
            "searchengines.ru", "rbc.ru", "sape.ru", "narod.ru", "webmoney.ru",
            "kinopoisk.ru", "ya.ru", "lenta.ru", "cy-pr.com", "gogetlinks.net",
            "pr-cy.ru", "rutor.org", "gismeteo.ru", "googleusercontent.com", "auto.ru",
            "ebay.com", "aliexpress.com", "vk.me", "drom.ru", "linkedin.com",
            "hh.ru", "ria.ru", "yahoo.com", "wmmail.ru", "subscribe.ru",
            "instagram.com", "microsoft.com", "miralinks.ru", "tiu.ru", "wmtransfer.com",
            "alfabank.ru", "echo.msk.ru", "fotostrana.ru", "gazeta.ru", "mmgp.ru",
            "mts.ru", "nic.ru", "qiwi.com", "xhamster.com", "seopult.ru",
            "ucoz.com", "yaplakal.com", "alibaba.com", "ozon.ru", "seosprint.net",
            "adobe.com", "directadvert.ru", "justclick.ru", "irr.ru", "xvideos.com",
            "gi-akademie.com", "vesti.ru", "seobuilding.ru", "apple.com", "kp.ru" };


        public static string[] SocialDomenArr = {"vk.com/{0}", "my.mail.ru/mail/{0}", "www.youtube.com/user/{0}", "www.odnoklassniki.ru/profile/{0}", "facebook.com/{0}", "liveinternet.ru/users/{0}",
            "{0}.livejournal.com", "twitter.com/{0}", "{0}.blogspot.ru", "habrahabr.ru/users/{0}", "{0}.narod.ru", "{0}.ya.ru","www.linkedin.com/profile/view?id={0}", "instagram.com/{0}"};

      
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        public static string GetRandomLink()
        {
            return string.Format("http://{0}", DomenArr[rand.Next(DomenArr.Count())]);
        }


        public static string GetRandomSocial(string Name, string Surname)
        {
            var typeOfGenerate = (TypeOfGenerate)((rand.Next() + 1) % 7);

            var year = (rand.Next() % 90) + 1910;
            var domain = SocialDomenArr[rand.Next(SocialDomenArr.Count())];

            var translitName = Translit.Translate(Name);
            var translitSurname = Translit.Translate(Surname);

            switch (typeOfGenerate)
            {
                case TypeOfGenerate.NameDotSurname:
                    return string.Format("http://"+ domain, string.Format("{0}_{1}", translitName, translitSurname));
                case TypeOfGenerate.AttrDotSurname:
                    return string.Format("http://" + domain, string.Format("{0}_{1}", translitName.Substring(0, 1).ToUpper(), translitSurname));
                case TypeOfGenerate.NameSurname:
                    return string.Format("http://" + domain, string.Format("{0}{1}", translitName, translitSurname));
                case TypeOfGenerate.NameDotSurnameYear:
                    return string.Format("http://" + domain, string.Format("{0}_{1}{2}", translitName, translitSurname, year));
                case TypeOfGenerate.AttrDotSurnameYear:
                    return string.Format("http://" + domain, string.Format("{0}_{1}{2}", translitName.Substring(0, 1).ToUpper(), translitSurname, year));
                default:
                    return string.Format("http://" + domain, string.Format("{0}{1}{2}", translitName, translitSurname, year));
            }
        }
    }
}
