using HtmlAgilityPack;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Task01
{
    class RateReader
    {
        private async Task<string> GetHtmlContent()
        {
            var tableContent = string.Empty;
            var client = new HttpClient();
            var fileRequest = await client.GetAsync("http://www.nbg.ge/rss.php");
            var fileContent = await fileRequest.Content.ReadAsStringAsync();

            var tmpFile = Path.GetTempFileName();
            File.WriteAllText(tmpFile, fileContent);

            using (var xmlReader = XmlReader.Create(tmpFile, new XmlReaderSettings() { Async = true }))
            {
                var parser = new RssParser();
                var feedReader = new RssFeedReader(xmlReader, parser);
                while (feedReader.Read().Result)
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        var content = feedReader.ReadContent().Result;
                        var item = parser.CreateItem(content);

                        tableContent = item.Description;
                        break;
                    }
                }
            }
            return tableContent;
        }

        public async Task<decimal> GetRate(string cur1, string cur2)
        {
            var tableContent = await GetHtmlContent();

            var doc = new HtmlDocument();
            doc.LoadHtml(tableContent);

            var table = doc.DocumentNode.SelectSingleNode("//table");
            var tableRows = table.SelectNodes("tr");
            var cur1T = 1M;
            var cur2T = 1M;

            foreach (var r in tableRows)
            {
                var columns = r.SelectNodes("td");
                if (columns[0].InnerText == cur1)
                {
                    var t = columns[1].InnerText.Split(' ');

                    int cur1A;
                    int.TryParse(t[0], out cur1A);

                    decimal cur1R;
                    decimal.TryParse(columns[2].InnerText, out cur1R);
                    cur1T = cur1R / cur1A;
                }
                else if (columns[0].InnerText == cur2)
                {
                    var t = columns[1].InnerText.Split(' ');

                    int cur2A;
                    int.TryParse(t[0], out cur2A);

                    decimal cur2R;
                    decimal.TryParse(columns[2].InnerText, out cur2R);
                    cur2T = cur2R / cur2A;
                }
            }

            return cur1T / cur2T;
        }
    }
}
