using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapCore.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BootstrapCore.Controllers
{
    public class InscriptionController : Controller
    {
        private InscriptionService InscriptionService;
        public InscriptionController(InscriptionService _s)
        {
            InscriptionService = _s;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
