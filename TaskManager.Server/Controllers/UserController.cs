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
    public class UserController : ApiController
    {

        public HttpResponseMessage Get()
        {
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.Content = new ObjectContent<List<User>>(ent.Users.ToList(), new JsonMediaTypeFormatter());
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }


        public HttpResponseMessage Get(int id)
        {
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.Content = new ObjectContent<User>(ent.Users.FirstOrDefault(e => e.id == id), new JsonMediaTypeFormatter());
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }

        public HttpResponseMessage Post(HttpRequestMessage request)///
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.BadRequest;
                var user = ent.Users.FirstOrDefault(e => e.email == requestData.email);
                if (user != null)
                {
                    response.Content = new StringContent("Email already exists in the system");
                    return response;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new ObjectContent<User>(requestData, new JsonMediaTypeFormatter());
                    ent.Users.Add(requestData);
                    ent.SaveChanges();
                    return response;
                }

            }
        }

        public HttpResponseMessage Put(HttpRequestMessage request)
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                var obj = ent.Users.FirstOrDefault(user => user.id == requestData.id);
                obj.firstname = requestData.firstname;
                obj.lastname = requestData.lastname;
                if (requestData.password != null && requestData.password.Length > 0)
                    obj.password = requestData.password;
                response.Content = new ObjectContent<User>(obj, new JsonMediaTypeFormatter());
                ent.SaveChanges();
                return response;
            }
        }

    }
}
