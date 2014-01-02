using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;
using System.ComponentModel.DataAnnotations;


namespace gender.Models.ViewModels
{ 
	public class PageView
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "¬ведите им€")]
		public string Name {get; set; }

        [Required(ErrorMessage = "¬ведите заголовок")]
		public string Header {get; set; }

        [Required(ErrorMessage = "¬ведите url")]
        public string Url {get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "¬ведите текст")]
		public string Text {get; set; }

    }
}