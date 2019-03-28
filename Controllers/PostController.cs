using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CustomMVC.Models;

namespace CustomMVC.Controllers {

    public class PostController : Controller {

        private Context dbContext;

        public PostController(Context context) {
            dbContext = context;
        }

        public IActionResult Posts() {
            return View();
        }
    }
}
