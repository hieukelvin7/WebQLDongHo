using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDongHo.Models
{
    public class Giohang
    {
        DBDongHoDataContext db = new DBDongHoDataContext();
        public int iMasanpham { get; set; }
        public string sTensanpham { get; set; }
        public string sAnhbia { get; set; }
        public Double dDongia { get; set; }
        public int iSoluong { get; set; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }

        public Giohang(int Masanpham)
        {
            iMasanpham = Masanpham;
            SanPham sp = db.SanPhams.Single(n => n.MaSanPham == iMasanpham);
            sTensanpham = sp.TenSanPham;
            sAnhbia = sp.ImgUrl;
            dDongia = Double.Parse(sp.GiaGoc.ToString());
            iSoluong = 1;
        }
    }
}