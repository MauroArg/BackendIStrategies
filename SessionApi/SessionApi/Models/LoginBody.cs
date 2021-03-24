using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SessionApi.Models
{
    public class LoginBody
    {
        public string email { get; set; }
        public string pass { get; set; }
    }
}