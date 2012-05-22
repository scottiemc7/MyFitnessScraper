﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequestProxy Create(string requestUriString);
    }
}
