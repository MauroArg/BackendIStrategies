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
    public class AccountController : ApiController
    {
        private DB_A71A17_accountEntity db = new DB_A71A17_accountEntity();

        // GET: api/account
        public IQueryable<account> Getaccount()
        {
            return db.account;
        }

        // GET: api/account/5
        [ResponseType(typeof(account))]
        public IHttpActionResult Getaccount(long id)
        {
            account account = db.account.Find(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/account/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putaccount(long id, account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.account_id)
            {
                return BadRequest();
            }

            db.Entry(account).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!accountExists(id))
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

        // POST: api/account
        [ResponseType(typeof(account))]
        public IHttpActionResult Postaccount(account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.account.Add(account);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = account.account_id }, account);
        }

        // DELETE: api/account/5
        [ResponseType(typeof(account))]
        public IHttpActionResult Deleteaccount(long id)
        {
            account account = db.account.Find(id);
            if (account == null)
            {
                return NotFound();
            }

            db.account.Remove(account);
            db.SaveChanges();

            return Ok(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool accountExists(long id)
        {
            return db.account.Count(e => e.account_id == id) > 0;
        }
    }
}