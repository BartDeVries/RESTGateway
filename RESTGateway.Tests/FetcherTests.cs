﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTGateway.Core;
using RestSharp;
using System.Collections.Generic;

namespace RESTGateway.Tests
{
    public class Post
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
    [TestClass]
    public class FetcherTests
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            
            Fetcher f = new Fetcher();
            var result = f.Execute<List<Post>>("http://jsonplaceholder.typicode.com/posts", new RestRequest());
        }


        [TestMethod]
        public void TestMethod2()
        {

            Fetcher f = new Fetcher();
            var result = f.Execute(new RestRequest());
        }

        [TestMethod]
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
}
