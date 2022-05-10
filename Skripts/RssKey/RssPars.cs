using System.Diagnostics;
using System.Xml;
using NewsRESTapi.Models;

namespace NewsRESTapi.Skripts.RssKey
{
    public static class RssPars
    {
        /// <summary>
        /// Метод принимает коллекцию объектов типа RssKey, получает XML по Rss ключам  и
        /// обходим все узлы элемента ,  сохраняет в объект типа (News) ,после добавляет его в лист объектов типа (News) и
        /// отдает его в ответ .
        /// </summary>
        /// <param name="rssList"></param>
        /// <returns></returns>
        public static List<News> Pars(Models.RssKey rssList)
        {
            List<News> newsList = new List<News>();

            if (rssList.Rss == null)
            {
                Debug.Fail($"RssPars::Pars::22  rssList.Rss == null");
                return newsList;
            }

            foreach (string rss in rssList.Rss)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement? xRoot = null;
                try
                {
                    xmlDoc.Load(rss);
                    xRoot = xmlDoc.DocumentElement;
                }
                catch (Exception ex)
                {
                    Debug.Fail($"RssAdd::Pars::37 {ex}");
                    return newsList;
                }

                if (xRoot != null)
                {
                    foreach (XmlElement xnode in xRoot)
                    {
                        News newsName = new();

                        foreach (XmlNode childnode in xnode.ChildNodes) // обходим все узлы элемента
                        {
                            News news = new();
                            switch (childnode.Name)
                            {
                                case "item":
                                    foreach (XmlNode _childnode in childnode.ChildNodes) // обходим все дочерние узлы элемента
                                    {
                                        switch (_childnode.Name)
                                        {
                                            case "title":
                                                news.Title = _childnode.InnerText;
                                                break;
                                            case "description":
                                                news.Description = _childnode.InnerText;
                                                break;
                                            case "link":
                                                news.Link = _childnode.InnerText;
                                                break;
                                            case "pubDate":
                                                news.LastBuildDate = _childnode.InnerText;
                                                break;
                                        }
                                    }

                                    break;
                                default:
                                    switch (childnode.Name)
                                    {
                                        case "title":
                                            newsName.Title = childnode.InnerText;
                                            break;
                                        case "description":
                                            newsName.Description = childnode.InnerText;
                                            break;
                                        case "link":
                                            newsName.Link = childnode.InnerText;
                                            break;
                                        case "lastBuildDate":
                                            newsName.LastBuildDate = childnode.InnerText;
                                            break;
                                    };
                                    break;
                            };

                            if (childnode.Name == "item")
                            {
                                newsList.Add(news);
                            }
                        }

                        newsList.Add(newsName);
                    }
                }
            }

            return newsList;
        }
    }
}