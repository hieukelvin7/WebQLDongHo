using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;
using Newtonsoft.Json;



namespace WebDongHo.Controllers
{
    public class NguoiDungController : Controller
    {
        DBDongHoDataContext data = new DBDongHoDataContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, User us)
        {
            var hoten = collection["HoTenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var nhaplaimatkhau = collection["NhapLaiMatKhau"];
           // var gioitinh = collection["GioiTinh"];
           // var diachi = collection["DiaChi"];
            var email = collection["Email"];
           // var dienthoai = collection["DienThoai"];
           // var ngaysinh = String.Format("{0:MM/đ/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Vui lòng nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Vui lòng xác nhận lại mật khẩu";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống";
            }
            //if (String.IsNullOrEmpty(dienthoai))
            //{
            //    ViewData["Loi6"] = "Vui lòng nhập SDT";
            //}
            //if (String.IsNullOrEmpty(ngaysinh))
            //{
            //    ViewData["Loi7"] = "Vui lòng nhập ngày sinh";
            //}
            else
            {
                us.HoTen = hoten;
                us.TaiKhoan = tendn;
                us.MatKhau = matkhau;
                us.Email = email;
               
               // us.GioiTinh = gioitinh;
               // us.DiaChi = diachi;
               // us.SDT = dienthoai;
               // us.NgaySinh =DateTime.Parse(ngaysinh);
                data.Users.InsertOnSubmit(us);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {

            
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                User us = data.Users.SingleOrDefault(p => p.TaiKhoan == tendn && p.MatKhau == matkhau);
                if (us!=null)
                {               
                    Session["TaiKhoan"] = us;
                    return RedirectToAction("Index", "HomeUser");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
               
            }
            return View();
        }
      
        public ActionResult LogOff() 
        {
            Session["TaiKhoan"] = null;

            return RedirectToAction("Index", "HomeUser");
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        [AllowAnonymous]
        public ActionResult LoginFB()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "335800521360326",
                client_secret = "75cd6945571df117172a0aa4c39eef39",
                Redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "335800521360326",
                client_secret = "75cd6945571df117172a0aa4c39eef39",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            TempData["email"] = me.email;
            TempData["first_name"] = me.first_name;
            TempData["lastname"] = me.last_name;
            TempData["picture"] = me.picture.data.url;
            //FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home");
        }
    }
}