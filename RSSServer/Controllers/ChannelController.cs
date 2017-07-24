using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSServer.Services;
using RSSServer.Models;
using Microsoft.AspNetCore.Authorization;

namespace RSSServer.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/channel")]
    public class ChannelController : Controller
    {

        private ChannelServices channelServices;
        public ChannelController()
        {
            channelServices = new ChannelServices();
        }

        [HttpPost, Route("find")]
        public List<Channel> FindListChannel([FromBody]Collection collection)
        {
            return channelServices.FindChannels(collection);
        }
        [HttpPost, Route("create")]
        public IActionResult CreateChannel([FromBody]Channel channel)
        {
            channelServices.CreateChannel(channel);
            return new OkResult();
        }
        [HttpPost, Route("delete")]
        public IActionResult DeleteChannel([FromBody]Channel channel)
        {
            channelServices.DeleteChannel(channel);
            return new OkResult();
        }
        [HttpPost, Route("update")]
        public IActionResult UpdateChannel([FromBody]Channel channel)
        {
            channelServices.UpdateChannel(channel);
            return new OkResult();
        }
        [HttpPost, Route("loadnews")]
        public Channel LoadNews([FromBody]Channel channel)
        {
            channelServices.LoadNewsAsync(channel).Wait();
            return channel;

        }
    }
}