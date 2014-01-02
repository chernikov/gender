using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;


namespace gender.Models.ViewModels
{ 
	public class RedirectView
    {

        [PrimaryField]
        [HiddenField]
        public int ID { get; set; }

        [TextBoxField]
		public string OldLink {get; set; }

        [TextBoxField]
		public string NewLink {get; set; }

        [CheckBoxField]
		public bool IsForum {get; set; }

    }
}