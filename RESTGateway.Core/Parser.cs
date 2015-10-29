using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTGateway.Core
{   
    public class Field
    {
        public string Selector { get; set; }
    }
    public class ParserRules
    {
        public ParserRules()
        {
            Fields = new List<Field>();
        }

        public List<Field> Fields { get; private set; }
    }
    public class Parser
    {
        public Parser()
        {
            
        }

        public List<Dictionary<string, object>> Parse(IRestResponse<dynamic> responseToParse, ParserRules ruleset)
        {
            if (responseToParse.ResponseStatus != ResponseStatus.Completed)
                throw new ApplicationException("Response was not [Completed]");

            if (responseToParse.Data == null)
                throw new ApplicationException("Response data could not be parsed");

            var resultset = responseToParse.Data as IEnumerable<dynamic>;

            if (resultset == null)
                throw new ApplicationException("Response data could not be identified as collection");
            List<Dictionary<string, object>> l = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> item in resultset)
            {
                var newItem = new Dictionary<string, object>();
                foreach (var field in ruleset.Fields)
                {
                    if (item.ContainsKey(field.Selector))
                    {
                        newItem.Add(field.Selector, item[field.Selector]);
                    }
                }

                if (newItem.Any())
                    l.Add(newItem);

            }

            return l;
        }



    }
}
