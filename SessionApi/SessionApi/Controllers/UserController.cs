using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SessionApi.Models;

namespace SessionApi.Controllers
{
    public class UserController : ApiController
    {
        private DB_A71A17_userEntity db = new DB_A71A17_userEntity();

        // GET: api/User
        public GetUsersRes Getuser()
        {
            GetUsersRes res = new GetUsersRes();
            IQueryable < user > us = db.user;
            if(us.Count() > 0)
            {
                res.code = 0;
                res.message = "Completado";
                res.user = us.ToList();
            }
            else
            {
                res.code = 1;
                res.message = "No se encontraron usuarios";
            }

            return res;
        }

        // GET: api/User/5
        [ResponseType(typeof(user))]
        public GetUserRes Getuser(long id)
        {
            GetUserRes res = new GetUserRes();
            user user = db.user.Find(id);
            if (user == null)
            {
                res.code = 1;
                res.message = "No encontrado";
                return res;
            }

            res.code = 0;
            res.message = "Completado";
            res.user = user;
            return res;
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public PutUserRes Putuser(long id, user user)
        {
            PutUserRes res = new PutUserRes();
            if (!ModelState.IsValid)
            {
                res.code = 1;
                res.message = "Usuario no valido";
                return res;
            }

            if (id != user.id)
            {
                res.code = 2;
                res.message = "Usuario y Id enviados no corresponen";
                return res;
            }

            IQueryable<user> us = from x in db.user
                                  where !x.id.Equals(id) && x.email.Equals(user.email)
                                  select x;

            List<user> usList = us.ToList();

            if (usList.Count() > 0)
            {
                res.code = 3;
                res.message = "Email repetido";
                return res;
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
                {
                    res.code = 4;
                    res.message = "Usuario no existe";
                    return res;
                }
                else
                {
                    throw;
                }
            }

            res.code = 0;
            res.message = "Completado";
            return res;
        }

        // POST: api/User
        [ResponseType(typeof(user))]
        public PostUserRes Postuser(user user)
        {
            PostUserRes res = new PostUserRes();
            if (!ModelState.IsValid)
            {
                res.code = 1;
                res.message = "Usuario no valido";
                return res;
            }

            IQueryable<user> us = from x in db.user
                                  where x.email.Equals(user.email)
                                  select x;

            List<user> usList = us.ToList();

            if (usList.Count() > 0)
            {
                res.code = 2;
                res.message = "Email repetido";
                return res;
            }
            else
            {

                try
                {
                    db.user.Add(user);
                    db.SaveChanges();

                    res.code = 0;
                    res.message = "Completado";
                    res.user = user;
                    return res;
                }
                catch (Exception e)
                {
                    res.code = 99;
                    res.message = "Error inesperado";
                    return res;
                }
            }

        }

        // DELETE: api/User/5
        [ResponseType(typeof(user))]
        public DeleteUserRes Deleteuser(long id)
        {
            DeleteUserRes res = new DeleteUserRes();
            user user = db.user.Find(id);
            if (user == null)
            {
                res.code = 3;
                res.message = "Usuario no existe";
                return res;
            }

            db.user.Remove(user);
            db.SaveChanges();

            res.code = 0;
            res.message = "Completado";
            return res;
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(long id)
        {
            return db.user.Count(e => e.id == id) > 0;
        }
    }
}