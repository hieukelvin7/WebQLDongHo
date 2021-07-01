﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;
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
            var gioitinh = collection["GioiTinh"];
            var diachi = collection["DiaChi"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/đ/yyyy}", collection["NgaySinh"]);
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
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Vui lòng nhập SDT";
            }
            if (String.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Loi7"] = "Vui lòng nhập ngày sinh";
            }
            else
            {
                us.HoTen = hoten;
                us.TaiKhoan = tendn;
                us.MatKhau = matkhau;
                us.Email = email;
               // us.GioiTinh = gioitinh;
                us.DiaChi = diachi;
                us.SDT = dienthoai;
                us.NgaySinh =DateTime.Parse(ngaysinh);
                data.Users.InsertOnSubmit(us);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
    }
}