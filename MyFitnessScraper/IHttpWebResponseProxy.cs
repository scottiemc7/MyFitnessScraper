﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MyFitnessScraper
{
    public interface IHttpWebResponseProxy : IDisposable
    {
        CookieCollection Cookies { get; set; }
        Stream GetResponseStream();
    }
}
