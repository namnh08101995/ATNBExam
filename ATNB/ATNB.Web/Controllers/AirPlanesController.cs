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

        // GET: AirPlanes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPlane airPlane = _AirPlaneService.GetById(id);
            if (airPlane == null)
            {
                return HttpNotFound();
            }
            return View(airPlane);
        }

        // GET: AirPlanes/Create
        public ActionResult Create()
        {
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name");
            ViewBag.AirPlaneType = new SelectList(PlaneTypes, "TypeCode", "TypeName");
            return View();
        }

        // POST: AirPlanes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Model,AirPlaneType,CruiseSpeed,EmptyWeight,MaxTakeoffWeight,MinNeededRunwaySize,AirPortId")] AirPlane airPlane)
        {
            if (ModelState.IsValid)
            {
                _AirPlaneService.Create(airPlane);
                return RedirectToAction("Index");
            }
            ViewBag.AirPlaneType = new SelectList(PlaneTypes, "TypeCode", "TypeName");
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", airPlane.AirPortId);
            return View(airPlane);
        }

        // GET: AirPlanes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPlane airPlane = _AirPlaneService.GetById(id);
            if (airPlane == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", airPlane.AirPortId);
            return View(airPlane);
        }

        // POST: AirPlanes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Model,AirPlaneType,CruiseSpeed,EmptyWeight,MaxTakeoffWeight,MinNeededRunwaySize,AirPortId")] AirPlane airPlane)
        {
            if (ModelState.IsValid)
            {
                _AirPlaneService.Update(airPlane);
                return RedirectToAction("Index");
            }
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", airPlane.AirPortId);
            return View(airPlane);
        }

        // GET: AirPlanes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPlane airPlane = _AirPlaneService.GetById(id);
            if (airPlane == null)
            {
                return HttpNotFound();
            }
            return View(airPlane);
        }

        // POST: AirPlanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AirPlane airPlane = _AirPlaneService.GetById(id);
            _AirPlaneService.Delete(airPlane);
            return RedirectToAction("Index");
        }
    }
}
