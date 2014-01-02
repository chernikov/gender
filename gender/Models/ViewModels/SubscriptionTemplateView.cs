using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;


namespace gender.Models.ViewModels
{ 
	public class SubscriptionTemplateView
    {
        [PrimaryField]
        [HiddenField]
        public int ID { get; set; }

        [ShowIndex]
        [CheckBoxField]
		public bool IsActive {get; set; }

        [ShowIndex]
        [TextAreaField]
        [AllowHtml]
		public string Template {get; set; }

    }
}