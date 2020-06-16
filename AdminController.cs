//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using LapTop.Models;
//using PagedList;
//using PagedList.Mvc;
//using System.IO;


//namespace LapTop.Controllers
//{
//    public class AdminController : Controller
//    {
//        WEBEntities1 db = new WEBEntities1();
//        // GET: Admin
//        public ActionResult Index()
//        {
//            return View();
//        }
//        //public ActionResult SanPham(int? page)
//        //{
//        //    int pageNumber = (page ?? 1);
//        //    int pageSize = 7;
//        //    // return View(db.HANGs.ToList());
//        //    return View(db.HANGs.ToList().OrderBy(n => n.Mahang).ToPagedList(pageNumber, pageSize));

//        //}

//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Login(FormCollection collection)
//        {
//            // Gán các giá trị người dùng nhập liệu cho các biến 
//            var tendn = collection["username"];
//            var matkhau = collection["password"];
//            if (String.IsNullOrEmpty(tendn))
//            {
//                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
//            }
//            else if (String.IsNullOrEmpty(matkhau))
//            {
//                ViewData["Loi2"] = "Phải nhập mật khẩu";
//            }
//            else
//            {
//                //Gán giá trị cho đối tượng được tạo mới (ad)        
//                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);

//                if (ad != null)
//                {
//                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công"; 
//                    Session["Taikhoanadmin"] = ad;
//                    return RedirectToAction("Index", "Admin");
//                }
//                else
//                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
//            }
//            return View();
//        }
//        [HttpGet]
//        //public ActionResult ThemmoiHang()
//        //{
//        //    //Dua du lieu vao dropdownList
//        //    //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
//        //    ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
//        //    ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
//        //    return View();
//        //}

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult ThemmoiHang(DanhMuc hang, HttpPostedFileBase fileUpload)
//        {
//            //Dua du lieu vao dropdownload
//            ViewBag.ma = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.ten), "MaDanhMuc", "TenDanhMuc");
//            ViewBag.ma = new SelectList(db.thuongHieux.ToList().OrderBy(n => n.ten), "MaThuongHieu", "TenThuong");
//            //Kiem tra duong dan file
//            if (fileUpload == null)
//            {
//                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
//                return View();
//            }
//            //Them vao CSDL
//            else
//            {
//                if (ModelState.IsValid)
//                {
//                    //Luu ten fie, luu y bo sung thu vien using System.IO;
//                    var fileName = Path.GetFileName(fileUpload.FileName);
//                    //Luu duong dan cua file
//                    var path = Path.Combine(Server.MapPath("~/image_product"), fileName);
//                    //Kiem tra hình anh ton tai chua?
//                    if (System.IO.File.Exists(path))
//                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
//                    else
//                    {
//                        //Luu hinh anh vao duong dan
//                        fileUpload.SaveAs(path);
//                    }
//                    hang.Anhbia = fileName;
//                    //Luu vao CSDL
//                    db.HANGs.InsertOnSubmit(hang);
//                    db.SubmitChanges();
//                }
//                return RedirectToAction("Hang");
//            }
//        }
//        //Hiển thị sản phẩm
//        public ActionResult Chitiethang(int id)
//        {
//            //Lay ra doi tuong sach theo ma
//            HANG hang = db.HANGs.SingleOrDefault(n => n.Mahang == id);
//            ViewBag.Mahang = hang.Mahang;
//            if (hang == null)
//            {
//                Response.StatusCode = 404;
//                return null;
//            }
//            return View(hang);
//        }
//        [HttpGet]
//        public ActionResult Xoahang(int id)
//        {
//            //Lay ra doi tuong sach can xoa theo ma
//            HANG hang = db.HANGs.SingleOrDefault(n => n.Mahang == id);
//            ViewBag.Mahang = hang.Mahang;
//            if (hang == null)
//            {
//                Response.StatusCode = 404;
//                return null;
//            }
//            return View(hang);
//        }
//        [HttpPost, ActionName("Xoahang")]
//        public ActionResult Xacnhanxoa(int id)
//        {
//            //Lay ra doi tuong sach can xoa theo ma
//            HANG hang = db.HANGs.SingleOrDefault(n => n.Mahang == id);
//            ViewBag.Mahang = hang.Mahang;
//            if (hang == null)
//            {
//                Response.StatusCode = 404;
//                return null;
//            }
//            db.HANGs.DeleteOnSubmit(hang);
//            db.SubmitChanges();
//            return RedirectToAction("Hang");
//        }
//        //Chinh sửa sản phẩm
//        [HttpGet]
//        public ActionResult Suahang(int id)
//        {
//            //Lay ra doi tuong sach theo ma
//            HANG hang = db.HANGs.SingleOrDefault(n => n.Mahang == id);
//            ViewBag.Masach = hang.Mahang;
//            if (hang == null)
//            {
//                Response.StatusCode = 404;
//                return null;
//            }
//            //Dua du lieu vao dropdownList
//            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
//            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude", hang.MaCD);
//            ViewBag.MaNXB = new SelectList(db.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", hang.MaNCC);
//            return View(hang);
//        }

//        [HttpPost]
//        [ValidateInput(false)]
//        public ActionResult Suahang(HANG hang, HttpPostedFileBase fileUpload)
//        {
//            //Dua du lieu vao dropdownload
//            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
//            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
//            //Kiem tra duong dan file
//            if (fileUpload == null)
//            {
//                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
//                return View();
//            }
//            //Them vao CSDL
//            else
//            {
//                if (ModelState.IsValid)
//                {
//                    //Luu ten fie, luu y bo sung thu vien using System.IO;
//                    var fileName = Path.GetFileName(fileUpload.FileName);
//                    //Luu duong dan cua file
//                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham/"), fileName);
//                    //Kiem tra hình anh ton tai chua?

//                    fileUpload.SaveAs(path);

//                    HANG lp = db.HANGs.SingleOrDefault(n => n.Mahang == hang.Mahang);
//                    lp.Tenhang = hang.Tenhang;
//                    lp.Mota = hang.Mota;
//                    lp.Anhbia = fileName;
//                    lp.Giaban = hang.Giaban;
//                    lp.Soluongton = hang.Soluongton;
//                    //Luu vao CSDL   
//                    UpdateModel(hang);
//                    db.SubmitChanges();

//                }
//                return RedirectToAction("Hang");
//            }
//        }
//    }

//    internal class Admin
//    {
//    }
//}