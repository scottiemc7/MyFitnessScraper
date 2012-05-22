﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    class Food : IFood
    {
        public string Name
        {
            get;
            set;
        }

        public int CaloricContent
        {
            get;
            set;
        }


        public List<INutrient> Nutrients
        {
            get;
            set;
        }
    }
}
