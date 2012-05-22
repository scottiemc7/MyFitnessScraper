using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MyFitnessScraper;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace MyFitnessScraper_Tests
{
    [TestFixture]
    public class HtmlDataParser_Test
    {
        private readonly string _html;
        public HtmlDataParser_Test() 
        { 
            //Grab our html sample page
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("MyFitnessBuddy_Tests.SampleHTML.txt"))
            using (StreamReader sr = new StreamReader(s))
            {
                _html = sr.ReadToEnd();
            }
        }

        [SetUp]
        public void Init()
        { /* ... */ }

        [TearDown]
        public void Cleanup()
        { /* ... */ }

        [Test]
        public void ParseFoodDiary_ParsesCorrect_Number_Of_Meals()
        {
            HtmlDataParser parser = new HtmlDataParser();

            //Parse out the data
            List<IMeal> meals = parser.ParseFoodDiaryDate(_html, DateTime.Now.Date);

            //Our assertions
            Assert.AreEqual(4, meals.Count);
        }


        [Test]
        public void ParseFoodDiary_Each_Food_Has_Same_Nutrients()
        {
            HtmlDataParser parser = new HtmlDataParser();

            //Parse out the data
            List<IMeal> meals = parser.ParseFoodDiaryDate(_html, DateTime.Now.Date);

            //Our assertions
            List<string> firstNutrientNames = new List<string>();
            foreach (IMeal meal in meals)
            {
                foreach (IFood food in meal.Foods)
                {
                    if (firstNutrientNames.Count == 0)                    
                        firstNutrientNames.AddRange(food.Nutrients.Select(p => p.Name));                    
                    else
                    {
                        Assert.AreEqual(firstNutrientNames, food.Nutrients.Select(p => p.Name));
                    }//end if
                }//end foreach
            }//end foreach
        }
    }
}
