using System.Web.Mvc;
using ATNB.Model;
using ATNB.Service.Abstractions;
using System.Net;

namespace ATNB.Web.Controllers
{
    public class AirPortsController : Controller
    {
        //Initialize service object
        IAirPortService _AirPortService;

        public AirPortsController(IAirPortService AirPortService)
        {
            _AirPortService = AirPortService;
        }

        // GET: AirPorts
        public ActionResult Index()
        {
            return View(_AirPortService.GetAll());
        }

        // GET: AirPorts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPort airPort = _AirPortService.GetById(id);
            if (airPort == null)
            {
                return HttpNotFound();
            }
            return View(airPort);
        }

        // GET: AirPorts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AirPorts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AirPort airPort)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                _AirPortService.Create(airPort);
                return RedirectToAction("Index");
            }

            return View(airPort);
        }

        // GET: AirPorts/Edit/5
        public ActionResult Edit(string id)
        {
            AirPort airPort = _AirPortService.GetById(id);
            if (airPort == null)
            {
                return HttpNotFound();
            }
            return View(airPort);
        }

        // POST: AirPorts/Edit
        [HttpPost]
        public ActionResult Edit(AirPort airPort)
        {
            if (ModelState.IsValid)
            {
                _AirPortService.Update(airPort);
                return RedirectToAction("Index");
            }
            return View(airPort);
        }

        // GET: AirPorts/Delete/5
        public ActionResult Delete(string id)
        {
            AirPort airPort = _AirPortService.GetById(id);
            if (airPort == null)
            {
                return HttpNotFound();
            }
            return View(airPort);
        }

        // POST: AirPorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, FormCollection data)
        {
            AirPort airPort = _AirPortService.GetById(id);
            _AirPortService.Delete(airPort);
            return RedirectToAction("Index");
        }
    }
}
