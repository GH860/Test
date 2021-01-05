using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static readonly String url = "https://www.google.com";
        static void Main(string[] args)
        {
            // Search a text here
            WriteMessage("Enter a name");

            // Create a string variable and get user input from the keyboard and store it in the variable
            string userInput = Console.ReadLine();

            // initialize a WebDriver instance
            IWebDriver driver = new ChromeDriver();

            // load google search page
            driver.Navigate().GoToUrl(url);

            // print title
            WriteMessage($"Page title: {driver.Title}");

            // enter search word and submit
            IWebElement element = driver.FindElement(By.Name("q"));
            element.SendKeys(userInput);
            element.Submit();

            //create list of data
            IList<IWebElement> rcElementList = driver.FindElements(By.ClassName("rc"));
            List<SearchResultModel> dataModels = GetSearchResultModelList(rcElementList);

            WriteMessage($"done:  {dataModels.Count}");

        }

        //get list of results model
        static List<SearchResultModel> GetSearchResultModelList(IList<IWebElement> webElementList)
        {
            List<SearchResultModel> searchResultModelList = new List<SearchResultModel>();

            foreach (IWebElement webElement in webElementList)
            {
                SearchResultModel searchResultModel = ExtractSearchResultModel(webElement);
                searchResultModelList.Add(searchResultModel);

                WriteMessage($"dataModel : \n link : { searchResultModel.Link} \n title :  { searchResultModel.Title}");
                WriteMessage("\n --------------------------------------------------------------------");
            }

            return searchResultModelList;
        }


        //extract link and title from element
        static SearchResultModel ExtractSearchResultModel(IWebElement webElement)
        {
            SearchResultModel searchResultModel = new SearchResultModel();
            try
            {
                IWebElement web = webElement.FindElement(By.TagName("a"));

                searchResultModel.Link = web.GetAttribute("href");
                searchResultModel.Title = webElement.Text;
            }
            catch (NotFoundException ex)
            {
                Debug.WriteLine(ex.ToString());

                searchResultModel.Link = "";
                searchResultModel.Title = "";
            }

            return searchResultModel;
        }

        //print String
        static void WriteMessage(String value)
        {
            Console.WriteLine(value);
        }
    }
}