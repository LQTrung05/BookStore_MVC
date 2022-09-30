using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website_sell_books.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace Website_sell_books.Controllers
{
    public class QuanLySachController : Controller
    {
        private QuanLyBanSachModel db = new QuanLyBanSachModel();

        // GET: QuanLySach
        public ActionResult Index(int? page)
        {
            var pageSize = 8;
            var pageNumber = (page ?? 1);

            var saches = db.Saches.Include(s => s.ChuDe).Include(s => s.NhaXuatBan).ToList().OrderBy(n=>n.MaSach).ToPagedList(pageNumber,pageSize);
            return View(saches);
        }

        // GET: QuanLySach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }
        
        // GET: QuanLySach/Create

        public ActionResult Create()
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            return View();
        }

        // POST: QuanLySach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sach sach)
        {
           
            if (ModelState.IsValid)
            {
                sach.AnhBia = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                { //User Namespace called: System.IO
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    //Lấy tên file upload
                    string UploadPath = Server.MapPath("~/wwwroot/images/books/" + FileName);
                    //Copy và lưu file vào server
                    f.SaveAs(UploadPath);
                    //Lưu tên file vào trường Image
                    sach.AnhBia = FileName;
                }
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // GET: QuanLySach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // POST: QuanLySach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,GiaBan,MoTa,NgayCapNhat,AnhBia,SoLuongTon,MaChuDe,MaNXB,Moi")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                sach.AnhBia= "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/WineImages/" + FileName);
                    f.SaveAs(UploadPath);
                    sach.AnhBia = FileName;
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // GET: QuanLySach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: QuanLySach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
