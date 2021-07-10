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
            return View();
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
            }
            return this.Contact();
        }
        public ActionResult Casio()
        {          
                var sp = from s in data.SanPhams
                         where s.MaDanhMuc == 1
                         select s;
                return View(sp);
           
        }
        public ActionResult Gshock()
        {
            var sp = from s in data.SanPhams
                     where s.MaDanhMuc == 2
                     select s;
            return View(sp);

        }
        public ActionResult Seiko()
        {
            var sp = from s in data.SanPhams
                     where s.MaDanhMuc == 3
                     select s;
            return View(sp);

        }
        public ActionResult Michael()
        {
            var sp = from s in data.SanPhams
                     where s.MaDanhMuc == 4
                     select s;
            return View(sp);

        }
        public ActionResult Citizen()
        {
            var sp = from s in data.SanPhams
                     where s.MaDanhMuc == 5
                     select s;
            return View(sp);

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
    }
}