﻿using System;
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
    public class UserProfilesController : ApiController
    {
        private SentinelEntities db = new SentinelEntities();

        // GET api/UserProfiles
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            return db.UserProfiles.AsEnumerable();
        }

        // GET api/UserProfiles/5
        public UserProfile GetUserProfile(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return userprofile;
        }

        // PUT api/UserProfiles/5
        public HttpResponseMessage PutUserProfile(int id, UserProfile userprofile)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != userprofile.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(userprofile).State = EntityState.Modified;

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

        // POST api/UserProfiles
        public HttpResponseMessage PostUserProfile(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userprofile);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userprofile.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/UserProfiles/5
        public HttpResponseMessage DeleteUserProfile(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserProfiles.Remove(userprofile);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userprofile);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}