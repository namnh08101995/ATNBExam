using System.Web.Mvc;
using ATNB.Model;
using ATNB.Service.Abstractions;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using ATNB.Web.Models;

namespace ATNB.Web.Controllers
{
    public class AirPortsController : Controller
    {
        //Initialize service object
        IAirPlaneService _AirPlaneService;
        IAirPortService _AirPortService;
        IHelicopterService _HelicopterService;

        public AirPortsController(IAirPlaneService AirPlaneService, IAirPortService AirPortService, IHelicopterService HelicopterService)
        {
            _AirPlaneService = AirPlaneService;
            _AirPortService = AirPortService;
            _HelicopterService = HelicopterService;
        }

        //public ViewResult Index()
        //{
        //    return View(_AirPortService.GetAll());
        //}

        // GET: AirPorts
        public ActionResult Index(string id)
        {
            var viewModel = new AirPortIndexData();
            viewModel.AirPorts = _AirPortService.GetAll();

            if (id != null)
            {
                ViewBag.AirPortID = id;
                viewModel.AirPlanes = _AirPlaneService.GetAll().Where(i => i.AirPortId == id);
                viewModel.Helicopters = _HelicopterService.GetAll().Where(i => i.AirPortId == id);
            }

            return View(viewModel);
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

            //Get all airplane with airportId = id
            IEnumerable<AirPlane> airplanes = _AirPlaneService.GetAll().Where(i => i.AirPortId == id);

            //Get all helicopter with airportId = id
            IEnumerable<Helicopter> helicopters = _HelicopterService.GetAll().Where(i => i.AirPortId == id);

            //Remove relationship
            //foreach(var airplane in airplanes)
            //{
            //    airplane.AirPortId = null;
            //    _AirPlaneService.Update(airplane);
            //}
            //foreach (Helicopter helicopter in helicopters)
            //{
            //    helicopter.AirPortId = null;
            //    _HelicopterService.Update(helicopter);
            //}

            //_AirPortService.Delete(airPort);
            return RedirectToAction("Index");
        }
    }
}
