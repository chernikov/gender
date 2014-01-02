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
    public class LinkView
    {

        public int ID { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public string Title { get; set; }

    }
}