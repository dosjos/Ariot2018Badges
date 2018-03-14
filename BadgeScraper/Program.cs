using HtmlAgilityPack;
using Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paho.MqttDotnet;
using System.Threading;

namespace BadgeScraper
{
    class BadgeScraper
    {
        static void Main(string[] args)
        {
           
            var bs = new BadgeScraper();
            Start:
            try
            {
               
                Console.WriteLine("Startin badgescraping");
                bs.Run().GetAwaiter().GetResult();
                Console.WriteLine("Done Scraping");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto Start;
            }
            
        }

        private async Task Run()
        {
            BadgeContext db = new BadgeContext();

            ScrapingBrowser sb = new ScrapingBrowser();
            
                // var client = new MqttClient("BadgeScraper.azure-devices.net:8883", "BadgeScraper");      //("mqtt://m23.cloudmqtt.com:13018", "BadgeScraper");



                //client.Connect(new ConnectOption
                //{
                //    Username = @"BadgeScraper.azure-devices.net\BadgeScraper",
                //    Password = @"HostName=BadgeScraper.azure-devices.net;DeviceId=BadgeScraper;SharedAccessKey=IM2URTILcWf15agfm/oFRvcUWtAOK5+eIDiTffdSYao=",
                //});
                //https://github.com/intel-iot-devkit/up-squared-grove-IoT-dev-kit-arduino-create/tree/master/examples/MqttPubAzure
                while (true)
                {

                    WebPage pageresult = sb.NavigateToPage(new Uri("http://ariot.no/Home/Badges"));

                    var badges = pageresult.Html.CssSelect(".ariot-badge");




                    foreach (var badge in badges)
                    {
                        var image = badge.CssSelect(".ariot-badge__img").First(); ;

                        var text = badge.InnerText.Trim().Split('\r');

                        var img = image.InnerHtml.Split('"');

                        var b = new Badge()
                        {
                            Title = text[0].Trim(),
                            Description = text[6].Trim(),
                            Points = int.Parse(text[3].Split(':')[1].Trim()),
                            ImageUrl = "http://ariot.no" + img[3]
                        };

                        if (db.Badges.Count(x => x.Title == b.Title) == 0)
                        {
                           // var res = client.SendMessageAsync("devices/BadgeScraper/badge", new MqttMessage(MqttQoS.ExactlyOnce, b.Title));
                            //if (res == MqttError.Success)
                            //{

                            //}
                            db.Badges.Add(b);
                            db.SaveChanges();
                            Console.WriteLine("New badge: {0} - {1}", b.Title, b.Points);
                        }

                    }
                    Thread.Sleep(30000);
                
            }
            
        }
    }
}
