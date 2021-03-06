﻿using System;
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

        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<List_Tasks>(json);
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                if (requestData.start_date != null)
                {
                    requestData.start_date = ((DateTime)requestData.start_date).AddDays(1);
                }
                if (requestData.end_date != null)
                {
                    requestData.end_date = ((DateTime)requestData.end_date).AddDays(1);
                }
                response.Content = new ObjectContent<List_Tasks>(requestData, new JsonMediaTypeFormatter());
                ent.List_Tasks.Add(requestData);
                ent.SaveChanges();
                return response;
            }
        }

        public HttpResponseMessage Put(HttpRequestMessage request)
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<List_Tasks>(json);
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                var obj = ent.List_Tasks.FirstOrDefault(task => task.id == requestData.id);
                if(obj.status == "open")
                {
                    obj.status = "doing";
                }else if(obj.status == "doing")
                {
                    obj.status = "done";
                }
                ent.SaveChanges();
                return response;
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            using (TaskManagerEntities ent = new TaskManagerEntities())
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                var obj = ent.List_Tasks.FirstOrDefault(task => task.id == id);
                ent.List_Tasks.Remove(obj);
                ent.SaveChanges();
                return response;
            }
        }

    }
}
