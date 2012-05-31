﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    public interface IMeal
    {
		//DateTime Date { get; set; }
        string Name { get; set; }
        List<IFood> Foods { get; set; }
    }
}
