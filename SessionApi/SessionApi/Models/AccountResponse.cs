using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SessionApi.Models
{
    public class AccountResponse
    {
    }

    public class GetAccountRes
    {
        public int code { get; set; }
        public string message { get; set; }
        public account account { get; set; }
    }

    public class GetAccountsRes
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<account> account { get; set; }
    }

    public class PutAccountRes
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class PostAccountRes
    {
        public int code { get; set; }
        public string message { get; set; }
        public account account { get; set; }
    }

    public class DeleteAccountRes
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}