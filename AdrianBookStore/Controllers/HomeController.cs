﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdrianBookStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to Poor Adrian's Almanac!";
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Inquiry";

            return View();
        }
        
        public ActionResult Store()
        {
            ViewBag.Message = "The Collection";

            return View();
        }
    }
}