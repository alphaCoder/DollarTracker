using System;
using System.Web.Mvc;

namespace DollarTracker.Web.Tests.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
