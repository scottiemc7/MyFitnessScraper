using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFitnessScraper
{
    public class CSVDataFormatter : IDataFormatter
    {
        public string Format(List<ITrackableDay> days)
        {
			if (days == null || days.Count == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("MealDate, MealName, FoodName, Calories");

            //Grab first food in first meal that has food to get nutrients tracked
            IFood firstFood = null;
			for (int i = 0; i < days[0].Meals.Count; i++)
            {
				if (days[0].Meals[i].Foods != null && days[0].Meals[i].Foods.Count > 0)
                {
					firstFood = days[0].Meals[i].Foods[0];
                    break;
                }//end if
            }//end for

            //No foods found
            if (firstFood == null)
                return string.Empty;

            //List nutrients in header
            if (firstFood.Nutrients != null)
            {
                foreach (INutrient nutrient in firstFood.Nutrients)
                    sb.AppendFormat(",{0}", nutrient.Name);
            }//end if
            sb.AppendLine();

            //List meals
			foreach (ITrackableDay day in days)
			{
				foreach (IMeal meal in day.Meals)
				{
					foreach (IFood food in meal.Foods)
					{
						sb.AppendFormat("{0}, {1}, {2}, {3}", day.Date.Date.ToShortDateString(), meal.Name, food.Name.Replace(",", " "), food.CaloricContent);
						if (food.Nutrients != null)
						{
							foreach (INutrient nutrient in food.Nutrients)
								sb.AppendFormat(",{0}", nutrient.Amount);
						}//end if
						sb.AppendLine();
					}//end foreach
				}//end foreach
			}//end foreach
            
            return sb.ToString();
        }
    }
}
