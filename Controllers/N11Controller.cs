using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace seniorproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class N11Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {   //url will be dynamic
            var url = "https://www.n11.com/arama?q=araba";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.QuerySelectorAll("ul.list-ul li.column ");

            var data = nodes.Select((node) =>
            {
                return new
                {
                    productName = node.QuerySelector("h3.productName").InnerText,
                    productUrl = node.QuerySelector("a").GetAttributeValue("href", null),
                    productImageUrl = node.QuerySelector("img").GetAttributeValue("data-original", null),
                    productRating = node.QuerySelector("div.ratingCont span.ratingText").InnerText,
                    productPrice = node.QuerySelector("ins").InnerText
                };
            });
            return Ok(data);
        }
    }
}
