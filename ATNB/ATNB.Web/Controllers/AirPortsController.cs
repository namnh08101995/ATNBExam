using System.Web.Mvc;
using ATNB.Model;
using ATNB.Service.Abstractions;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using ATNB.Web.Models;
using System.IO;

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
            AirPort airport = _AirPortService.GetById(id);
            //Get all airplane with airportId = id
            IEnumerable<AirPlane> airplanes = _AirPlaneService.GetAll().Where(i => i.AirPortId == id);

            //Get all helicopter with airportId = id
            IEnumerable<Helicopter> helicopters = _HelicopterService.GetAll().Where(i => i.AirPortId == id);

            List<string> listIdAirplane = new List<string>();
            List<string> listIdHelicopter = new List<string>();

            //Remove relationship
            foreach (AirPlane airplane in airplanes)
            {
                listIdAirplane.Add(airplane.Id);
            }
            foreach (Helicopter helicopter in helicopters)
            {
                listIdHelicopter.Add(helicopter.Id);
            }

            foreach (string i in listIdHelicopter)
            {
                //do some thing
                Helicopter helicopter = _HelicopterService.GetById(i);
                helicopter.AirPortId = null;
                _HelicopterService.Update(helicopter);
            }
            foreach (string i in listIdAirplane)
            {
                //do some thing
                AirPlane airplane = _AirPlaneService.GetById(i);
                airplane.AirPortId = null;
                _AirPlaneService.Update(airplane);
            }

            _AirPortService.Delete(airport);
            return RedirectToAction("Index");
        }

        // GET: /AirPorts/AddHelicopter/5
        public ActionResult AddHelicopter(string id)
        {
            ViewBag.AirPortId = id;
            IEnumerable<Helicopter> helicopters = _HelicopterService.GetAll().Where(x => x.AirPortId == null);
            if (helicopters == null)
            {
                return HttpNotFound();
            }
            return View(helicopters);
        }

        // POST: /AirPorts/AddHelicopter/5
        public ActionResult AddHelicopterToAirPort(FormCollection frm)
        {
            var AirPortId = Request.Form["airPortId"];
            string listId = Request.Form["listIdCheck"];
            if (listId != null)
            {
                foreach(string id in listId.Split(','))
                {
                    //do some thing
                    Helicopter helicopter = _HelicopterService.GetById(id);
                    helicopter.AirPortId = AirPortId;
                    _HelicopterService.Update(helicopter);
                }
            }
            return RedirectToAction("Index",new { id=AirPortId });
        }

        // GET: /AirPorts/AddAirPlane/5
        public ActionResult AddAirPlane(string id)
        {
            ViewBag.AirPortId = id;
            double? runwaySize = _AirPortService.GetById(id).RunwaySize;
            IEnumerable<AirPlane> airplanes = _AirPlaneService.GetAll().Where(x => x.AirPortId == null && x.MinNeededRunwaySize < runwaySize );
            if (airplanes == null)
            {
                return HttpNotFound();
            }
            return View(airplanes);
        }

        // POST: /AirPorts/AddAirPlane/5
        public ActionResult AddAirPlaneToAirPort(FormCollection frm)
        {
            var AirPortId = Request.Form["airPortId"];
            string listId = Request.Form["listIdCheck"];
            if (listId != null)
            {
                foreach (string id in listId.Split(','))
                {
                    //do some thing
                    AirPlane airplane = _AirPlaneService.GetById(id);
                    airplane.AirPortId = AirPortId;
                    _AirPlaneService.Update(airplane);
                }
            }
            return RedirectToAction("Index", new { id = AirPortId });
        }

        // GET: /AirPorts/RemoveAirPlane/5
        public ActionResult RemoveAirPlane(string id)
        {
            ViewBag.AirPortId = id;
            IEnumerable<AirPlane> airplanes = _AirPlaneService.GetAll().Where(x => x.AirPortId == id);
            if (airplanes == null)
            {
                return HttpNotFound();
            }
            return View(airplanes);
        }

        // POST: /AirPorts/RemoveAirPlane/5
        public ActionResult RemoveAirPlaneFromAirPort(FormCollection frm)
        {
            var AirPortId = Request.Form["airPortId"];
            string listId = Request.Form["listIdCheck"];
            if (listId != null)
            {
                foreach (string id in listId.Split(','))
                {
                    //do some thing
                    AirPlane airplane = _AirPlaneService.GetById(id);
                    airplane.AirPortId = null;
                    _AirPlaneService.Update(airplane);
                }
            }
            return RedirectToAction("Index", new { id = AirPortId });
        }

        // GET: /AirPorts/RemoveHelicopter/5
        public ActionResult RemoveHelicopter(string id)
        {
            ViewBag.AirPortId = id;
            IEnumerable<Helicopter> helicopters = _HelicopterService.GetAll().Where(x => x.AirPortId == id);
            if (helicopters == null)
            {
                return HttpNotFound();
            }
            return View(helicopters);
        }

        // POST: /AirPorts/RemoveHelicopter/5
        public ActionResult RemoveHelicopterFromAirPort(FormCollection frm)
        {
            var AirPortId = Request.Form["airPortId"];
            string listId = Request.Form["listIdCheck"];
            if (listId != null)
            {
                foreach (string id in listId.Split(','))
                {
                    //do some thing
                    Helicopter helicopter = _HelicopterService.GetById(id);
                    helicopter.AirPortId = null;
                    _HelicopterService.Update(helicopter);
                }
            }
            return RedirectToAction("Index", new { id = AirPortId });
        }

        //Export file CSV
        public void ExportCSV(string id)
        {
            AirPort airPort = _AirPortService.GetById(id);
            //Get IEnumerable AirPlane and Helicopter
            IEnumerable<AirPlane> airPlanes = _AirPlaneService.GetAll().Where(i => i.AirPortId == id);
            IEnumerable<Helicopter> helicopters = _HelicopterService.GetAll().Where(i => i.AirPortId == id);

            //Check an airPort is valid
            int numOfAirPlane = 0;
            int numOfHelicopter = 0;

            foreach(var x in airPlanes)
            {
                numOfAirPlane++;
            }
            foreach (var x in helicopters)
            {
                numOfHelicopter++;
            }

            //check airport isvalid.
            if(numOfAirPlane >= 5 && numOfHelicopter >= 10)
            {
                StringWriter sw = new StringWriter();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ExportAirport.csv");
                Response.ContentType = "text/csv";

                sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                    airPort.Id, airPort.Name, airPort.RunwaySize, airPort.MaxFWParkingPlace, airPort.MaxRWParkingPlace));

                foreach(AirPlane airPlane in airPlanes)
                {
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                        airPlane.Id,
                        airPlane.Model,
                        airPlane.AirPlaneType,
                        airPlane.CruiseSpeed,
                        airPlane.EmptyWeight,
                        airPlane.MaxTakeoffWeight,
                        airPlane.MinNeededRunwaySize,
                        airPlane.FlyMethod
                        ));
                }

                foreach (Helicopter helicopter in helicopters)
                {
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6}",
                        helicopter.Id,
                        helicopter.Model,
                        helicopter.CruiseSpeed,
                        helicopter.EmptyWeight,
                        helicopter.MaxTakeoffWeight,
                        helicopter.Range,
                        helicopter.FlyMethod
                        ));
                }

                Response.Write(sw.ToString());
                Response.End();
            }
        }

    }
}
