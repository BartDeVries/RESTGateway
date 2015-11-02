using RESTGateway.Core;
using RestSharp;
using System.Collections.Generic;
using Xunit;

namespace RESTGateway.Tests
{
    
    public class FetcherTests
    {
        [Fact]
        public void TestMethod1()
        {
            
            Fetcher f = new Fetcher();
            var result = f.Execute<List<Post>>("http://jsonplaceholder.typicode.com/posts", new RestRequest());
        }


        [Fact]
        public void TestMethod2()
        {

            Fetcher f = new Fetcher();
            var result = f.Execute(new RestRequest());
        }

        [Fact]
        public void TestMethod3()
        {

            Fetcher f = new Fetcher();
            var result = f.Execute<dynamic>("http://jsonplaceholder.typicode.com/posts", new RestRequest());
            
            //var resultset = result.Data as IEnumerable<dynamic>;
            var ruleSet = new ParserRules();
            ruleSet.Fields.Add(new Field { Selector = "id" });
            Parser p = new Parser();
            var parseResult = p.Parse(result, ruleSet);

            var serialized = SimpleJson.SerializeObject(parseResult);
        }
    }

    public class Post
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }


}
