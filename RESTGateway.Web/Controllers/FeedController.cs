using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RESTGateway.Core.Entities;
using RESTGateway.Core.Managers;
using Microsoft.Framework.OptionsModel;
using RESTGateway.Core;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace RESTGateway.Web.Controllers
{
    [Route("api/[controller]")]
    public class FeedController : Controller
    {
        //static readonly List<FeedEntity> _items = new List<FeedEntity>()
        //{
        //    new FeedEntity { Id = 1, Name = "First Item" }
        //};

        public FeedController(IOptions<AppSettings> optionsAccessor)
        {
            Settings = optionsAccessor.Value;
        }

        AppSettings Settings { get; }


        // GET: api/values
        [HttpGet]
        public IEnumerable<FeedEntity> Get()
        {
            FeedManager cm = new FeedManager(Settings);

            return cm.GetAll().Result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            FeedManager cm = new FeedManager(Settings);
            var item = cm.GetAll(rowKey: id).Result.SingleOrDefault(); // _items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]FeedEntity item)
        {

            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;
            }
            else
            {
                item.Id = Guid.NewGuid();
                item.UserId = Guid.Empty;

                FeedManager cm = new FeedManager(Settings);
                var res = cm.Save(item);

                res.Wait();

                string url = Url.RouteUrl("Get", new { id = item.Id },
                    Request.Scheme, Request.Host.ToUriComponent());

                HttpContext.Response.StatusCode = 201;
                HttpContext.Response.Headers["Location"] = url;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]FeedEntity item)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;
            }
            else
            {
                FeedManager cm = new FeedManager(Settings);
                var res = cm.Save(item);

                res.Wait();

                string url = Url.RouteUrl("Get", new { id = item.Id },
                    Request.Scheme, Request.Host.ToUriComponent());

                HttpContext.Response.StatusCode = 201;
                HttpContext.Response.Headers["Location"] = url;
            }
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var item = _items.FirstOrDefault(x => x.Id == id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    _items.Remove(item);
        //    return new HttpStatusCodeResult(204); // 201 No Content
        //}
    }
}
