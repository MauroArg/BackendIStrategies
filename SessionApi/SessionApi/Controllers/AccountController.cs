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

        //GET: api/accountByUser
        [Route("api/AccountByUser")]
        public GetAccountsRes GetAccountsByUser(long id, long user)
        {
            GetAccountsRes res = new GetAccountsRes();

            IQueryable<account> ac = from x in db.account
                                     where x.user_id.Equals(id)
                                     select x;

            List<account> acList = ac.ToList();

            if (acList.Count() > 0)
            {
                res.code = 0;
                res.message = "Completado";
                res.account = acList;
                return res;
            }
            else
            {
                res.code = 1;
                res.message = "Este usuario no tiene cuentas";
                return res;
            }

        }

        // GET: api/account
        public GetAccountsRes Getaccount()
        {
            GetAccountsRes res = new GetAccountsRes();

            IQueryable<account> ac = db.account;

            if(ac.Count() > 0)
            {
                res.code = 0;
                res.message = "Completado";
                res.account = ac.ToList();
            }
            else
            {
                res.code = 1;
                res.message = "No se encontraron cuentas";
            }

            return res;
        }

        // GET: api/account/5
        [ResponseType(typeof(account))]
        public GetAccountRes Getaccount(long id)
        {
            GetAccountRes res = new GetAccountRes();
            account account = db.account.Find(id);
            if (account == null)
            {
                res.code = 1;
                res.message = "No encontrado";
                return res;
            }

            res.code = 0;
            res.message = "Completado";
            res.account = account;
            return res;
        }

        // PUT: api/account/5
        [ResponseType(typeof(void))]
        public PutAccountRes Putaccount(long id, account account)
        {
            PutAccountRes res = new PutAccountRes();
            if (!ModelState.IsValid)
            {
                res.code = 1;
                res.message = "Cuenta no valida";
                return res;
            }

            if (id != account.account_id)
            {
                res.code = 2;
                res.message = "Cuenta y Id enviados no corresponen";
                return res;
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
                    res.code = 3;
                    res.message = "Cuenta no existe";
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

        // POST: api/account
        [ResponseType(typeof(account))]
        public PostAccountRes Postaccount(account account)
        {
            PostAccountRes res = new PostAccountRes();
            if (!ModelState.IsValid)
            {
                res.code = 1;
                res.message = "Cuenta no valida";
                return res;
            }

            db.account.Add(account);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                res.code = 2;
                res.message = "Usuario no valido";
                return res;
            }

            res.code = 0;
            res.message = "Completado";
            res.account = account;
            return res;
        }

        // DELETE: api/account/5
        [ResponseType(typeof(account))]
        public DeleteAccountRes Deleteaccount(long id, long user)
        {
            DeleteAccountRes res = new DeleteAccountRes();

            account account = db.account.Find(id);
            if (account == null)
            {
                res.code = 3;
                res.message = "Cuenta no existe";
                return res;
            }

            db.account.Remove(account);
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

        private bool accountExists(long id)
        {
            return db.account.Count(e => e.account_id == id) > 0;
        }
    }
}