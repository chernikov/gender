using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class DocumentAccess : IAccess
    {
        public IMaterial Material
        {
            get { return Document; }
        }
    }
}