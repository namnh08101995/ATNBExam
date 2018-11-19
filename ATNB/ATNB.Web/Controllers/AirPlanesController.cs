using System.Net;
using System.Web.Mvc;
using ATNB.Model;
using ATNB.Service.Abstractions;
using System.Collections.Generic;
using System;

namespace ATNB.Web.Controllers
{
    public class AirPlanesController : Controller
    {
        IAirPlaneService _AirPlaneService;
        IAirPortService _AirPortService;
        IEnumerable<PlaneType> PlaneTypes;

        public AirPlanesController(IAirPlaneService AirPlaneService, IAirPortService AirPortService)
        {
            _AirPlaneService = AirPlaneService;
            _AirPortService = AirPortService;
            PlaneTypes = new List<PlaneType>
            {
                new PlaneType
                {
                    TypeCode = "CAG",
                    TypeName = "Cargo"
                },
                new PlaneType
                {
                    TypeCode = "LGR",
                    TypeName = "Long range"
                },
                new PlaneType
                {
                    TypeCode = "PRV",
                    TypeName = "Private"
                }
            };
        }

        // GET: AirPlanes
        public ActionResult Index()
        {
            return View(_AirPlaneService.GetAll());
        }

        // GET: AirPlanes/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.AirPlaneId = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPlane airPlane = _AirPlaneService.GetById(id);
            if (airPlane == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirPlaneType = new SelectList(PlaneTypes, "TypeCode", "TypeName",airPlane.AirPlaneType);
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", airPlane.AirPortId);
            return View(airPlane);
        }

        // POST: AirPlanes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AirPlane airPlane)
        {
            AirPort airPort = _AirPortService.GetById(airPlane.AirPortId);

            var MinNeededRunwaySize = airPlane.MinNeededRunwaySize;
            var RunwaySize = airPort.RunwaySize;

            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", airPlane.AirPortId);
            ViewBag.AirPlaneType = new SelectList(PlaneTypes, "TypeCode", "TypeName", airPlane.AirPlaneType);

            if (ModelState.IsValid)
            {
                if (MinNeededRunwaySize < RunwaySize)
                {
                    _AirPlaneService.Update(airPlane);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ABC", "Min runway size does not excess the airport runway size: " + RunwaySize);
                    //return View(airPlane);
                }
            }

            return View(airPlane);
        }
    }
}
