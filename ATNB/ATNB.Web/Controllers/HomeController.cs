using ATNB.Service.Abstractions;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ATNB.Web.Controllers
{
    public class HomeController : Controller
    {
        IAirPlaneService _AirPlaneService;
        IAirPortService _AirPortService;

        public HomeController(IAirPlaneService AirPlaneService, IAirPortService AirPortService)
        {
            _AirPlaneService = AirPlaneService;
            _AirPortService = AirPortService;
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

                //Execute a loop over the rows.
                //Call method ExecuteFileCSV(csvData);
                ExecuteFileCSV(csvData);
            }
            else
            {
                ModelState.AddModelError("File", "This file format is not supported");
                return View();
            }
            return View();
        }

        public void ExecuteFileCSV(string csvData)
        {


            foreach (string row in csvData.Split('\n'))
            {
                SaveToDatabase(row);

            }
        }
        public void SaveToDatabase(string row)
        {
            string id = "";
            if (!string.IsNullOrEmpty(row))
            {
                id = row.Split(',')[0];

                _AirPlaneService.Create(new Model.AirPlane
                {
                    Id = row.Split(',')[0],
                    Model = row.Split(',')[1],
                    AirPlaneType = row.Split(',')[2],
                    CruiseSpeed = double.Parse(row.Split(',')[3]),
                    EmptyWeight = double.Parse(row.Split(',')[4]),
                    MaxTakeoffWeight = double.Parse(row.Split(',')[5]),
                    MinNeededRunwaySize = double.Parse(row.Split(',')[6])
                });
            }
        }
    }
}