using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Jira_User_add.Classes;
using Jira_User_add.Models;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jira_User_add.Controllers
{
    public class HomeController : Controller
    {
        private string jiraUrl = "http://localhost:8080/rest/api/2";

        //var request = new RestRequest("/user", Method.GET);
        //request.AddParameter("username", "Pete Conlan");
        //newdeveloper = new JiraDeveloper() { name = "Pete Conlan", password = "ttMLC4eg", emailAddress = "username@local", displayName = "pcon", notification = "yes" };
        public string AnalyzeResponse(IRestResponse response)
        {
            Regex regex = new Regex(@".*errors.*");
            Match match = regex.Match(response.Content);
            string errors = string.Empty;
            if (match.Success)
            {
                dynamic errorDetail = JObject.Parse(response.Content);
                var jsonErrormessages = errorDetail.errorMessages;
                var jsonErrors = errorDetail.errors;
                var allJsonErrors = jsonErrormessages.ToString() + jsonErrors.ToString();
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errors = allJsonErrors;
            }
            return errors;
        }

        public string AddUserToJiraGroup(JiraDeveloper newdeveloper, string role, IRestClient client)
        {
            var requestBody = string.Format("group/user?groupname={0}", role);
            var request = new RestRequest(requestBody, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                name = newdeveloper.name
            });
            var resp = client.Execute(request);
            return AnalyzeResponse(resp);

        }

        //dummy change 1222252fff22dddfdf
        //ddfsdfsfasdfasdf
        //first commit
        //second commit
        public ActionResult Index()
        {

            var vv = Directory.GetDirectories(@"\\EVBYMINSD246C\Upload_Cache");
            var client = new RestClient(jiraUrl)
            {
                Authenticator = new HttpBasicAuthenticator("Pavel_Pesetskiy@epam.com", "ttMLC4eg")
            };

            //var request = new RestRequest("group/user?groupname=jira-developers", Method.POST);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(new
            //{
            //    name = "Pete Conlan"
            //});

            //var request = new RestRequest("/user", Method.GET);
            //request.AddParameter("username", "Pete Conlan");

            //IRestResponse resp = client.Execute(request);

            //Regex regex = new Regex(@".*errors.*");
            //Match match = regex.Match(resp.Content);
            //if (match.Success)
            //{
            //    dynamic errorDetail = JObject.Parse(resp.Content);
            //    var error = errorDetail.errorMessages;
            //    return ThrowJsonError(new Exception(error.ToString()));
            //}

            return View();

        }

        [HttpPost]
        public JsonResult Index(JiraDeveloper newdeveloper, string[] roles)
        {
            var errors = string.Empty;
            var client = new RestClient(jiraUrl)
            {
                Authenticator = new HttpBasicAuthenticator("Pavel_Pesetskiy@epam.com", "ttMLC4eg")
            };

            var request = new RestRequest("user", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newdeveloper);
            var resp = client.Execute(request);
            errors += AnalyzeResponse(resp);
            
            //adding roles
            if ((roles != null) && newdeveloper != null)
            {
                foreach (var role in roles)
                {
                    errors += AddUserToJiraGroup(newdeveloper, role, client);
                }
            }

            if (errors == string.Empty)
            {
                return Json(new { Message = "User successfully added." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = errors}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}