using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;

namespace WebDongHo.Controllers
{
    public class ThuongHieuController : Controller
    {
        // GET: ThuongHieu
        DBDongHoDataContext db = new DBDongHoDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.DanhMucSanPhams.ToList());
        }
        public ActionResult Create()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else          
                return View();
        }
        [HttpPost,ActionName("Create")]
        public ActionResult Them(DanhMucSanPham th)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                db.DanhMucSanPhams.InsertOnSubmit(th);
                db.SubmitChanges();
                return RedirectToAction("Index", "ThuongHieu");
            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var thuonghieu = from th in db.DanhMucSanPhams where th.MaDanhMuc == id select th;
                return View(thuonghieu.SingleOrDefault());
            }
        }
        [HttpPost,ActionName("Edit")]
        public ActionResult Sua(int id)
        {
            {
                DanhMucSanPham dm = db.DanhMucSanPhams.Where(n => n.MaDanhMuc == id).SingleOrDefault();
                UpdateModel(dm);
                db.SubmitChanges();
                return RedirectToAction("Index", "ThuongHieu");
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
                var thuonghieu = from th in db.DanhMucSanPhams where th.MaDanhMuc == id select th;
                return View(thuonghieu.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            {
                DanhMucSanPham dm = db.DanhMucSanPhams.Where(n => n.MaDanhMuc == id).SingleOrDefault();
                db.DanhMucSanPhams.DeleteOnSubmit(dm);
                db.SubmitChanges();
                return RedirectToAction("Index", "ThuongHieu");
            }
        }

    }
}