using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;


namespace WebDongHo.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        DBDongHoDataContext db = new DBDongHoDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.Users.ToList());
        }
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var user = from us in db.Users where us.MaKH == id select us;
                return View(user.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            {
                User user = db.Users.Where(n => n.MaKH == id).SingleOrDefault();
                db.Users.DeleteOnSubmit(user);
                db.SubmitChanges();
                return RedirectToAction("Index", "TaiKhoan");
            }
        }
    }
}