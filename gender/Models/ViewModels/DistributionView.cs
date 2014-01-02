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
	public class DistributionView
    {
        [PrimaryField]
        public int ID { get; set; }

        [ShowIndex]
        [TextBoxField]
        [Required(ErrorMessage = "¬ведите заголовок")]
		public string Subject {get; set; }

        [AllowHtml]
        [Required(ErrorMessage="¬ведите текст")]
		public string Body {get; set; }

        [ShowIndex]
		public bool IsStart {get; set; }

    }
}