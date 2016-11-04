using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCourse_HomeWork.Models
{
    public abstract class BaseController : Controller
    {
        protected int pageSize = 1;


        protected void CreateCell(string word,IRow ContentRow,int CellIndex,ICellStyle cellStyleBorder)
        {
            ICell _cell = ContentRow.CreateCell(CellIndex);
            _cell.SetCellValue(word);
            _cell.CellStyle = cellStyleBorder;
        }
    }
}