using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;

namespace WebDongHo.Controllers
{
    public class AdminController : Controller
    {

        DBDongHoDataContext data = new DBDongHoDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
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
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }else if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }else
            {
                //gắn giá trị cho đối tượng được tạo mới (ad)
                Admin ad = data.Admins.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (ad != null)
                {
                    Session["TaiKhoan"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = " Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }    
            return View();
        }

    }
}