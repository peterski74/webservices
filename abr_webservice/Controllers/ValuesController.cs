using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;

namespace abr_webservice.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [actionfilter.JsonCallback]
        public string Get(string id)
        {
            //return "value: " + id;
            string SearchPayload = "";
            HttpGetSearch Search;
            Search = new HttpGetDocumentSearch();
            SearchPayload = Search.AbnSearch(id, "N", WebConfigurationManager.AppSettings["Guid"]);


            string outputMsg = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(SearchPayload);

            //Display all the book titles.
            XmlNodeList elemList = doc.GetElementsByTagName("organisationName");
            if(elemList.Count > 0) {
                //for (int i = 0; i < elemList.Count; i++)
                outputMsg = elemList[0].InnerXml;
                
            }
            else
            {
                XmlNodeList elemErrList = doc.GetElementsByTagName("exceptionDescription");
                outputMsg = elemErrList[0].InnerXml;
            }


            return outputMsg;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
