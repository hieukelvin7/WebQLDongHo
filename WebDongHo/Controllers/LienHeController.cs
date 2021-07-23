using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;

namespace WebDongHo.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        DBDongHoDataContext db = new DBDongHoDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.LienHes.ToList());
            
        }
        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lienhe = from lh in db.LienHes where lh.MaLienHe == id select lh;
                return View(lienhe.SingleOrDefault());
            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lienhe = from lh in db.LienHes where lh.MaLienHe == id select lh;
                return View(lienhe.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            {
                LienHe lh = db.LienHes.Where(n => n.MaLienHe == id).SingleOrDefault();
                db.LienHes.DeleteOnSubmit(lh);
                db.SubmitChanges();
                return RedirectToAction("Index", "LienHe");
            }
        }
    }
}