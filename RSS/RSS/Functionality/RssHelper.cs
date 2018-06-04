using HtmlAgilityPack;
using RSS.Abstract;
using RSS.Entities;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace RSS.Functionality
{
    class RssHelper
    {
        private IRssRepository RssRepository;

        public RssHelper(IRssRepository RssRepository)
        {
            this.RssRepository = RssRepository;
        }

        private void correctTextForXmlCreation(ref string uncorrectText)
        {
            Regex regex = new Regex(@"<link>\s*\S+\&+\S+\s*<\/link>");
            MatchCollection matches = regex.Matches(uncorrectText);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    string uncorrectLine = match.Value;
                    string correctLine = uncorrectLine.Replace("&", "&amp;");
                    uncorrectText = uncorrectText.Replace(uncorrectLine, correctLine);
                }
            }
        }

        private void clearDataFromHtmlElements(ref string description)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNode div = htmlDoc.CreateElement("div");
            div.InnerHtml = description;
            description = div.InnerText;
            if (description.Contains("Читать дальше →"))
            {
                description = description.Replace("Читать дальше →", " ");
            }
            description = WebUtility.HtmlDecode(description).Trim();
            description = Regex.Replace(description, @"\s+", " ");
        }

        public void ReadXmlFile(string route)
        {
            XmlDocument xDoc = new XmlDocument();
            StreamReader reader = File.OpenText(route);
            string textFromFile;
            textFromFile = reader.ReadToEnd();
            try
            {
                xDoc.LoadXml(textFromFile);
            }
            catch (Exception e)
            {
                correctTextForXmlCreation(ref textFromFile);
                xDoc.LoadXml(textFromFile);
            }
            XmlNode rssChannelRoot = xDoc.SelectSingleNode("rss/channel");
            string sourceName = rssChannelRoot.SelectSingleNode("title").InnerText.Trim();
            string sourceUrl = rssChannelRoot.SelectSingleNode("link").InnerText.Trim();
            RssSource source = RssRepository.Sources.FirstOrDefault(s => s.Name == sourceName && s.Url == sourceUrl);
            if (source == null)
            {
                source = new RssSource();
                source.Name = sourceName;
                source.Url = sourceUrl;
            }
            XmlNodeList itemsNodes = rssChannelRoot.SelectNodes("item");
            int numberOfSavedEntities = 0; 
            foreach (XmlNode itemNode in itemsNodes)
            {
                Novelty item = new Novelty();
                string title = itemNode.SelectSingleNode("title").InnerText.Trim();
                clearDataFromHtmlElements(ref title);
                item.Header = title;
                item.Date = DateTime.Parse(itemNode.SelectSingleNode("pubDate").InnerText.Trim());
                Novelty dbEntry = RssRepository.News.FirstOrDefault(n => n.Date == item.Date && n.Header == item.Header);
                if (dbEntry == null)
                {
                    item.Url = itemNode.SelectSingleNode("guid").InnerText.Trim();
                    string description = itemNode.SelectSingleNode("description").InnerText.Trim();
                    clearDataFromHtmlElements(ref description);
                    item.Description = description;
                    item.Source = source;
                    RssRepository.AddNovelty(item);
                    numberOfSavedEntities++;
                }                
            }
            Console.WriteLine("Источник новостей: {0}", source.Name);
            Console.WriteLine("Прочитано новостей: {0}", itemsNodes.Count);
            Console.WriteLine("Сохранено новостей: {0}", numberOfSavedEntities);
        }
    }
}
