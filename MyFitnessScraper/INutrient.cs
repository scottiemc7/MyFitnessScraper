﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    public interface INutrient
    {
        string Name { get; set; }
        decimal Amount { get; set; }
    }
}
