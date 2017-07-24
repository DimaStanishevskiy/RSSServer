using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSSServer.Models;
using System.Xml;
using System.Net.Http;
using System.Xml.Linq;

namespace RSSServer.Services
{
    public class ChannelServices
    {
        private DataContext context;

        public ChannelServices()
        {
            context = new DataContext();
        }

        public bool CreateChannel(Channel channel)
        {
            context.AttachRange(channel.Collection);
            context.AddRange(channel);
            context.SaveChanges();
            return true;
        }

        public void DeleteChannel(Channel channel)
        {
            context.RemoveRange(channel);
            context.SaveChanges();
        }

        public void UpdateChannel(Channel channel)
        {
            context.Update(channel);
            context.SaveChanges();
        }

        public List<Channel> FindChannels(Collection Collection)
        {
            return context.Channels.Where(x => x.Collection.Id == Collection.Id).ToList();
        }

        public Channel FindChannel(Collection Collection, int Id)
        {
            return context.Channels.FirstOrDefault(x => x.Id == Id && x.Collection == Collection);
        }

        public async Task<bool> LoadNewsAsync(Channel Channel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Channel.Url);
                    var responseMessage = await client.GetAsync(Channel.Url);
                    var responseString = await responseMessage.Content.ReadAsStringAsync();

                    //extract feed items
                    XDocument doc = XDocument.Parse(responseString);
                    string baseElement;
                    string description;
                    //if (doc.Root.Descendants().First(i => i.Name.LocalName == "channel") != null)
                    //{
                    //    baseElement = "channel";
                    //    description = "description";
                    //}
                    //else if (doc.Root.Descendants().First(i => i.Name.LocalName == "feed") != null)
                    //{
                    //    baseElement = "feed";
                    //    description = "	subtitle";
                    //}
                    //else
                    //    return false;
              
                    var feedItems = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                                    select new News
                                    {
                                        Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                        Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                        Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                                    };
                    Channel.News = feedItems.ToList();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
