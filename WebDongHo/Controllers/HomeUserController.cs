using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDongHo.Models;
using PagedList;
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
        public IEnumerable<SanPham> ListAllPage(string searchString, int page, int pageSize)
        {
        
            IQueryable<SanPham> model = data.SanPhams;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.TenSanPham.Contains(searchString) || s.MoTaNgan.Contains(searchString));
            }
            return model.OrderByDescending(x => x.NgayCapNhap).ToPagedList(page, pageSize);
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
            ViewBag.TenTH = (from th in data.DanhMucSanPhams
                             where th.MaDanhMuc == id
                             select th);
            return View(sp);

        }
        public ActionResult Detail(int id)
        {
            var sp = from s in data.SanPhams
                     where s.MaSanPham == id
                     select s;
            
            ViewBag.SP_Khac = (from c in data.SanPhams
                               where c.MaDanhMuc != id                        
                               select c).ToList().Take(4);
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
        public ActionResult ThuongHieu(/*string searchString, int page = 1, int pageSize = 10*/ int? page)
        {
            var sp = from s in data.SanPhams select s;


            int pageNumber = (page ?? 1);
            int pageSize = 8;

            //ViewBag.TimKiem = ListAllPage(searchString, page, pageSize);

            //return View(sp);
            
            return View(data.SanPhams.ToList().OrderBy(n => n.MaSanPham).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Search(string searching)
        {
            //Lấy ra danh sách sản phẩm từ chuỗi tìm kiếm truyền vào
            var sp = from s in data.SanPhams select s;     
            ViewBag.TuKhoa = searching;

            return View(data.SanPhams.Where(x=>x.TenSanPham.Contains(searching) ||  searching == null).ToList());
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
        public ActionResult Payment()
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO2C5U20210720";
            string accessKey = "xTyRu9YtM0I082Z7";
            string serectkey = "bNjdi5w3IoG1fhVZbFPCDy5dUkfLZH0i";
            string orderInfo = "test";
            string returnUrl = "https://localhost:44324/HomeUser/ConfirmPaymentClient";
            string notifyurl = "https://localhost:44324/HomeUser/SavePayment"; 

            string amount = TongTien().ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            Momo crypto = new Momo();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>(); Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        public double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }


        public ActionResult ConfirmPaymentClient()
        {
            if (!Request.QueryString["errorCode"].Equals("0"))
            {
                Session["or"] = null;
                Session["lst"] = null;
                ViewBag.mess = "Thanh Toán thất bại";
            }
            else
            {
                ViewBag.mess = "Thanh toán thành công";
                SavePayment();
                Session["Giohang"] = null;
            }
            return View();
        }
     
        public void SavePayment()
        {         
            //DONDATHANG order = (DONDATHANG)Session["or"];
            //data.DONDATHANGs.InsertOnSubmit(order);
            //data.SubmitChanges();
            //List<CHITIETDONHANG> lst = (List<CHITIETDONHANG>)Session["lst"];
            //foreach (var item in lst)
            //{
            //    item.MaDonHang = order.MaDonHang;
            //    data.CHITIETDONHANGs.InsertOnSubmit(item);
            //    data.SubmitChanges();
            //}
            Session["or"] = null;
            Session["lst"] = null;
            Session["Giohang"] = null;
        }
        

        public ActionResult Sale()
        {

            var clients = from c in data.SanPhams
                          where c.KhuyenMai != null
                          select c;
            return View(clients);
        }
    }
}