﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    class Meal : IMeal
    {
		//public DateTime Date { get; set; }
        public List<IFood> Foods
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
