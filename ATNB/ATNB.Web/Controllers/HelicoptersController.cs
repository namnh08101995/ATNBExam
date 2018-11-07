using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ATNB.Model;
using ATNB.Service.Abstractions;

namespace ATNB.Web.Controllers
{
    public class HelicoptersController : Controller
    {
        IHelicopterService _HelicopterService;
        IAirPortService _AirPortService;
        public HelicoptersController(IHelicopterService HelicopterService, IAirPortService AirPortService)
        {
            _HelicopterService = HelicopterService;
            _AirPortService = AirPortService;
        }

        // GET: Helicopters
        public ActionResult Index()
        {
            return View(_HelicopterService.GetAll());
        }

        // GET: Helicopters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Helicopter helicopter = _HelicopterService.GetById(id);
            if (helicopter == null)
            {
                return HttpNotFound();
            }
            return View(helicopter);
        }

        // GET: Helicopters/Create
        public ActionResult Create()
        {
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Helicopters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Model,CruiseSpeed,EmptyWeight,MaxTakeoffWeight,Range,AirPortId")] Helicopter helicopter)
        {
            if (ModelState.IsValid)
            {
                _HelicopterService.Create(helicopter);
                return RedirectToAction("Index");
            }

            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", helicopter.AirPortId);
            return View(helicopter);
        }

        // GET: Helicopters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Helicopter helicopter = _HelicopterService.GetById(id);
            if (helicopter == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", helicopter.AirPortId);
            return View(helicopter);
        }

        // POST: Helicopters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Model,CruiseSpeed,EmptyWeight,MaxTakeoffWeight,Range,AirPortId")] Helicopter helicopter)
        {
            if (ModelState.IsValid)
            {
                _HelicopterService.Update(helicopter);
                return RedirectToAction("Index");
            }
            ViewBag.AirPortId = new SelectList(_AirPortService.GetAll(), "Id", "Name", helicopter.AirPortId);
            return View(helicopter);
        }

        // GET: Helicopters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Helicopter helicopter = _HelicopterService.GetById(id);
            if (helicopter == null)
            {
                return HttpNotFound();
            }
            return View(helicopter);
        }

        // POST: Helicopters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Helicopter helicopter = _HelicopterService.GetById(id);
            _HelicopterService.Delete(helicopter);
            return RedirectToAction("Index");
        }
    }
}
