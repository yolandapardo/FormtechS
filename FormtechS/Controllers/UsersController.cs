using FormtechS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FormtechS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        // GET: Users
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {

            var users = (from u in context.Users
                         select new UsersRole()
                         {
                             Id=u.Id,
                             UserName = u.UserName,
                             Email = u.Email,
                             Roles = (from ur in u.Roles
                                      join r in context.Roles on ur.RoleId
                                      equals r.Id
                                      select r.Name).ToList()
                         }).ToList();
        
            return View(users);
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user= (from u in context.Users
                       where u.Id==id
                       select new UsersRole()
                       {
                           Id = u.Id,
                           UserName = u.UserName,
                           Email = u.Email,
                           Roles = (from ur in u.Roles
                                    join r in context.Roles on ur.RoleId
                                    equals r.Id
                                    select r.Name).ToList()
                       }).FirstOrDefault();
           
            ViewBag.Roles = context.Roles;
            
            return View(user);
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsersRole ur)
        {
            if (ModelState.IsValid)
            {

                var uM = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var list = uM.GetRoles(ur.Id);
                foreach (var i in list)
                {
                    uM.RemoveFromRole(ur.Id, i);
                }
                foreach (var i in ur.Roles)
                {
                    uM.AddToRole(ur.Id, i);
                }
                return RedirectToAction("Index");

            }
            return View(ur);

        }


        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var uR = (from u in context.Users
                      where u.Id == id
                      select new UsersRole()
                      {
                          UserName = u.UserName,
                          Email = u.Email,
                          Roles = (from r in context.Roles
                                   join us in u.Roles
                                   on r.Id equals us.RoleId
                                   select r.Name).ToList()
                      }).FirstOrDefault();

            return View(uR);
        }

        // POST: Counties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}