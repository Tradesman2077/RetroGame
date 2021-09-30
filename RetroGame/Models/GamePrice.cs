using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace RetroGame.Models
{
    public static class GamePrice
    {
        public static string Conversion(string dollars)
        {
            ///conversion rate 
            double POUNDSTODOLLARS = 0.74;

            double pounds;
            string stringPounds = "";
            dollars = dollars.Substring(1);


            pounds = double.Parse(dollars);
            ///convert
            pounds = Math.Round(pounds * POUNDSTODOLLARS, 2);

            stringPounds = pounds.ToString("0.00");

            return stringPounds;
        }

        public static async Task<string> GetHtmlAsyncPriceChart(string title, string platform)
        {
            var url = "https://www.pricecharting.com/game/pal-" + platform.Replace(' ', '-').ToLower() + "/" + title.Replace(' ', '-').ToLower();

            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);


            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var productsHtml = htmlDocument.DocumentNode.Descendants("td")
                .Where(node => node.GetAttributeValue("id", "")
                    .Equals("complete_price")).ToList();

            var completePrice = productsHtml[0].Descendants("span")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("price js-price")).ToList();

            var minusChange = productsHtml[0].Descendants("span")
                .Where(node => node.GetAttributeValue("Title", "").Equals("dollar change from last update")).ToList();

            var changeInPrice = productsHtml[0].Descendants("span")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("js-price")).ToList();

            //check if game has gone up or down in value
            var change = minusChange[0].InnerHtml.ToString();
            string changeAmount = changeInPrice[0].InnerHtml.ToString();

            if (change.Contains("&#43;"))
            {
                changeAmount = "+£" + Conversion(changeAmount);
            }
            else
            {
                changeAmount = "-£" + Conversion(changeAmount);
            }
            return "Complete Price : " +"£"+ Conversion(completePrice[0].InnerHtml.Trim()) + " Change in price : " + changeAmount;
        }
    }
}
