using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;


namespace gender.Models.ViewModels
{
    public class SubjectView
    {
        public int _ID
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
            }
        }

        public int ID { get; set; }

        public int? ParentID { get; set; }

        public string Name { get; set; }

        public bool MainShow { get; set; }
    }
}