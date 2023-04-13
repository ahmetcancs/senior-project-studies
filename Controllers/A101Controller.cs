using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace seniorproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class A101Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //url will be dynamic
            var url = "https://www.a101.com.tr/list/?search_text=k%C3%B6pek%20mamas%C4%B1&personaclick_search_query=k%C3%B6pek%20mamas%C4%B1&personaclick_input_query=k%C3%B6pek%20mamas%C4%B1";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.row ul.product-list-general li.col-md-4.col-sm-6.col-xs-6.set-product-item article.product-card.js-product-wrapper");


            var data = nodes.Select((node) =>
            {
                return new
                {
                    productName = node.QuerySelector("a").GetAttributeValue("title", null),
                    productUrl = "https://www.a101.com.tr/" + node.QuerySelector("a").GetAttributeValue("href", null),
                    productImageUrl = node.QuerySelector("img.lazyload").GetAttributeValue("data-src", null),
                    productPrice = node.QuerySelector("section.prices span.current").InnerText
                };
            });
            return Ok(data);
        }
    }
}

