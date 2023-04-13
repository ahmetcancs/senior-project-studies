using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;


namespace seniorproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrendyolController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //url will be dynamic
            var searchQuery = "ceptelefonu";
            var url = "https://www.trendyol.com/sr?q=" + searchQuery;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.p-card-chldrn-cntnr.card-border");

            var data = nodes.Select((node) =>
            {
                var Brand = node.QuerySelector("span.prdct-desc-cntnr-ttl").InnerText;
                return new
                {
                    productName = Brand + " " + node.QuerySelector("a span.prdct-desc-cntnr-name.hasRatings").GetAttributeValue("title", null),
                    productUrl = "https://www.trendyol.com" + node.QuerySelector("a").GetAttributeValue("href", null),
                };
            });
            return Ok(data);
        }
    }
}

