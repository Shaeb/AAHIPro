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
    public class UserApplicationsController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/UserApplications
        public IEnumerable<UserApplication> GetUserApplications()
        {
            return db.UserApplications.AsEnumerable();
        }

        // GET api/UserApplications/5
        public UserApplication GetUserApplication(int id)
        {
            UserApplication userapplication = db.UserApplications.Find(id);
            if (userapplication == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return userapplication;
        }

        // PUT api/UserApplications/5
        public HttpResponseMessage PutUserApplication(int id, UserApplication userapplication)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != userapplication.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(userapplication).State = EntityState.Modified;

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

        // POST api/UserApplications
        public HttpResponseMessage PostUserApplication(UserApplication userapplication)
        {
            if (ModelState.IsValid)
            {
                db.UserApplications.Add(userapplication);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userapplication);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userapplication.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/UserApplications/5
        public HttpResponseMessage DeleteUserApplication(int id)
        {
            UserApplication userapplication = db.UserApplications.Find(id);
            if (userapplication == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserApplications.Remove(userapplication);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userapplication);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}