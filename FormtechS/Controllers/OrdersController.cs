using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormtechS.Models;
using PagedList;


namespace FormtechS.Controllers
{
    public class OrdersController : Controller
    {
        private FormtechDb db = new FormtechDb();

        // GET: Orders
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;
            var orders = db.Orders.Include(o => o.Address).Include(o => o.Client).Include(o => o.Company).Include(o => o.Description).Include(o => o.TypeOfSurvey);
            if (!String.IsNullOrEmpty(searchString))
                orders = orders.Where(o => o.JobNumber.Contains(searchString));
            switch(sortOrder)
            {
                case "name_desc":
                    orders = orders.OrderByDescending(o => o.JobNumber);
                    break;
                case "Date":
                    orders = orders.OrderByDescending(o => o.Fecha);
                    break;
                case "date_desc":
                    orders = orders.OrderBy(o => o.Fecha);
                    break;
                default:
                    orders = orders.OrderBy(o => o.JobNumber);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(orders.ToList().ToPagedList(pageNumber,pageSize));
        }
        public ActionResult AutoComplete(string prefix)
        {
            var JobNumbers = db.Orders.Where(o => o.JobNumber.StartsWith(prefix)).Select(o => o.JobNumber).ToList();
            return Json(JobNumbers, JsonRequestBehavior.AllowGet);
        }

