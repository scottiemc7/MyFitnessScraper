﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    public interface IFood
    {
        string Name { get; set; }
        int CaloricContent { get; set; }
        List<INutrient> Nutrients { get; set; }
    }
}
