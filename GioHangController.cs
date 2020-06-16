using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTop.Models;

namespace GioHang.Controllers
{
    public class GiohangController : Controller
    {
        //Tao doi tuong data chua dữ liệu từ model dbBansach đã tạo. 
        WEBEntities1 data = new WEBEntities1();
        //Lay gio hang
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        //Them hang vao gio
        public ActionResult ThemGiohang(int iMahang, string strURL)
        {
            //Lay ra Session gio hang
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
            Giohang sanpham = lstGiohang.Find(n => n.iMahang == iMahang);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMahang);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }


        //Xay dung trang Gio hang
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        //Tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }


        //x
        //Tao Partial view de hien thi thong tin gio hang
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        //Cap nhat Giỏ hàng
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {

            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMahang == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        //Xoa Giohang
        public ActionResult XoaGiohang(int iMaSP)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMahang == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMahang == iMaSP);
                return RedirectToAction("GioHang");

            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "SanPham");
            }
            return RedirectToAction("GioHang");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
        //Hien thi View DatHang de cap nhat cac thong tin cho Don hang
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
        //Xay dung chuc nang Dathang


        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            HoaDon ddh = new HoaDon();
            Account kh = (Account)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.makh = kh.makh;
            ddh.ngaydat = DateTime.Now;
            var ngaysuat = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.ngaysuat = DateTime.Parse(ngaysuat);
            ddh.tinhtranggiaohang = false;
            ddh.dathanhtoan = false;
            //data.HoaDons.InsertOnSubmit(ddh);
            //data.SubmitChanges();
            //Them chi tiet don hang           

            foreach (var item in gh)
            {
                CTHoaDon ctdh = new CTHoaDon();
                ctdh.ma = ddh.ma;
                ctdh.maHoaDon = item.iMahang;
                ctdh.soluong = item.iSoluong;
                //ctdh.gia = (decimal)item.dDongia;
                //data.CTHoaDons.InsertOnSubmit(ctdh);
            }
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}