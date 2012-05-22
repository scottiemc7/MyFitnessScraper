﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
   public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequestProxy Create(string requestUriString)
        {
            return new HttpWebRequestProxy(requestUriString);
        }
    }
}
