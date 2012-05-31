using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    class TrackableDay : ITrackableDay
    {
        public DateTime Date { get; set; }
        public List<IMeal> Meals { get; set; }
        public int WaterCups { get; set; }
        public string Notes { get; set; }
    }
}
