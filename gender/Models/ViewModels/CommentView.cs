using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gender.Models.ViewModels
{
    public class CommentView
    {
        public int ID { get; set; }

        public int OwnerID { get; set; }

        public int? ParentID { get; set; }

        public int UserID { get; set; }

        [Required]
        public string Text { get; set; }
    }
}