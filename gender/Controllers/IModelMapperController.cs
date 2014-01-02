using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gender.Mappers;

namespace gender.Controllers
{
    public interface IModelMapperController
    {
        IMapper ModelMapper { get; }
    }
}
