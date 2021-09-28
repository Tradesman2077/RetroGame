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
            var changeAmount = changeInPrice[0].InnerHtml.ToString();
            System.Diagnostics.Debug.WriteLine(change);

            if (change.Contains("&#43;"))
            {

                changeAmount = "+" + changeAmount;
            }
            else
            {
                changeAmount = "-" + changeAmount;
            }



            return "Complete Price : " + completePrice[0].InnerHtml.Trim() + " Change in price : " + changeAmount;
        }
    }
}
