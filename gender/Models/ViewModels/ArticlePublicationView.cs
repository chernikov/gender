using gender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Models.ViewModels
{
    public class ArticlePublicationView : PublicationView
    {
        public override int Type
        {
            get
            {
                return (int)Publication.TypeEnum.Article;
            }
        }

        public int? ParentID { get; set; }

        public string ParentName { get; set; }

    }
}