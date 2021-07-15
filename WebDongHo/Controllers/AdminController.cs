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
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
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
            
       

    }
}