using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SessionApi.Models
{
    public class UserResponse
    {
    }

    public class LoginSession
    {
        public int code { get; set; }
        public string message { get; set; }
        public long id { get; set; }
    }

    public class GetUserRes
    {
        public int code { get; set; }
        public string message { get; set; }
        public user user { get; set; }
    }

    public class GetUsersRes
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<user> user { get; set; }
    }

    public class PutUserRes
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class PostUserRes
    {
        public int code { get; set; }
        public string message { get; set; }
        public user user { get; set; }
    }

    public class DeleteUserRes
    {
        public int code { get; set; }
        public string message { get; set; }
    }



}