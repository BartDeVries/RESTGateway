using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using RESTGateway.Core;
using RESTGateway.Core.Managers;
using RestSharp;
using System;
using System.Collections.Generic;


namespace RESTGateway.Api.Controllers
{

    public class GetRequest
    {
        public string Feed { get; set; }

        public List<Field> Fields { get; set; }
    }

    [Route("gateway")]
    public class GatewayController : Controller
    {

        public GatewayController(IOptions<AppSettings> optionsAccessor)
        {
            Settings = optionsAccessor.Value;
        }

        AppSettings Settings { get; }

        // GET api/values/5

        [HttpGet("{id}")]
        public dynamic Get(string id)
        {
            FeedManager fm = new FeedManager(Settings);
            var feed = fm.Get(id).Result;


            var request = new GetRequest { Feed = feed.RemoteFeed, Fields=new List<Field>() };
            string[] fields = feed.Fields.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields)
            {
                request.Fields.Add(new Field { Selector = field });
            }
            
            if (string.IsNullOrWhiteSpace(request.Feed))
                throw new ApplicationException("feed param empty");

            if (request.Fields == null)
                throw new ApplicationException("no fields");

            Fetcher f = new Fetcher();
            var result = f.Execute<dynamic>(request.Feed, new RestRequest());

            //var resultset = result.Data as IEnumerable<dynamic>;
            var ruleSet = new ParserRules();
            ruleSet.Fields.AddRange(request.Fields);
            //foreach (var field in fields)
            //{
            //    ruleSet.Fields.Add(new Field { Selector = field });
            //}

            Parser p = new Parser();
            var parseResult = p.Parse(result, ruleSet);
            return parseResult;
            //var serialized = SimpleJson.SerializeObject(parseResult);
            //return "value";
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
