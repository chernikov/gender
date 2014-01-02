using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{
    public partial class UpdateRecord
    {
        public enum TypeEnum : int
        {
            New = 0x01,
            NewWithoutText = 0x02,
            NewText = 0x03,
            NewFile = 0x04,
            NewFiles = 0x05,
            NewLink = 0x06,
            NewLinks = 0x07,
            NewUser = 0x08,
        }

        public enum MaterialTypeEnum : int
        {
            Article = 0x01,
            Document = 0x02,
            Event = 0x03,
            Image = 0x04,
            Organization = 0x05,
            Person = 0x06,
            Publication = 0x07,
            StudyMaterial = 0x08,
            WebLink = 0x09,
            User = 0x0A
        };


        public string ShortDesc
        {
            get
            {
                switch ((TypeEnum)Type)
                {
                    case TypeEnum.New:

                        switch ((MaterialTypeEnum)MaterialType)
                        {
                            case MaterialTypeEnum.Article:
                                return "����� ������:";
                            case MaterialTypeEnum.Document:
                                return "����� ��������:";
                            case MaterialTypeEnum.Event:
                                return "����� �������:";
                            case MaterialTypeEnum.Image:
                                return "����� �����������:";
                            case MaterialTypeEnum.Organization:
                                return "����� �����������:";
                            case MaterialTypeEnum.Person:
                                return "����� �������:";
                            case MaterialTypeEnum.Publication:
                                return "����� ����������:";
                            case MaterialTypeEnum.StudyMaterial:
                                return "����� ������� ��������:";
                            case MaterialTypeEnum.WebLink:
                                return "����� ���-������:";
                            case MaterialTypeEnum.User:
                                return "����� ������������:";
                        }
                        break;
                    case TypeEnum.NewWithoutText:
                        return "��������� ���������� (������ �������� ������):";
                    case TypeEnum.NewText:
                        return "�������� ����� � ����������:";
                    case TypeEnum.NewFile:
                        return "����� ���� � ����������:";
                    case TypeEnum.NewFiles:
                        return "����� ����� � ����������:";
                    case TypeEnum.NewLink:
                        return "��������� ������ � ����������:";
                    case TypeEnum.NewLinks:
                        return "��������� ������ � ����������:";
                    case TypeEnum.NewUser:
                        return "����� ������������:";
                }
                return "";
            }
        }

        public string Icon
        {
            get
            {
                switch ((TypeEnum)Type)
                {
                    case TypeEnum.New:

                        switch ((MaterialTypeEnum)MaterialType)
                        {
                            case MaterialTypeEnum.Article:
                                return "bookmark";
                            case MaterialTypeEnum.Document:
                                return "file";
                            case MaterialTypeEnum.Event:
                                return "flag";
                            case MaterialTypeEnum.Image:
                                return "picture";
                            case MaterialTypeEnum.Organization:
                                return "group";
                            case MaterialTypeEnum.Person:
                                return "user";
                            case MaterialTypeEnum.Publication:
                                return "book";
                            case MaterialTypeEnum.StudyMaterial:
                                return "briefcase";
                            case MaterialTypeEnum.WebLink:
                                return "globe";
                            case MaterialTypeEnum.User:
                                return "user";
                        }
                        break;
                    case TypeEnum.NewWithoutText:
                        return "book";
                    case TypeEnum.NewText:
                        return "book";
                    case TypeEnum.NewFile:
                        return "book";
                    case TypeEnum.NewFiles:
                        return "book";
                    case TypeEnum.NewLink:
                        return "book";
                    case TypeEnum.NewLinks:
                        return "book";
                    case TypeEnum.NewUser:
                        return "user";
                }
                return "";
            }
        }

        public string Link { get; set; }

        public string NameLink { get; set; }

        public bool Moderated { get; set; }
    }
}