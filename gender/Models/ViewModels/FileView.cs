using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageAttribute;
using gender.Model;


namespace gender.Models.ViewModels
{ 
	public class FileView
    {
        public int ID { get; set; }

		public string Path {get; set; }

        public string Name { get; set; }

		public bool IsImage {get; set; }

		public string MimeType {get; set; }
    }
}