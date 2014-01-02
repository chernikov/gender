using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class SubscriptionTemplate
    {
        public enum TypeEnum : int
        {
            Header = 0x01,
            Footer = 0x02,
            NewUser = 0x03,
            GiveRights = 0x04,
            AddMaterialAuthor = 0x05,
            AddMaterialSubscribers = 0x06,
            NewTextAuthor = 0x07,
            NewTextSubscribers = 0x08,
            NewFileAuthor = 0x09,
            NewFileSubscribers = 0x0A,
            NewBlogSubscribers = 0x0B,
            NewComment = 0x0C,
            AddSubject = 0x0D
        }

        public string TypeStr
        {
            get
            {
                switch ((TypeEnum)Type)
                {
                    case TypeEnum.Header:
                        return "Шапка";
                    case TypeEnum.Footer:
                        return "Подвал";
                    case TypeEnum.NewUser:
                        return "Новый пользователь";
                    case TypeEnum.GiveRights:
                        return "Выдача прав";
                    case TypeEnum.AddMaterialAuthor:
                        return "Добавлен материал (автору)";
                    case TypeEnum.AddMaterialSubscribers:
                        return "Добавлен материал (подписчикам)";
                    case TypeEnum.NewTextAuthor:
                        return "Добавлен текст в публикацию (авторам)";
                    case TypeEnum.NewTextSubscribers:
                        return "Добавлен текст в публикацию (подписчикам)";
                    case TypeEnum.NewFileAuthor:
                        return "Добавлен файл в публикацию (авторам)";
                    case TypeEnum.NewFileSubscribers:
                        return "Добавлен файл в публикацию (подписчикам)";
                    case TypeEnum.NewBlogSubscribers:
                        return "Новый блог";
                    case TypeEnum.NewComment:
                        return "Новый комментарий";
                    case TypeEnum.AddSubject:
                        return "Добавлена тема";
                }
                return "";
            }
        }
    }
}