﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gender.Mappers
{
    public interface IMapper
    {
        object Map(object source, Type sourceType, Type destinationType);
    }
}