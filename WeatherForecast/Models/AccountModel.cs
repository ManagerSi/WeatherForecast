using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace WeatherForecast.Models
{
    public class LoginModel
    {
        [Required]
        public string Account
        {
            get;
            set;
        }
        [Required]
        public string PassWord
        {
            get;
            set;
        }
    }
}