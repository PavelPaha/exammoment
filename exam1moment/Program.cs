using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Xml.XPath;

namespace Exam1Moment
{
    internal class Program
    {
        public static int TestsCount;
        public static void Main(string[] args)
        {
            Console.WriteLine(@"Введите путь до папки пользователя бразура Chrome\n (путь выглядит примерно так: C:\Users\vasil\AppData\Local\Google\Chrome\User Data)");
            //TODO: автоматизировать поиск директории браузера
            var pathToBrowser = Console.ReadLine();
            
            Console.WriteLine("Введите ссылку на тест:");
            var href = Console.ReadLine();
            
            Console.WriteLine("Сколько тестов вы хотите решить?");
            TestsCount = int.Parse(Console.ReadLine());
            
            var pathToFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\";
            var browserOptions = new ChromeOptions();
            browserOptions.AddArgument($"user-data-dir={pathToBrowser}");
            browserOptions.AddArgument("--log-level=3");
            //browserOptions.AddArgument("window-size=1000,900");

            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            
            Console.WriteLine("Браузер открывается...");
            var driver = new ChromeDriver(pathToFile, browserOptions);
            DoNTests(driver, href);
        }
        
        public static void DoNTests(ChromeDriver driver, string href)
        {
            try
            {
                OpenTest(driver, href);
            }
            catch
            {
                Console.WriteLine("Введена неверная ссылка");
                return;
            }
            
            for (var i = 0; i < TestsCount; i++)
            {
                SolveTest(driver);
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//*[@class=\"float-right\"]")).Click();
                Thread.Sleep(100);
                Console.WriteLine($"Тест {i+1} выполнен.\n\n");
 
            }
            
            Console.WriteLine($"Выполнено {TestsCount} тестов.\n Нажмите Enter, чтобы завершить программу");
            Console.ReadLine();
        }


        private static void OpenTest(ChromeDriver driver, string href)
        {
            driver.Navigate().GoToUrl(href);
            // User.AutorizeUser();
            Console.WriteLine("Введите email и пароль в браузере и нажмите здесь Enter");
            Console.ReadLine();
            // driver.FindElement(By.XPath("//*[@id=\"userNameInput\"]")).SendKeys(User.Email);
            // driver.FindElement(By.XPath("//*[@id=\"passwordInput\"]")).SendKeys(User.Password);
            driver.FindElement(
                    By.XPath("//*[@id=\"submitButton\"]")).Click();
        }
        

        private static void SolveTest(ChromeDriver driver)
        {
            driver.FindElement(By.XPath("//*[@class=\"btn btn-secondary\"]")).Click();
            
            while (true)
            {
                Thread.Sleep(2000);
                var questionCase = driver.FindElements(
                    By.XPath("//*[@class=\"ss-btn icon fa fa-magic fa-fw\"]"));
                Thread.Sleep(100);
                foreach (var item in questionCase)
                {
                    item.Click();
                    Thread.Sleep(100);
                    var suggest = driver.FindElement(By.XPath("//*[@id=\"page-mod-quiz-attempt\"]/ul/li[1]"));
                    suggest.Click();
                    Thread.Sleep(100);
                    var syncshareButton =
                        suggest.FindElement(By.XPath("ul/li/span"));
                    syncshareButton.Click();
                    Console.Write(".");
                    Thread.Sleep(100);
                }

                Thread.Sleep(100);
                var nextQuestion = driver.FindElements(By.XPath("//*[@name=\"next\"]"));
                if (nextQuestion.Count > 0)
                    nextQuestion[0].Click();
                else
                {
                    var exitFromTestButton = driver.FindElements(By.XPath("//*[@class=\"btn btn-secondary\"]"));
                    exitFromTestButton.Last().Click();
                    Thread.Sleep(500);
                    var exitConfirm =
                        driver.FindElements(By.XPath("//*[@class=\"btn btn-primary\"]"));
                    if (exitConfirm.Count>0)
                        exitConfirm.Last().Click();
                    break;
                }
            }
        }
    }
}