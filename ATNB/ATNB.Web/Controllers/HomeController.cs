using ATNB.Service.Abstractions;
using LumenWorks.Framework.IO.Csv;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ATNB.Web.Controllers
{
    public class HomeController : Controller
    {
        IAirPlaneService _AirPlaneService;
        IAirPortService _AirPortService;
        IHelicopterService _HelicopterService;

        public HomeController(IAirPlaneService AirPlaneService, IAirPortService AirPortService, IHelicopterService HelicopterService)
        {
            _AirPlaneService = AirPlaneService;
            _AirPortService = AirPortService;
            _HelicopterService = HelicopterService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;

            if (postedFile == null || postedFile.ContentLength < 1)
            {
                ModelState.AddModelError("File", "Please choose Your file");
                return View();
            }

            if (postedFile.FileName.EndsWith(".csv"))
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Call method ExecuteFileCSV(csvData);
                ExecuteFileCSV(csvData);
            }
            else
            {
                ModelState.AddModelError("File", "This file format is not supported");
                return View();
            }
            ViewBag.Message = "Load file successfull!";
            return View();
        }

        public void ExecuteFileCSV(string csvData)
        {
            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    SaveToDatabase(row);
                }
            }
        }
        public void SaveToDatabase(string row)
        {
            string id = (row.Split(',')[0]).Trim();
            string subId = id.Substring(0, 2);
            switch (subId)
            {
                case "AP":
                    _AirPortService.Create(new Model.AirPort
                    {
                        Id = id,
                        Name = row.Split(',')[1],
                        RunwaySize = int.Parse(row.Split(',')[2]),
                        MaxFWParkingPlace = int.Parse(row.Split(',')[3]),
                        MaxRWParkingPlace = int.Parse(row.Split(',')[5])
                    });

                    string[] listFWId = (row.Split(',')[4]).TrimEnd().Split(' ');
                    foreach (string FWId in listFWId)
                    {
                        Model.AirPlane airplane = _AirPlaneService.GetById(FWId);
                        airplane.AirPortId = id;
                        _AirPlaneService.Update(airplane);
                    }

                    string[] listRWId = (row.Split(',')[6]).TrimEnd().Split(' ');
                    foreach (string RWId in listRWId)
                    {
                        Model.Helicopter helicopter = _HelicopterService.GetById(RWId);
                        helicopter.AirPortId = row.Split(',')[0];
                        _HelicopterService.Update(helicopter);
                    }
                    break;
                case "FW":
                    _AirPlaneService.Create(new Model.AirPlane
                    {
                        Id = id,
                        Model = row.Split(',')[1],
                        AirPlaneType = row.Split(',')[2],
                        CruiseSpeed = double.Parse(row.Split(',')[3]),
                        EmptyWeight = double.Parse(row.Split(',')[4]),
                        MaxTakeoffWeight = double.Parse(row.Split(',')[5]),
                        MinNeededRunwaySize = double.Parse(row.Split(',')[6])
                    });
                    break;
                case "RW":
                    _HelicopterService.Create(new Model.Helicopter
                    {
                        Id = id,
                        Model = row.Split(',')[1],
                        CruiseSpeed = double.Parse(row.Split(',')[2]),
                        EmptyWeight = double.Parse(row.Split(',')[3]),
                        MaxTakeoffWeight = double.Parse(row.Split(',')[4]),
                        Range = double.Parse(row.Split(',')[5])
                    });
                    break;
                default:
                    ViewBag.Message = "Load file false";
                    break;
            }
        }
    }
}