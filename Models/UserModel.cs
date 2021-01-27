using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TestWebApplication.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string gender { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string city { get; set; }

        public IRestResponse UserRegistration(UserModel content)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["WEBAPIURL"]);
            var request = new RestRequest("user/CreateUser", Method.POST);
            request.AddJsonBody(new { UserName = content.UserName, email_id = content.Email, Password = content.Password, gender = content.gender, phone_number = content.phoneNumber, city = content.city });
            IRestResponse res = client.Execute(request);
            Console.WriteLine(res);
            return res;
        }
        public IRestResponse LoginDetails(UserLogin uObj)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["WEBAPIURL"]);
            var request = new RestRequest("user/UserLogin", Method.POST);
            request.AddJsonBody(new { UserName = uObj.UserName, Password = uObj.Password });
            IRestResponse res = client.Execute(request);
            Console.WriteLine(res);
            return res;
        }
    }
    public class UserLogin
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}