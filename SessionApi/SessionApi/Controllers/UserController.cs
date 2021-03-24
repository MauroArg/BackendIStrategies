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
        public IQueryable<user> Getuser()
        {
            return db.user;
        }

        // GET: api/User/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Getuser(long id)
        {
            user user = db.user.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser(long id, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User
        [ResponseType(typeof(user))]
        public IHttpActionResult Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.user.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.id }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Deleteuser(long id)
        {
            user user = db.user.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.user.Remove(user);
            db.SaveChanges();

            return Ok(user);
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