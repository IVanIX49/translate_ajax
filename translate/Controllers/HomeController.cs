using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using translate.Models;
using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace translate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //Get
        /*public async Task<IActionResult> Index(string content_text = null, string from_lan = null, string to_lan = null )
        {
            Task<string> task = PostResponse(content_text,from_lan,to_lan);
            string result = await task;
            ViewBag.Name = result;

            return View();
        }
        */

        [HttpPost]
        public async Task<JsonResult> SendText(string text, string from_lan, string to_lan)
        {

            Task<string> task = PostResponse(text, from_lan, to_lan);
            string result = await task;
            
            return Json(result);
        }



        [HttpPost]
        public IActionResult Submit(string content,string from_lan, string to_lan)
        {
            // Ïåðåíàïðàâëÿåì îáðàòíî íà ñòðàíèöó ñ ïåðåäàííûì èìåíåì
            return RedirectToAction("Test", new { content , from_lan, to_lan });
        }

        public async Task<IActionResult> Test()
        {
            //Task<string> task = PostResponse(text, from_lan, to_lan);
            //string result = await task;
            //ViewBag.Content = result;


            return View();
        }



        public async Task<string> PostResponse(string _content_text, string from_lan, string to_lan)
        {
            string content_text = _content_text;
            Dictionary<string, string> data = new Dictionary<string, string> ();
            data.Add("text", content_text);
            data.Add("from_lan", from_lan);
            data.Add("to_lan", to_lan);

            var jsonData = JsonConvert.SerializeObject(data);

            using (HttpClient client = new HttpClient()) 
            {   
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://31.31.207.6:80/api/translate", content);
                

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data_text = JsonConvert.DeserializeObject(responseBody);
                    var context_text = data_text.translated_text;
                    await Task.Delay(1000); // Èìèòàöèÿ çàäåðæêè â 1 ñåêóíäó
                    return context_text;
                }
                else
                {
                    Console.WriteLine("Îøèáêà: " + response.StatusCode);
                    await Task.Delay(1000); // Èìèòàöèÿ çàäåðæêè â 1 ñåêóíäó
                    return "Îæèäàíèå...";
                }
                
            }
    }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
