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
                        return "�����";
                    case TypeEnum.Footer:
                        return "������";
                    case TypeEnum.NewUser:
                        return "����� ������������";
                    case TypeEnum.GiveRights:
                        return "������ ����";
                    case TypeEnum.AddMaterialAuthor:
                        return "�������� �������� (������)";
                    case TypeEnum.AddMaterialSubscribers:
                        return "�������� �������� (�����������)";
                    case TypeEnum.NewTextAuthor:
                        return "�������� ����� � ���������� (�������)";
                    case TypeEnum.NewTextSubscribers:
                        return "�������� ����� � ���������� (�����������)";
                    case TypeEnum.NewFileAuthor:
                        return "�������� ���� � ���������� (�������)";
                    case TypeEnum.NewFileSubscribers:
                        return "�������� ���� � ���������� (�����������)";
                    case TypeEnum.NewBlogSubscribers:
                        return "����� ����";
                    case TypeEnum.NewComment:
                        return "����� �����������";
                    case TypeEnum.AddSubject:
                        return "��������� ����";
                }
                return "";
            }
        }
    }
}