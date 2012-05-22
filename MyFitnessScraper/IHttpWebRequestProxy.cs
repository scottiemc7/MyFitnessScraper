﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MyFitnessScraper
{
    public interface IHttpWebRequestProxy
    {
        string Method { get; set; }
        string ContentType { get; set; }
        long ContentLength { get; set; }
        CookieContainer CookieContainer { get; set; }

        Stream GetRequestStream();
        IHttpWebResponseProxy GetResponse();
    }
}
