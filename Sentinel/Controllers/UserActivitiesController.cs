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
    public class UserActivitiesController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/UserActivities
        public IEnumerable<UserActivity> GetUserActivities()
        {
            return db.UserActivities.AsEnumerable();
        }

        // GET api/UserActivities/5
        public UserActivity GetUserActivity(int id)
        {
            UserActivity useractivity = db.UserActivities.Find(id);
            if (useractivity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return useractivity;
        }

        // PUT api/UserActivities/5
        public HttpResponseMessage PutUserActivity(int id, UserActivity useractivity)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != useractivity.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(useractivity).State = EntityState.Modified;

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

        // POST api/UserActivities
        public HttpResponseMessage PostUserActivity(UserActivity useractivity)
        {
            if (ModelState.IsValid)
            {
                db.UserActivities.Add(useractivity);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, useractivity);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = useractivity.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/UserActivities/5
        public HttpResponseMessage DeleteUserActivity(int id)
        {
            UserActivity useractivity = db.UserActivities.Find(id);
            if (useractivity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserActivities.Remove(useractivity);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, useractivity);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}