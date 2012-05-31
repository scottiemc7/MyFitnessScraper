using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    public interface ITrackableDay
    {
        DateTime Date { get; set; }
        List<IMeal> Meals { get; set; }
        int WaterCups { get; set; }
        string Notes { get; set; }
    }
}
