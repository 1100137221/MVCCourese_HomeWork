﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCCourse_HomeWork.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MVCCourse_HomeWork.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        private 客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        private 客戶資料Repository customerRepo = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶聯絡人
        public ActionResult Index(string nickName = "", int page = 1)
        {
            var data = repo.All().Where(p=>!p.客戶資料.Is刪除);
            if (!string.IsNullOrEmpty(nickName))
            {
                data = data.Where(p => p.職稱.Contains(nickName));
            }
            data = data.OrderBy(p => p.Id);
            ViewBag.nickName = nickName;
            return View(data.ToPagedList(page, pageSize));
        }

        [HttpPost]
        public ActionResult DownloadExcel()
        {
            var data = repo.All();
            //讀取檔案
            string ExcelPath = Server.MapPath("~/excel/客戶聯絡人.xlsx");
            FileStream Template = new FileStream(ExcelPath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(Template);
            Template.Close();

            //取得 Excel 的第一個頁籤
            ISheet _sheet = workbook.GetSheetAt(0);
            ICellStyle CellStyle = _sheet.GetRow(1).Cells[0].CellStyle;

            //塞資料
            int currentRow = 1;
            foreach (var item in data)
            {
                IRow MyRow = _sheet.CreateRow(currentRow);
                CreateCell(item.職稱, MyRow, 0, CellStyle);
                CreateCell(item.姓名, MyRow, 1, CellStyle);
                CreateCell(item.Email, MyRow, 2, CellStyle);
                CreateCell(item.手機, MyRow, 3, CellStyle);
                CreateCell(item.電話, MyRow, 4, CellStyle);
                CreateCell(item.客戶資料.客戶名稱, MyRow, 5, CellStyle);
                currentRow++;
            }

            string SavePath = @"D:/ReportCunstomerContact.xlsx";
            FileStream file = new FileStream(SavePath, FileMode.Create);
            workbook.Write(file);
            file.Close();

            return File(SavePath, "application/excel", "ReportCunstomerContact.xlsx");
        }

        public ActionResult test()
        {
            return View();
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo.find(id);
            客戶聯絡人.Is刪除 = true;
            repo.UnitOfWork.Commit();
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
