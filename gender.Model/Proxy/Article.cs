using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class Article
    {
        public IEnumerable<Subject> SubSubjects
        {
            get
            {
                return ArticleSubjects.Select(p => p.Subject);
            }
        }

        public IEnumerable<Subject> MainSubjects
        {
            get
            {
                var allParents = ArticleSubjects.Select(p => p.Subject).ToList().Select(p => p.Ancestor).Distinct();


                return allParents;
            }
        }

        
	}
}