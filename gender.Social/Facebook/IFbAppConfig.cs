﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gender.Social.Facebook
{
    public interface IFbAppConfig
    {
        string AppId { get; }

        string AppSecret { get; }
    }
}
