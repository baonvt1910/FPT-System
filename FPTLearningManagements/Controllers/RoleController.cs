using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FPTLearningManagement.CustomFilters;
using FPTLearningManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace FPTLearningManagement.Controllers
{
    // GET: Role
    public class RoleController : Controller
    {
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        [AuthLog(Roles = "Admin")]
        
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }


        [AuthLog(Roles = "Admin")]
         
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

         
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}