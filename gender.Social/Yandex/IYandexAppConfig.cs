using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gender.Social.Yandex
{
    public interface IYandexAppConfig
    {
        string AppId { get; }

        string AppSecret { get; }
    }
}
