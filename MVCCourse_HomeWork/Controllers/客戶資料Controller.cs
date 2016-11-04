using System;
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
    public class 客戶資料Controller : BaseController
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        private 客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶資料
        public ActionResult Index(string customerName = "",int page =1)
        {
            var data = repo.All();
            if (!String.IsNullOrEmpty(customerName))
            {
                data = data.Where(p => p.客戶名稱.Contains(customerName));
            }

            data = data.OrderBy(p=>p.Id);

            ViewBag.customerName = customerName;

            return View(data.ToPagedList(page, pageSize));
        }

        [HttpPost]
        public ActionResult DownloadExcel()
        {
            var data = repo.All();
            //讀取檔案
            string ExcelPath = Server.MapPath("~/excel/客戶資料.xlsx");
            FileStream Template = new FileStream(ExcelPath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(Template);
            Template.Close();

            //取得 Excel 的第一個頁籤
            ISheet _sheet = workbook.GetSheetAt(0);
            ICellStyle CellStyle = _sheet.GetRow(0).Cells[0].CellStyle;

            //塞資料
            int currentRow = 1;
            foreach (var item in data)
            {
                IRow MyRow = _sheet.CreateRow(currentRow);
                CreateCell(item.客戶名稱, MyRow, 0, CellStyle);
                CreateCell(item.統一編號, MyRow, 1, CellStyle);
                CreateCell(item.電話, MyRow, 2, CellStyle);
                CreateCell(item.傳真, MyRow, 3, CellStyle);
                CreateCell(item.地址, MyRow, 4, CellStyle);
                CreateCell(item.Email, MyRow,5, CellStyle);
                currentRow++;
            }

            string SavePath = @"D:/Report.xlsx";
            FileStream file = new FileStream(SavePath, FileMode.Create);
            workbook.Write(file);
            file.Close();

            return File(SavePath, "application/excel","Report.xlsx");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(客戶資料 客戶資料,FormCollection form)
        {
            if(form["account"] != "" && form["password"] != "")
            {
                string pwd = SHA256(form["password"]);
                string account = form["account"];
                if (repo.All().Where(p=>p.account == account && p.password==pwd).Any())
                {
                    Session["User"] = repo.All().Where(p => p.account == account && p.password == pwd).FirstOrDefault();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "帳號或密碼錯誤");
                }
            }
            return View();
        }

        public ActionResult EditProfile(int id)
        {
            客戶資料 客戶資料 = repo.find(id);
            客戶資料.password = "";
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        [HttpPost]
        public ActionResult EditProfile(int id,FormCollection form)
        {
            客戶資料 客戶資料 = repo.find(id);
            if (TryUpdateModel(客戶資料, null
                , new string[] { "電話", "傳真", "地址", "Email", "password" }
                ))
            {
                客戶資料.password = SHA256(form["password"]);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerList()
        {

            return View(db.vw_CustomerList.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,account,password")] 客戶資料 客戶資料,FormCollection form)
        {
            if (ModelState.IsValid)
            {
                客戶資料.password = SHA256(form["password"]);
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            客戶資料 客戶資料 = repo.find(id);
            if(TryUpdateModel(客戶資料,null
                , new string[] { "Id", "客戶名稱", "統一編號", "電話", "傳真", "地址", "Email" }
                ))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.find(id);
            客戶資料.Is刪除 = true;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
        
    }
}
