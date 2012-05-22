﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HtmlAgilityPack;
using System.Diagnostics;

namespace MyFitnessScraper
{
    public class HtmlDataParser
    {
        public List<IMeal> ParseFoodDiaryDate(string data, DateTime date)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);

            HtmlNode mainNode = doc.GetElementbyId("main");
            if (mainNode == null)
                throw new ArgumentException("No node with id of 'main' could be found");

            //All meals are stored in a table, with class = 'table0' and meal headers have the class 'meal_header'
            HtmlNodeCollection mealHeaderNodes = mainNode.SelectNodes("div[@class='container']/table[@class='table0']/tbody/tr[@class='meal_header']");
            HtmlNodeCollection mealDataNodes = mainNode.SelectNodes("div[@class='container']/table[@class='table0']/tbody/tr");
            
            List<IMeal> meals = new List<IMeal>();
            StringBuilder sb = new StringBuilder();

            //TODO: throw an exception here
            if (mealHeaderNodes.Count == 0 || mealDataNodes.Count == 0)
                return meals;

            //Grab nutrient names from first meal header
            List<string> nutrientNames = new List<string>();
            HtmlNodeCollection nutrientTDs = mealHeaderNodes[0].SelectNodes("td[@class='alt']");
            if (nutrientTDs != null)
            {
                for (int i = 1; i < nutrientTDs.Count; i++)//skip over 'Calories'
                    nutrientNames.Add(nutrientTDs[i].InnerText.Trim());
            }//end if

            foreach (HtmlNode mealHeaderNode in mealHeaderNodes)
            {
                foreach (HtmlNode mealNameNode in mealHeaderNode.SelectNodes("td[@class='first alt']"))
                {
                    IMeal meal = new Meal() { Name = mealNameNode.InnerText, Foods = new List<IFood>(), Date = date.Date };

                    //Grab <tr> nodes, until you reach the "bottom" which will be the end of this meal's data rows
                    HtmlNode sibling = mealNameNode.ParentNode.NextSibling;
                    while(sibling != null && (sibling.Attributes["class"] == null || sibling.Attributes["class"].Value != "bottom"))
                    {
                        if (sibling.NodeType == HtmlNodeType.Element && String.Compare(sibling.Name, "tr", true) == 0)
                        {
                            HtmlNodeCollection tds = sibling.SelectNodes("td");

                            //First <td> always seems to be the name, second is calories, and the rest is optional
                            string foodName = tds[0].InnerText.Trim();
                            int calories = Int32.Parse(tds[1].InnerText.Trim());

                            List<INutrient> nutrients = new List<INutrient>();
                            for (int i = 0; i < nutrientNames.Count && i+2 < tds.Count; i++)//skip over 'Calories' and 'Name'
                            {
                                nutrients.Add(new Nutrient() { Name = nutrientNames[i], Amount = Int32.Parse(tds[i+2].InnerText.Trim()) });
                            }//end for

                            meal.Foods.Add(new Food() { CaloricContent = calories, Name = foodName, Nutrients = nutrients });                            
                        }//end if

                        sibling = sibling.NextSibling;
                    }//end while

                    meals.Add(meal);
                }//end foreach
            }//end foreach

            return meals;
        }
    }
}
