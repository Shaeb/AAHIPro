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
    public class RolesController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/Roles
        public IEnumerable<Role> GetRoles()
        {
            return db.Roles.AsEnumerable();
        }

        // GET api/Roles/5
        public Role GetRole(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return role;
        }

        // PUT api/Roles/5
        public HttpResponseMessage PutRole(int id, Role role)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != role.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(role).State = EntityState.Modified;

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

        // POST api/Roles
        public HttpResponseMessage PostRole(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, role);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = role.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Roles/5
        public HttpResponseMessage DeleteRole(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Roles.Remove(role);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, role);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}