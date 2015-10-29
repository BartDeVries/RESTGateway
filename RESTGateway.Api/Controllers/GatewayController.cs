using RESTGateway.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTGateway.Api.Controllers
{

    public class GetRequest
    {
        public string Feed { get; set; }

        public List<Field> Fields { get; set; }
    }
    public class GatewayController : ApiController
    {
        // GET api/values/5
        public dynamic Get(string requestId)
        {
            var request = new GetRequest { Feed = "http://jsonplaceholder.typicode.com/posts", Fields=new List<Field>() };
            request.Fields.Add(new Field { Selector = "id" });
            request.Fields.Add(new Field { Selector = "title" });

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
