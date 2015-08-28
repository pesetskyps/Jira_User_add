using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jira_User_add.Models
{
    public class JiraDeveloper
    {
        public string name { get; set; }
        public string password { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public string notification { get; set; }
    }
}