using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;
namespace WebDongHo.Controllers
{
    public class HomeUserController : Controller
    {
        DBDongHoDataContext data = new DBDongHoDataContext();
        // GET: HomeUser
        
        public List<SanPham> SanPhamMoi(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.NgayCapNhap).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var spmoi = SanPhamMoi(4);
            return View(spmoi);
        }
       
        public ActionResult SPNu()
        {
            var clients = from c in data.SanPhams
                          where c.TenSanPham.Contains("Nữ")
                          select c;
            return PartialView(clients);
        }

        public ActionResult SPNam()
        {
            var clients = from c in data.SanPhams
                          where c.TenSanPham.Contains("Nam")
                          select c;
            return PartialView(clients);
        }
        public ActionResult DanhMucSanPham()
        {
            var danhmuc = from dm in data.DanhMucSanPhams select dm;
            return PartialView(danhmuc);
        }
        public ActionResult DanhMucThuongHieu(int id)
        {
            var sp = from s in data.SanPhams
                     where s.MaDanhMuc == id
                     select s;
            return View(sp);

        }
        public ActionResult Detail(int id)
        {
            var sp = from s in data.SanPhams
                     where s.MaSanPham == id
                     select s;
            return View(sp.Single());
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection f,LienHe lh)
        {
            var Hoten = f["HoTen"];
            var Email = f["Email"];
            var Chude = f["ChuDe"];
            var Noidung = f["NoiDung"];
            if(String.IsNullOrEmpty(Hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            else if (String.IsNullOrEmpty(Email))
            {
                ViewData["Loi2"] = "Email không được để trống";
            }
            else if (String.IsNullOrEmpty(Chude))
            {
                ViewData["Loi3"] = "Chủ đề không được để trống";
            }
            else if (String.IsNullOrEmpty(Noidung))
            {
                ViewData["Loi4"] = "Nội dung không được để trống";
            }
            else
            {
                lh.HoTen = Hoten;
                lh.Email = Email;
                lh.ChuDe = Chude;
                lh.NoiDung = Noidung;
                data.LienHes.InsertOnSubmit(lh);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Contact();  
        }        
        public ActionResult ThuongHieu()
        {
            var sp = from s in data.SanPhams select s;

            return View(sp);

        }
        public ActionResult Nu()
        {
            var clients = from c in data.SanPhams
                          where c.TenSanPham.Contains("Nữ")
                          select c;
            return View(clients);
        }

        public ActionResult Nam()
        {
            var clients = from c in data.SanPhams
                          where c.TenSanPham.Contains("Nam")
                          select c;
            return View(clients);
        }
        public ActionResult TinTuc()
        {     
            return View();
        }

        public ActionResult TinTuc1()
        {
            return View();
        }

        public ActionResult TinTuc2()
        {
            return View();
        }

        public ActionResult TinTuc3()
        {
            return View();
        }

        public ActionResult TinTuc4()
        {
            return View();
        }

        public ActionResult TinTuc5()
        {
            return View();
        }

        public ActionResult TinTuc6()
        {
            return View();
        }
    }
}