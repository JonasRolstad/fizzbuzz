using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fizzbuzztemp.Models;
using System.Net;
using System.IO;
using System.Globalization;

namespace fizzbuzztemp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string dataUrl = "https://opencom.no/dataset/58f23dea-ab22-4c68-8c3b-1f602ded6d3e/resource/d9f75b07-ad78-4d86-81a2-4b48db754a06/download/badetemp.csv";
            //  fetch data
            List<TemperatureData> temperatures = GetData(dataUrl);
            // sort by warmest temperature
            var sortedByWarmth = temperatures.OrderByDescending(x => x.Temperature);
            // decorate first item
            // send to View
            return View(sortedByWarmth);
        }

        public static List<TemperatureData> GetData(string url) // TODO: Place in a Service
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            List<TemperatureData> data = new List<TemperatureData>();

            using (StreamReader sr = new StreamReader(resp.GetResponseStream())) 
            {
                sr.ReadLine(); // Ignore first line
                int id = 1;
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    List<string> lineValues = line.Split(',').ToList();
                    // Convert each line into an object
                    TemperatureData obj = new TemperatureData()
                    {
                        Id = id,
                        LocationName = lineValues[0],
                        Temperature = double.Parse(lineValues[1], CultureInfo.InvariantCulture),
                        ReadingTime = lineValues[2],
                        Latitude = double.Parse(lineValues[3], CultureInfo.InvariantCulture),
                        Longitude = double.Parse(lineValues[4], CultureInfo.InvariantCulture)
                    };
                    data.Add(obj);
                    id += 1;
                }
            }
            return data;
        }
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
