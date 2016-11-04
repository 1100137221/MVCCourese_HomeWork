using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCCourse_HomeWork.Models
{
    [計算action執行時間]
    public abstract class BaseController : Controller
    {
        protected int pageSize = 1;


        protected void CreateCell(string word,IRow ContentRow,int CellIndex,ICellStyle cellStyleBorder)
        {
            ICell _cell = ContentRow.CreateCell(CellIndex);
            _cell.SetCellValue(word);
            _cell.CellStyle = cellStyleBorder;
        }

        protected string SHA256(string password)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();//建立一個SHA256
            byte[] source = Encoding.Default.GetBytes(password);//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return result;
        }
    }
}