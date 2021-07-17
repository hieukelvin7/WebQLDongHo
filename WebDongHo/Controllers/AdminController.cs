using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;
using System.IO;
namespace WebDongHo.Controllers
{
    public class AdminController : Controller
    {

        DBDongHoDataContext data = new DBDongHoDataContext();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        public ActionResult QLSanPham(int? page)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.SanPhams.ToList().OrderBy(n => n.MaSanPham).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // gắn các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["username"];
            var matkhau = collection["pass"];


            //gắn giá trị cho đối tượng được tạo mới (ad)
            Admin ad = data.Admins.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
            if (ad != null)
            {
                Session["TaiKhoanAdmin"] = ad;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ThongBao = " Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ThemSP()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.MaDanhMuc = new SelectList(data.DanhMucSanPhams.ToList().OrderBy(n => n.TenDanhMuc), "MaDanhMuc", "TenDanhMuc");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSP(SanPham sp, HttpPostedFileBase[] files)
        {
            ViewBag.MaDanhMuc = new SelectList(data.DanhMucSanPhams.ToList().OrderBy(n => n.TenDanhMuc), "MaDanhMuc", "TenDanhMuc");
            //if (fileupload == null)
            //{
            //    ViewBag.Thongbao = "Vui lòng chọn ảnh";
            //    return View();
            //}
            ////them vào csdl
            //else
            //{
            //    if (ModelState.IsValid)
            //    {
            //        //Luu ten file
            //        var fileName = Path.GetFileName(fileupload.FileName);
            //        //Luu duong dan cua file
            //        var path = Path.Combine(Server.MapPath("~/images"), fileName);
            //        // kiem tra hinh anh ton tai chua?
            //        if (System.IO.File.Exists(path))
            //        {
            //            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
            //        }
            //        else
            //        {
            //            fileupload.SaveAs(path);
            //        }

            //    }

            //    sp.ImgUrl = "/images/" + fileName;
            //    sp.ImgUrl1 = "/images/" + fileName;
            //    sp.ImgUrl2 = "/images/" + fileName;
            //    data.SanPhams.InsertOnSubmit(sp);
            //    data.SubmitChanges();
            //    return RedirectToAction("QLSanPham");
            //}

            if (files.Count() != 3)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                int count = 1;
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null || file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);

                        string path = Path.Combine(Server.MapPath("/images/"), _FileName);
                        if (System.IO.File.Exists(path))
                        {
                            //nếu hình ảnh đã tồn tại, thì xóa ảnh cũ, cập nhật lại ảnh mới
                            System.IO.File.Delete(path);
                            file.SaveAs(path);
                        }
                        else
                        {
                            file.SaveAs(path);
                        }

                        if (count == 1)
                            sp.ImgUrl = "/images/" + _FileName;
                        if (count == 2)
                            sp.ImgUrl1 = "/images/" + _FileName;
                        if (count == 3)
                            sp.ImgUrl2 = "/images/" + _FileName;

                        count++;
                    }
                }

                data.SanPhams.InsertOnSubmit(sp);
                data.SubmitChanges();

            }


            return RedirectToAction("QLSanPham");

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSanPham == id);
            ViewBag.Masanpham = sp.MaSanPham;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteSP(int id)
        {
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSanPham == id);
            ViewBag.Masanpham = sp.MaSanPham;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SanPhams.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("QLSanPham");
        }
        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSanPham == id);
            ViewBag.Masanpham = sp.MaSanPham;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSanPham == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDanhMuc = new SelectList(data.DanhMucSanPhams.ToList().OrderBy(n => n.TenDanhMuc), "MaDanhMuc", "TenDanhMuc", sp.MaDanhMuc);
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase[] files)
        {
            ViewBag.MaDanhMuc = new SelectList(data.DanhMucSanPhams.ToList().OrderBy(n => n.TenDanhMuc), "MaDanhMuc", "TenDanhMuc");

            SanPham record = (from p in data.SanPhams
                              where p.MaSanPham == sp.MaSanPham
                              select p).SingleOrDefault();

            if (record != null)
            {
                if (files.Count() == 3) //=3 đồng nghĩa với việc người dùng chọn 3 ảnh -> thì mình sẽ set lại 3 ảnh đó db.
                {
                    int count = 1;
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file != null || file.ContentLength > 0)
                        {
                            string _FileName = Path.GetFileName(file.FileName);

                            string path = Path.Combine(Server.MapPath("/images/"), _FileName);
                            if (System.IO.File.Exists(path))
                            {
                                //nếu hình ảnh đã tồn tại, thì xóa ảnh cũ, cập nhật lại ảnh mới
                                System.IO.File.Delete(path);
                                file.SaveAs(path);
                            }
                            else
                            {
                                file.SaveAs(path);
                            }

                            if (count == 1)
                                record.ImgUrl = "/images/" + _FileName;
                            if (count == 2)
                                record.ImgUrl1 = "/images/" + _FileName;
                            if (count == 3)
                                record.ImgUrl2 = "/images/" + _FileName;

                            count++;
                        }
                    }
                }

                record.TenSanPham = sp.TenSanPham;
                record.MoTaNgan = sp.MoTaNgan;
                record.MoTaChiTiet = sp.MoTaChiTiet;
                UpdateModel(record);
                data.SubmitChanges();
            }
            return RedirectToAction("QLSanPham");
        }
    }
}