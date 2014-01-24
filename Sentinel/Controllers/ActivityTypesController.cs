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
    public class ActivityTypesController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/ActivityTypes
        public IEnumerable<ActivityType> GetActivityTypes()
        {
            return db.ActivityTypes.AsEnumerable();
        }

        // GET api/ActivityTypes/5
        public ActivityType GetActivityType(int id)
        {
            ActivityType activitytype = db.ActivityTypes.Find(id);
            if (activitytype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return activitytype;
        }

        // PUT api/ActivityTypes/5
        public HttpResponseMessage PutActivityType(int id, ActivityType activitytype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != activitytype.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(activitytype).State = EntityState.Modified;

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

        // POST api/ActivityTypes
        public HttpResponseMessage PostActivityType(ActivityType activitytype)
        {
            if (ModelState.IsValid)
            {
                db.ActivityTypes.Add(activitytype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, activitytype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = activitytype.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ActivityTypes/5
        public HttpResponseMessage DeleteActivityType(int id)
        {
            ActivityType activitytype = db.ActivityTypes.Find(id);
            if (activitytype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ActivityTypes.Remove(activitytype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, activitytype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}