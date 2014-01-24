using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sentinel.Controllers
{
    public class BannedPasswordsController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/BannedPasswords
        public IEnumerable<BannedPassword> GetBannedPasswords()
        {
            return db.BannedPasswords.AsEnumerable();
        }

        // GET api/BannedPasswords/5
        public BannedPassword GetBannedPassword(int id)
        {
            BannedPassword bannedpassword = db.BannedPasswords.Find(id);
            if (bannedpassword == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return bannedpassword;
        }

        // PUT api/BannedPasswords/5
        public HttpResponseMessage PutBannedPassword(int id, BannedPassword bannedpassword)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != bannedpassword.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(bannedpassword).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/BannedPasswords
        public HttpResponseMessage PostBannedPassword(BannedPassword bannedpassword)
        {
            if (ModelState.IsValid)
            {
                db.BannedPasswords.Add(bannedpassword);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, bannedpassword);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = bannedpassword.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/BannedPasswords/5
        public HttpResponseMessage DeleteBannedPassword(int id)
        {
            BannedPassword bannedpassword = db.BannedPasswords.Find(id);
            if (bannedpassword == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BannedPasswords.Remove(bannedpassword);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, bannedpassword);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}