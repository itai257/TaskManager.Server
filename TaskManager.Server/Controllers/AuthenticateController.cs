using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManagerDBA;

namespace TaskManager.Server.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AuthenticateController : ApiController
    {
        public HttpResponseMessage Get(int id, HttpResponseMessage request)
        {
            //MainAccess main = new MainAccess();
            //var idString = request.Headers.GetValues("token").First();
            //var response = new HttpResponseMessage();
            //if (id != int.Parse(idString))
            //{
            //    response.Content = new StringContent("the token is not correct");
            //    response.StatusCode = HttpStatusCode.Conflict;
            //    return response;
            //}

            //User user = main.GetUser(id);
            //Company company = main.GetCompany(user.companyId);

            //response.Content = new ObjectContent<AuthenticateResponse>(new AuthenticateResponse(user, company), new JsonMediaTypeFormatter());

            //response.StatusCode = HttpStatusCode.OK;
            //return response;

            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // POST api/values 
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            //MainAccess main = new MainAccess();
            //var json = request.Content.ReadAsStringAsync().Result;
            //var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
            //var response = new HttpResponseMessage();
            //User user;

            //try
            //{
            //    user = main.authenticateUser(requestData.username, requestData.password);
            //}
            //catch (ArgumentOutOfRangeException e)
            //{
            //    response.StatusCode = HttpStatusCode.BadRequest;
            //    if (e.ParamName == "username")
            //    {
            //        response.Content = new StringContent("שם משתמש או סיסמא שגויים");
            //    }
            //    else if (e.ParamName == "password")
            //    {
            //        response.Content = new StringContent("שם משתמש או סיסמא שגויים");
            //    }
            //    return response;
            //}

            //Company company = main.GetCompany(user.companyId);

            //response.Content = new ObjectContent<AuthenticateResponse>(new AuthenticateResponse(user, company), new JsonMediaTypeFormatter());

            //return response;
            //return new HttpResponseMessage(HttpStatusCode.NotFound);
            var json = request.Content.ReadAsStringAsync().Result;
            var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.BadRequest;
                //response.Content = new ObjectContent<User>(ent.Users.FirstOrDefault(e => e.email == requestData.email), new JsonMediaTypeFormatter());
                var user = ent.Users.FirstOrDefault(e => e.email == requestData.email);
                if (user == null)
                {
                    response.Content = new StringContent("No such email here...");
                    return response;
                }else if(user.password != requestData.password)
                {
                    response.Content = new StringContent("Wrong Password!!!");
                    return response;
                }else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter());
                    return response;
                }

            }
        }


        }
    }

