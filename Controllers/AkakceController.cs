using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace seniorproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AkakceController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //url will be dynamic
            var searchQuery = "cep telefonu";
            var url = "https://www.akakce.com/cep-telefonu.html";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.DocumentNode.SelectNodes("//li[@data-pr]");
            var data = nodes.Select((node) =>
            {
                return new
                {
                    productName = node.SelectSingleNode(".//h3[@class='pn_v8']").InnerText.Trim(),
                    productImageUrl = node.SelectSingleNode(".//img[@src]").Attributes["src"].Value.Trim(),
                    productPrice = node.SelectSingleNode(".//span[contains(@class, 'pt_v9')]").InnerText.Trim(),
                    productLink = node.SelectSingleNode(".//a[@class='iC']").Attributes["title"].Value.Trim()
                    //   sellerLinkNode = node.SelectSingleNode(".//a[@class='iG']").Attributes["href"].Value.Trim()
                };
            });


            return Ok(data);
        }
    }
}

