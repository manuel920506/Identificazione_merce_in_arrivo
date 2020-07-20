using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Magazzino.Models
{
    public class CarrelliController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
