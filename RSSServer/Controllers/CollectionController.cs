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
    [Route("api/collection")]
    public class CollectionController : Controller
    {
        private CollectionServices collectionServices;
        public CollectionController()
        {
            collectionServices = new CollectionServices();
        }

        [HttpPost, Route("find")]
        public List<Collection> FindList([FromBody]User user)
        {
            return collectionServices.FindCollections(user);
        }
        [HttpPost, Route("create")]
        public IActionResult CreateCollection([FromBody]Collection collection)
        {
            collectionServices.CreateCollection(collection);
            return new OkResult();
        }
        [HttpPost, Route("delete")]
        public IActionResult DeleteCollection([FromBody]Collection collection)
        {
            collectionServices.DeleteCollection(collection);
            return new OkResult();
        }
        [HttpPost, Route("update")]
        public IActionResult UpdateCollection([FromBody]Collection collection)
        {
            collectionServices.UpdateCollection(collection);
            return new OkResult();
        }
    }
}