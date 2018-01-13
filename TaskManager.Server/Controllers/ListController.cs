using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagerDBA;
using System.Net.Http.Formatting;
using System.Web.Http.Cors;

namespace TaskManager.Server.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ListController : ApiController
    {
        // get all lists
        public HttpResponseMessage Get()
        {
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.Content = new ObjectContent<List<List>>(ent.Lists.ToList(), new JsonMediaTypeFormatter());
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }
        //
        public HttpResponseMessage Get(int id)
        {
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.Content = new ObjectContent<List<List>>(ent.Lists.Where(e => e.id == id).ToList(), new JsonMediaTypeFormatter());
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }

        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<List>(json);
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<List>(requestData, new JsonMediaTypeFormatter());
                ent.Lists.Add(requestData);
                    ent.SaveChanges();
                    return response;
            }
        }


    }
}
