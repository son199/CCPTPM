using LapTop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;

namespace LapTop.Controllers
{
    public class TrangChuController : Controller
    {
        WEBEntities1 db = new WEBEntities1();

        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);
            var sp = db.SanPhams.ToList();
            var model = sp.OrderBy(x => x.ma).ToPagedList(pageNum, pageSize);

            return View(model);
        }
        // GET: TrangChu
        public ActionResult DanhMuc(int id, int? page)
        {
            var sanpham = db.SanPhams.Where(d => d.maDanhMuc == id).ToList();
            
            int pageSize = 2;
            int pageNum = (page ?? 1);
            var model = sanpham.OrderBy(x => x.ma).ToPagedList(pageNum, pageSize);

            return View(model);

        }
        [HttpGet]
        public ActionResult DangKi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKi(FormCollection collection, Account acc)
        {
            string mail = collection["txtEmail"].ToString();
            string mk = collection["txtPassWord"].ToString();
            string ht = collection["txtFullName"].ToString();
            string dc = collection["txtDiaChi"].ToString();
            string sdt = collection["txtPhone"].ToString();
            bool gt = true;
            acc.mail = mail;
            acc.mk = mk;
            acc.hoten = ht;
            acc.diachi = dc;
            acc.sdt = sdt;
           
            db.Accounts.Add(acc);
            db.SaveChanges();
            //LỖi
            
            return RedirectToAction("Index");
        }
        
        public ActionResult ChiTiet()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChiTiet(string masp)
        {
            ViewBag.masp = masp;
            return View();
        }
        [HttpGet]
        public ActionResult Timkiem(string q, int? page)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);
            var sp = db.SanPhams.Where(x => x.ten.Contains(q)).ToList();
            var model = sp.OrderBy(x => x.ma).ToPagedList(pageNum, pageSize);
            
            return View(model);
        }
    }
}