﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    public interface IDataFormatter
    {
        string Format(List<ITrackableDay> days);
    }
}
