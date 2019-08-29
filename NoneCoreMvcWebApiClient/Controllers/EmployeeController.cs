using Newtonsoft.Json.Linq;
using NoneCoreMvcWebApiClient.Infrastructure;
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
    [AuthorizationActionFilter]
    public class EmployeeController : Controller
    {

        private const string webapiUri = "http://webapiservices.com/";
        // GET: Employee
   
        //R => Read employee
        public async Task<ActionResult> GetEmployees()
        {
            IEnumerable<Employee> employees = new List<Employee>();

            string token = getSessionToken();
           

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(webapiUri);
                //setup the header with token
                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);

                //HTTP GET
                var responseTask = await client.GetAsync("api/employee");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<IEnumerable<Employee>>();
                    employees = readTask;
                }

                return View(employees);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string token = getSessionToken();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(webapiUri);
                client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", token);
                //Http Get
                var getTask = await client.GetAsync("api/employee/" + id.ToString());

                if (getTask.IsSuccessStatusCode)
                {
                    var readTask = await getTask.Content.ReadAsAsync<Employee>();

                    employee = readTask;

                    return View(employee);
                }
                else
                {
                    ModelState.AddModelError("", $"Process fail status Code is {getTask.StatusCode}");
                    return View(new Employee());
                }

            }
        }


        //U => update employees
        [HttpPost]
        public async Task<ActionResult> Update(Employee employee)
        {
            string token = getSessionToken();
           
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(webapiUri);

                    SetupHttpClient(client, token);

                    var putTask = await client.PutAsJsonAsync<Employee>($"api/employee/{employee.id}", employee);
                    if(putTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetEmployees");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Error in processing the request {putTask.StatusCode}");
                        return View(employee);
                    }

                }
                
            }
            else
            {
                return View(employee);
            }

        }

       [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            string token = getSessionToken();
          
            if(id <=0)
            {
                TempData["invalID"] = "Invalid Id";
                return RedirectToAction("GetEmployees");
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(webapiUri);
                SetupHttpClient(client, token);
                var deleteItemTask = await client.DeleteAsync($"api/employee/{id}");
                if(deleteItemTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetEmployees");
                }
                else
                {
                    TempData["message"] = $"Error in processing your request {deleteItemTask.StatusCode}";

                }

            }

            return RedirectToAction("GetEmployees");

        }

        [HttpGet]
        public ActionResult Create() => View(new Employee());


        //C => Create employee
        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
            string token = getSessionToken();
            if (ModelState.IsValid)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(webapiUri);

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage postTask = await client.PostAsJsonAsync<Employee>("api/employee", employee);
                    //HTTP POST
                    // var postTask = await client.PostAsJsonAsync<Employee>("api/employee", employee);

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetEmployees");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Fail in process request status code {postTask.StatusCode}");

                    }
                }
            }

            return View(employee);


        }

        private string getSessionToken()
        {
            string token = string.Empty;
            if (Session["access_token"] != null)
            {
                token = Session["access_token"].ToString();
            }
            return token;
        }


        private void SetupHttpClient(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}