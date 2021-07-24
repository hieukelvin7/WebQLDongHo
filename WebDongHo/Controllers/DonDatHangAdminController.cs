using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;

namespace WebDongHo.Controllers
{
    public class DonDatHangAdminController : Controller
    {
        // GET: DonDatHangAdmin
        DBDongHoDataContext db = new DBDongHoDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.DONDATHANGs.ToList());
        }
        public ActionResult ChiTietDatHang()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.CHITIETDONHANGs.ToList());
        }
    }
}