using SessionApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SessionApi.Controllers
{
    public class SessionController: ApiController
    {
        private DB_A71A17_userEntity db = new DB_A71A17_userEntity();

        //POST: api/Session
        public LoginSession PostLogin(LoginBody user)
        {
            LoginSession res = new LoginSession();

            IQueryable<user> us = from x in db.user
                                  where x.email.Equals(user.email) && x.pass.Equals(user.pass)
                                  select x;

            List < user > usList = us.ToList();

            if (usList.Count() > 0)
            {
                res.code = 0;
                res.message = "Completado";
                res.id = usList.ElementAt(0).id;
                return res;
            }
            else
            {
                res.code = 1;
                res.message = "Credenciales no validas";
                return res;
            }
        }

        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}