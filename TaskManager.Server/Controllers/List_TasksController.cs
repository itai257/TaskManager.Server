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
    public class List_TasksController : ApiController
    {
        // get all tasks
        public HttpResponseMessage Get()
        {
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.Content = new ObjectContent<List<List_Tasks>>(ent.List_Tasks.ToList(), new JsonMediaTypeFormatter());
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }

        // get all tasks of list_id == id
        public HttpResponseMessage Get(int id)
        {
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.Content = new ObjectContent<List<List_Tasks>>(ent.List_Tasks.Where(e => e.list_id == id).ToList(), new JsonMediaTypeFormatter());
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }
    }
}
