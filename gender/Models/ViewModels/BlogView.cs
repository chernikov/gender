using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;


namespace gender.Models.ViewModels
{ 
	public class BlogView
    {

        public int ID { get; set; }

		public int UserID {get; set; }

		public DateTime LastUpdate {get; set; }

    }
}