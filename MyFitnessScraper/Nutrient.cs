﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    class Nutrient : INutrient
    {
        public string Name
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }
    }
}
