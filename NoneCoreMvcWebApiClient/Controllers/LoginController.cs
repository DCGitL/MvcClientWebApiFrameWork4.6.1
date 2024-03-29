﻿using Newtonsoft.Json.Linq;
using NoneCoreMvcWebApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NoneCoreMvcWebApiClient.Controllers
{
    public class LoginController : Controller
    {
        private const string webapiUri = "http://webapiservices.com/";
        public ActionResult Index() => View(new UserInfo());


        [HttpPost]
        public async Task<ActionResult> Login(UserInfo user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(webapiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //setup login data
                var username = user.UserName;
                var password = user.Password;
                var formContent = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("grant_type", "password"),
                     new KeyValuePair<string, string>("username", username),
                     new KeyValuePair<string, string>("password", password),
                 });
                //send request
                HttpResponseMessage responseMessage = await client.PostAsync("/Token", formContent);
                //get access token from response body
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(responseJson);
                    var token = jObject.GetValue("access_token").ToString();
                    Session["access_token"] = token;

                    ViewBag.token = token;
                    return RedirectToAction("GetEmployees","Employee");
                }
                else
                {
                    ViewBag.statusCode = responseMessage.StatusCode;
                }


                return View(user);

            }

        }

    }
}