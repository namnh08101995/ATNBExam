using AirportManagement.DAL;
using AirportManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AirportManagement.Controllers
{
    public class HomeController : Controller
    {
        private AirportContext _airportContext;

        public HomeController()
        {
            _airportContext = new AirportContext();
        }

        public ActionResult Index()
        {
            return View(_airportContext.Airports.ToList());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            ICollection<Airport> airports = new List<Airport>();
            string filePath = string.Empty;
            if (file != null)
            {
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                file.SaveAs(_path);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(_path);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        airports.Add(new Airport
                        {
                            Name = row.Split(',')[0],
                            RunWay = Convert.ToDouble(row.Split(',')[1]),
                            FixedWingParkingPlace = Convert.ToInt32(row.Split(',')[2])
                        });
                    }
                }
            }

            return View(airports);
        }

        public ActionResult EditCSV(int id, string fileName = "airport.csv")
        {
            IList<Airport> airports = new List<Airport>();
            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
            string csvData = System.IO.File.ReadAllText(_path);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    airports.Add(new Airport
                    {
                        Name = row.Split(',')[0],
                        RunWay = Convert.ToDouble(row.Split(',')[1]),
                        FixedWingParkingPlace = Convert.ToInt32(row.Split(',')[2])
                    });
                }
            }

            ViewBag.Id = id;

            return View(airports[id]);
        }

        [HttpPost]
        public ActionResult EditCSV(Airport formData, string fileName = "airport.csv")
        {
            IList<Airport> airports = new List<Airport>();
            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
            string csvData = System.IO.File.ReadAllText(_path);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    airports.Add(new Airport
                    {
                        Name = row.Split(',')[0],
                        RunWay = Convert.ToDouble(row.Split(',')[1]),
                        FixedWingParkingPlace = Convert.ToInt32(row.Split(',')[2])
                    });
                }
            }

            foreach (var row in airports)
            {
                if (row.Name == formData.Name)
                {
                    row.Name = formData.Name;
                    row.RunWay = formData.RunWay;
                    row.FixedWingParkingPlace = formData.FixedWingParkingPlace;
                    break;
                }
            }
            var csv = new StringBuilder();
            foreach (var item in airports)
            {
                var newLine = string.Format($"{item.Name},{item.RunWay},{item.FixedWingParkingPlace}");
                csv.AppendLine(newLine);
            }
            System.IO.File.WriteAllText(_path, csv.ToString());

            // Reload file - Test
            airports = new List<Airport>();
            csvData = System.IO.File.ReadAllText(_path);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    airports.Add(new Airport
                    {
                        Name = row.Split(',')[0],
                        RunWay = Convert.ToDouble(row.Split(',')[1]),
                        FixedWingParkingPlace = Convert.ToInt32(row.Split(',')[2])
                    });
                }
            }
            return View("Index", airports);
        }

            public ActionResult ExportToExcel(string fileName)
        {
            // read ~/UploadedFiles/airport.csv
            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
            string csvData = System.IO.File.ReadAllText(_path);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Airport.csv");
            Response.ContentType = "text/csv";

            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    Response.Write(row);
                }
            }

            Response.End();

            return View("Index");
        }

        public ActionResult Airport()
        {
            return View(_airportContext.Airports.ToList());
        }

        public ActionResult FWAirplane()
        {
            return View(_airportContext.FWAirplanes.ToList());
        }

        public ActionResult Helicopter()
        {
            return View(_airportContext.Helicopters.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}