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
    public class UserGroupsController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/UserGroups
        public IEnumerable<UserGroup> GetUserGroups()
        {
            return db.UserGroups.AsEnumerable();
        }

        // GET api/UserGroups/5
        public UserGroup GetUserGroup(int id)
        {
            UserGroup usergroup = db.UserGroups.Find(id);
            if (usergroup == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return usergroup;
        }

        // PUT api/UserGroups/5
        public HttpResponseMessage PutUserGroup(int id, UserGroup usergroup)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != usergroup.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(usergroup).State = EntityState.Modified;

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

        // POST api/UserGroups
        public HttpResponseMessage PostUserGroup(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
                db.UserGroups.Add(usergroup);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, usergroup);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = usergroup.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/UserGroups/5
        public HttpResponseMessage DeleteUserGroup(int id)
        {
            UserGroup usergroup = db.UserGroups.Find(id);
            if (usergroup == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserGroups.Remove(usergroup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, usergroup);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}