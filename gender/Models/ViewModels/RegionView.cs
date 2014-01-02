using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;
using gender.Attributes.Validation;


namespace gender.Models.ViewModels
{ 
	public class RegionView
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

        public int? ParentID { get; set; }

        public int ID { get; set; }

		public string Name {get; set; }

        [AllowHtml]
        public string Map { get; set; }

        public string Description { get; set; }

        [ValidUrl]
        public string Link { get; set; }
    }
}