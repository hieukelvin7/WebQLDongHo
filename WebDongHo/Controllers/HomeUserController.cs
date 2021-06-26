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
    }
}