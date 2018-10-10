using mvcTaskBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Mvc;

namespace mvcTaskBoard.Controllers
{
    public class TaskBoardController : Controller
    {
        taskBoardDBEntities db = new taskBoardDBEntities();
        // GET: TaskBoard
        public ActionResult Index()
        {
            var item = db.Table_1.ToList();
            var item2 = item.OrderBy(x => x.rowNo);
            return View(item2.ToList());
        }
        public ActionResult UpdateItem(string itemIds)
        {
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach(var itemId in itemIdList)
            {
                try
                {
                    Table_1 item = db.Table_1.Where(x => x.id == itemId).FirstOrDefault();
                    item.rowNo = count;
                    db.Table_1.AddOrUpdate(item);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    continue;
                }
                count++;
            }
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}