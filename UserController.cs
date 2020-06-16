using LapTop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LapTop.Controllers
{
    public class USERController : Controller
    {
        // GET: USER
        WEBEntities1 db = new WEBEntities1();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        // POST: Hàm Dangky(post) Nh?n d? li?u t? trang Dangky và th?c hi?n vi?c t?o m?i d? li?u
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, Account kh)
        {
            // Gán các giá t? ngý?i dùng nh?p li?u cho các bi?n 
            var Mail = collection["mail"];
            var Mk = collection["mk"];            
            var Hoten = collection["hoten"];
            var Diachi = collection["diachi"];                                
            var Sdt = collection["sdt"];          
            if (String.IsNullOrEmpty(Hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(Hoten))
            {
                ViewData["Loi2"] = "Phải nhập Tên Đăng Nhập";
            }
            else if (String.IsNullOrEmpty(Mk))
            {
                ViewData["Loi3"] = "Phải Nhập Mật Khẩu";
            }
           


            if (String.IsNullOrEmpty(Mail))
            {
                ViewData["Loi5"] = "Email không được đăng kí";
            }

            if (String.IsNullOrEmpty(Sdt))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại";
            }
            else
            {
                //Gán giá tr? cho ð?i tý?ng ðý?c t?o m?i (kh)
                kh.mail = Mail ;
                kh.mk = Mk;                
                kh.hoten = Hoten;
                kh.diachi = Diachi;
                kh.sdt = Sdt;
                db.Accounts.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá tr? ngý?i dùng nh?p li?u cho các bi?n 
            var tendn = collection["tendangnhap"];
            var matkhau = collection["mk"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải Nhập Tên Đăng Nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải Nhập Mật Khẩu";
            }
            else
            {
                //Gán giá tr? cho ð?i tý?ng ðý?c t?o m?i (kh)
                Account kh = db.Accounts.SingleOrDefault(n => n.mail == tendn && n.mk == matkhau); // đăng nhập bằng mạl:)) thử đã
                if (kh != null)
                {
                    // ViewBag.Thongbao = "Chúc m?ng ðãng nh?p thành công";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "TrangChu");
                }
                else
                    ViewBag.Thongbao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng";
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            Response.Redirect("/TrangChu/Index");
            return View();
        }
    }
}