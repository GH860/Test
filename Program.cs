using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
       
        static void Main(string[] args)
        {
            // Search a text here
            Console.WriteLine("Enter a name");

            // Create a string variable and get user input from the keyboard and store it in the variable
            string text = Console.ReadLine();

            // initialize a WebDriver instance
            IWebDriver driver = new ChromeDriver();
            // load google search page
            driver.Navigate().GoToUrl("https://www.google.com");
            // print title
            Console.WriteLine("Page title: " + driver.Title);
            // enter search word and submit
            IWebElement element = driver.FindElement(By.Name("q"));
            element.SendKeys(text);
            element.Submit();

            //get elemets that contains search results
            IList<IWebElement> rcElements = driver.FindElements(By.ClassName("rc"));

            //create list of data
            List<DataModel> dataModels = new List<DataModel>();

            foreach (IWebElement webElement in rcElements)
            {
                DataModel dataModel = extractData(webElement);
                dataModels.Add(dataModel);

                Console.WriteLine("dataModel : \n link : " + dataModel.link + "\n title :  " + dataModel.title);
                Console.WriteLine("\n -------------------------------------------------------------------- \t");
               
            }

            Console.WriteLine("done : " + dataModels.Count);

       
            //extract link and title from element
        DataModel extractData(IWebElement webElement)
            {
                try
                {
                    DataModel dataModel = new DataModel();
                    IWebElement web = webElement.FindElement(By.TagName("a"));
                    dataModel.link = web.GetAttribute("href");
                    dataModel.title = webElement.Text;

                    return dataModel;
                }
                catch
                {
                    return new DataModel("", "");
                }
            }
        }
    }
}