       public ActionResult PrintViewToPdf(int id)
        {
            var report = new ActionAsPdf("Details",new{id=id});
            return report;
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
           
            ViewBag.Comm=string.Empty;
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            ViewBag.TypeOfSurveyId = new SelectList(db.TypeOfSurveys, "Id", "Type");
            ViewBag.StateId= new SelectList(db.States,"Id","Name");
            ViewBag.CountyId = new SelectList(string.Empty,"Id","Name");
            ViewBag.CityId= new SelectList(string.Empty,"Id","Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContactPerson,Precio,Phone,Fecha,JobNumber,ReferenceNumber,Panel,Date,Suffix,FloodZone,Elevation,CompanyId,Client, Address,TypeOfSurveyId,Description,Address.CommunityId")] Order order)
        {
            if (ModelState.IsValid)
            {
                Order ord = new Order();
                ord = db.Orders.Where(o => o.JobNumber == order.JobNumber).FirstOrDefault();
                if (ord == null)
                {
                    Client client = new Client();
                    client = db.Clients.Where(c => c.Name == order.Client.Name && c.Phone == order.Client.Phone).FirstOrDefault();
                    if (client == null)
                    {
                        db.Clients.Add(order.Client);
                    }
                    Address address = new Address();
                    address = db.Addresses.Where(a => a.Name == order.Address.Name && a.ZipCode == order.Address.ZipCode).FirstOrDefault();
                    if (address == null)
                    {
                        db.Addresses.Add(order.Address);
                    }
                    Description description = new Description();
                    description = db.Descriptions.Where(d => d.Lot == order.Description.Lot && d.Block == order.Description.Block && d.Plat == order.Description.Plat && d.Page == order.Description.Page).FirstOrDefault();
                    if (description == null)
                    {
                       db.Descriptions.Add(order.Description);
                    }
                    order.Address = address;
                    order.Client = client;
                    order.Description = description;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Order already exists with the same Job Number.");

            }

            ViewBag.Comm = db.Communities.Where(c => c.CityId == order.Address.Community.CityId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", order.CompanyId);
            ViewBag.TypeOfSurveyId = new SelectList(db.TypeOfSurveys, "Id", "Type", order.TypeOfSurveyId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", order.Address.Community.City.County.StateId);
            ViewBag.CountyId = new SelectList(db.Counties.Where(c => c.StateId == order.Address.Community.City.County.StateId), "Id", "Name", order.Address.Community.City.CountyId);
            ViewBag.CityId = new SelectList(db.Cities.Where(c => c.CountyId == order.Address.Community.City.CountyId), "Id", "Name", order.Address.Community.CityId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
           
            if (order == null)
            {
                return HttpNotFound();
            }

           
            ViewBag.Comm = db.Communities.Where(c => c.CityId == order.Address.Community.CityId);
            //ViewBag.CommunityId = new SelectList(db.Communities.Where(c => c.CityId == order.Address.Community.CityId), "Id", "Name", order.Address.CommunityId);
            
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", order.CompanyId);
            ViewBag.TypeOfSurveyId = new SelectList(db.TypeOfSurveys, "Id", "Type", order.TypeOfSurveyId);
            
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", order.Address.Community.City.County.StateId);

            ViewBag.CountyId = new SelectList(db.Counties.Where(c => c.StateId == order.Address.Community.City.County.StateId), "Id", "Name", order.Address.Community.City.CountyId);

            ViewBag.CityId = new SelectList(db.Cities.Where(c => c.CountyId == order.Address.Community.City.CountyId), "Id", "Name", order.Address.Community.CityId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContactPerson,Precio,Phone,Fecha,JobNumber,ReferenceNumber,Panel,Date,Suffix,FloodZone,Elevation,CompanyId,Client,Address,TypeOfSurveyId,Description")] Order order)
        {
            order.AddressId = order.Address.Id;
            order.DescriptionId = order.Description.Id;
            order.ClientId = order.Client.Id;
            db.Orders.Attach(order);
            
            if (ModelState.IsValid)
            {
                //--------------------ADDRESS----------------------
                var AddressIds = db.Addresses.Where(a => a.Name == order.Address.Name && a.ZipCode == order.Address.ZipCode).Select(a => a.Id).ToList();
                if(AddressIds.Count==0) //si mi nueva address no esta en la db
                {

                    if (db.Orders.Where(o => o.AddressId == order.Address.Id).ToList().Count <= 1) //si mi address vieja no esta en otra orden
                    {
                        db.Entry(order.Address).State = EntityState.Modified;
                        order.AddressId = order.Address.Id;
                    }
                    else
                    {
                        //si otra orden tiene mi vieja address, inserto una nueva address
                        Address a = new Address()
                        {
                            Name = order.Address.Name,
                            ZipCode = order.Address.ZipCode,
                            CommunityId = order.Address.CommunityId
                            
                        };
                        
                        db.Entry(a).State = EntityState.Added;
                        order.Address = a;
                    }
                }
                else if(!AddressIds.Contains(order.Address.Id)) //si mi nueva address esta en la db y no es mi Id
                {
                    order.Address = db.Addresses.Find(AddressIds[0]);
                }

                //-------------------Client--------------------
                var ClientIds = db.Clients.Where(a => a.Name == order.Client.Name && a.Phone == order.Client.Phone).Select(a => a.Id).ToList();
                if (ClientIds.Count == 0) //si mi nuevo client no esta en la db
                {

                    if (db.Orders.Where(o => o.ClientId == order.Client.Id).ToList().Count <= 1) //si mi client viejo no esta en otra orden
                    {
                        db.Entry(order.Client).State = EntityState.Modified;
                        order.ClientId = order.Client.Id;
                    }
                    else
                    {
                        //si otra orden tiene mi vieja address, inserto una nueva address
                        Client c = new Client()
                        {
                            Name = order.Client.Name,
                            Phone = order.Client.Phone,
                            Email = order.Client.Email

                        };

                        db.Entry(c).State = EntityState.Added;
                        order.Client = c;
                    }
                }
                else if (!ClientIds.Contains(order.Client.Id)) //si mi nueva address esta en la db y no es mi Id
                {
                    order.Client = db.Clients.Find(ClientIds[0]);
                }


                //-------------------DESCRIPTION--------------------
                var DescriptionIds = db.Descriptions.Where(a => a.Lot == order.Description.Lot && a.Block == order.Description.Block && a.Plat==order.Description.Plat && a.Page==order.Description.Page).Select(a => a.Id).ToList();
                if (DescriptionIds.Count == 0) //si mi nuevo client no esta en la db
                {

                    if (db.Orders.Where(o => o.DescriptionId == order.Description.Id).ToList().Count <= 1) //si mi client viejo no esta en otra orden
                    {
                        db.Entry(order.Description).State = EntityState.Modified;
                        order.DescriptionId = order.Description.Id;
                    }
                    else
                    {
                        //si otra orden tiene mi vieja address, inserto una nueva address
                        Description d = new Description()
                        {
                            Lot = order.Description.Lot,
                            Block = order.Description.Block,
                            Plat = order.Description.Plat,
                            Page = order.Description.Page

                        };

                        db.Entry(d).State = EntityState.Added;
                        order.Description = d;
                    }
                }
                else if (!DescriptionIds.Contains(order.Description.Id)) //si mi nueva address esta en la db y no es mi Id
                {
                    order.Description = db.Descriptions.Find(DescriptionIds[0]);
                }
                                
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Name", order.Address.Id);
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", order.Client.Id);
            //ViewBag.CommunityId = new SelectList(db.Communities, "Id", "Name", order.CommunityId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", order.CompanyId);
            ViewBag.DescriptionId = new SelectList(db.Descriptions, "Id", "Lot", order.Description.Id);
            ViewBag.TypeOfSurveyId = new SelectList(db.TypeOfSurveys, "Id", "Type", order.TypeOfSurveyId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
