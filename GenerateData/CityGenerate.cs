﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateData
{
    public class CityGenerate
    {
        public static string[] CitiesArr = {"Абу-Даби", "Абудже", "Аккра", "Адамстауне",
            "Аддис-Абеба", "Алжир", "Alofi", "Амман", "Амстердам", "Андорра-ла-Велья",
            "Анкара", "Антананариву", "Апиа", "Ашхабад", "Асмэрой", "Астана",
            "Асунсьон", "Афины", "Avarua", "Багдад", "Баку", "Бамако",
            "Бандар-Сери-Бегаван", "Бангкок", "Банги", "Банжула", "Бастер", "Пекин",
            "Бейрут", "Белфаст", "Белград", "Бельмопана", "Берлин", "Берн",
            "Бишкек", "Бисау", "Богота", "Бразилия", "Братислава", "Браззавиль",
            "Бриджтаун", "Брюссель", "Бухарест", "Будапешт", "Буэнос-Айрес", "Бужумбуре",
            "Cairo", "Канберра", "Каракас", "Кардифф", "Кастри", "кайенский",
            "Шарлотта-Амалия", "Кишинэу", "Коберн-Таун", "Конакри", "Копенгаген", "Дакар",
            "Дамаск", "Дакка", "Дили", "Джибути", "Додомы", "Дар-эс-Салам",
            "Доха", "Дуглас", "Дублин", "Душанбе", "Эдинбург", "Эдинбург семи морей",
            "Эль-Аюн", "Эпископи Cantonment", "Летающие рыбы Cove", "Фритаун", "Фунафути", "Габороне",
            "Джорджтаун", "Джорджтаун", "Джорджтаун", "Гибралтар", "Grytviken", "Гватемала",
            "Густавиа", "Hagatna", "Гамильтон", "Ханга-Роа", "Ханой", "Хараре",
            "Харгейсу", "Гавана", "Хельсинки", "Хониаре", "Исламабад", "Джакарта",
            "Джеймстаун", "Иерусалим", "Джуба", "Кабул", "Кампала", "Катманду",
            "Хартум", "Киев", "Кигали", "Кингстон", "Кингстон", "Кингстон",
            "Киншаса", "Куала-Лумпур", "Эль-Кувейт", "Либревиль", "Лилонгве", "Лайма",
            "Лиссабон", "Любляна", "Ломе", "Лондон", "Луанда", "Лусака",
            "Люксембург", "Мадрид", "Маджуро", "Малабо", "Мужской", "Манагуа",
            "Манама", "Манила", "Мапуту", "Marigot", "Масеру", "Мата-Уту",
            "Мбабане", "Мелекеок", "Мехико", "Минск", "Могадишо", "Монако",
            "Монровия", "Монтевидео", "Морони", "Москва", "Мускат", "Нджамены",
            "Найроби", "Нассау", "Naypyidaw", "Нью-Дели", "Ниамей", "Никосия",
            "Никосия", "Нуакшот", "Нумеа", "Нук", "Ораньестада", "Осло",
            "Оттава", "Уагадугу", "Паго-Паго", "Паликир", "Панама-Сити", "Папеэте",
            "Парамарибо", "Париж", "Филипсбург", "Пномпень", "Плимут", "Подгорица",
            "Порт-Луи", "Порт-Морсби", "Порт-оф-Спейн", "Порт-Вила", "Порт-о-Пренс", "Порто-Ново",
            "Прага", "Praia", "Кейптаун", "Приштина", "Пхеньян", "Кито",
            "Рабат", "Рейкьявик", "Рига", "Эр-Рияд", "Род-Таун", "Рим",
            "Розо", "Сайпан", "Сан-Хосе", "Сан-Хуан", "Сан-Марино", "Сан-Сальвадор",
            "Сана", "Сантьяго", "Санто-Доминго", "Сараево", "Сеул", "Сингапур",
            "Скопье", "София", "Святого Георгия", "Сент-Хельер", "Сент-Джонс", "Санкт-Питер-Порт",
            "Сен-Пьер", "Стенли", "Степанакерт", "Стокгольм", "Сукре", "Ла-Пас",
            "Сухуми", "Сува", "Сан-Томе", "Тайбэй", "Таллин", "Тарава",
            "Ташкент", "Тбилиси", "Тегусигальпа", "Тегеран", "Долина", "Тхимпху",
            "Тираны", "Тирасполь", "Токио", "Триполи", "Цхинвал", "Тунис",
            "Торсхавн", "Улан-Батор", "Вадуц", "Валлетта", "Ватикан", "Виктория",
            "Вены", "Вьентьян", "Вильнюс", "Варшава", "Вашингтон", "Веллингтон",
            "Уэст-Айленд", "Виллемстад", "Виндхук", "Ямусукро", "Яунде", "Ереван",
            "Загреб"};

        public static string GetRandom()
        {
            var rand = new Random((int)DateTime.Now.Ticks);

            var index = rand.Next(CitiesArr.Count());
            return CitiesArr[index];
        }
    }
}
