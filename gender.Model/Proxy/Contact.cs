using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Contact
    {
        public enum TypeEnum : int
        { 
            Email = 0x01,
            Phone = 0x02,
            Address = 0x03,
            Skype = 0x04, 
            Other = 0xA0
        }

        public string TypeStr
        {
            get
            {
                switch ((TypeEnum)Type)
                {
                    case TypeEnum.Address :
                        return "Адрес";
                    case TypeEnum.Email:
                        return "Email";
                    case TypeEnum.Phone:
                        return "Телефон";
                    case TypeEnum.Skype:
                        return "Скайп";
                    case TypeEnum.Other:
                        return "Другое";
                }
                return string.Empty;
            }
        }
	}
}