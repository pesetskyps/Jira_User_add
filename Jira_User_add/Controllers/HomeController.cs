using System;
using System.Collections.Generic;
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

        public JsonResult ThrowJsonError(Exception e) { 
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest; Response.StatusDescription = e.Message; 
            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult Index()
        {

            var client = new RestClient("http://localhost:8080/rest/api/2")
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

            Console.WriteLine("dd");
            return View();

        }

        [HttpPost]
        public JsonResult Index(JiraDeveloper newdeveloper, string[] role)
        {
            //newdeveloper = new JiraDeveloper() { name = "Pete Conlan", password = "ttMLC4eg", emailAddress = "username@local", displayName = "pcon", notification = "yes" };
            var client = new RestClient("http://localhost:8080/rest/api/2")
            {
                Authenticator = new HttpBasicAuthenticator("Pavel_Pesetskiy@epam.com", "ttMLC4eg")
            };

            var request = new RestRequest("user", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newdeveloper);

            //var request = new RestRequest("/user", Method.GET);
            //request.AddParameter("username", "Pete Conlan");

            IRestResponse resp = client.Execute(request);
            Regex regex = new Regex(@".*errors.*");
            Match match = regex.Match(resp.Content);
            if (match.Success)
            {
                dynamic errorDetail = JObject.Parse(resp.Content);
                var errormessages = errorDetail.errorMessages;
                var errors = errorDetail.errors;
                var allerrors = errormessages.ToString() + errors.ToString();
                //var response = ThrowJsonError(new Exception(allerrors));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                List<string> errorsdd = new List<string>();
                //..some processing
                errorsdd.Add("Error 1");
                //..some processing
                errorsdd.Add("Error 2");
                return Json(errors);
            }
            Console.WriteLine("dd");
            return Json(new { Message = "bla" }, JsonRequestBehavior.AllowGet); 

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