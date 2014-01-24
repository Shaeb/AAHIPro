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
    public class UserRolesController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/UserRoles
        public IEnumerable<UserRole> GetUserRoles()
        {
            return db.UserRoles.AsEnumerable();
        }

        // GET api/UserRoles/5
        public UserRole GetUserRole(int id)
        {
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return userrole;
        }

        // PUT api/UserRoles/5
        public HttpResponseMessage PutUserRole(int id, UserRole userrole)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != userrole.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(userrole).State = EntityState.Modified;

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

        // POST api/UserRoles
        public HttpResponseMessage PostUserRole(UserRole userrole)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userrole);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userrole);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userrole.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/UserRoles/5
        public HttpResponseMessage DeleteUserRole(int id)
        {
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserRoles.Remove(userrole);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userrole);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}