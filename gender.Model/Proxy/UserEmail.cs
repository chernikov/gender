using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Model
{ 
    public partial class UserEmail
    {
        public bool Activated
        {
            get
            {
                return ActivateDate.HasValue;
            }
        }
	}
}