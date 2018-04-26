using Service;
using SClub.ShopSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.Linq;
using SClub.ShopSystem.Web.Service;
using Newtonsoft.Json;

namespace SClub.ShopSystem.Web.Controllers
{
    public class BackstageController : Controller
    {
        private MappingService _mappingService = new MappingService();
        private BackstageService _backstageService = new BackstageService();
        
        // GET: Backstage
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("admin");
            return RedirectToAction("Login");
        }

        public ActionResult IfLogin()
        {
            BackstageService service = new BackstageService();
            var UserId = Request.Form["id"];
            var password = Request.Form["password"];
            Users admin = service.Login(UserId,password);
            if (admin != null)
            {
                List<Permissions> perMissions = _backstageService.GetUserPerMissions(admin.UserId).ToList();
                AdminModel adminModel = _mappingService.GetAdminModel(perMissions, admin);
                Session.Add("admin", adminModel);
                return RedirectToAction("PersonInfo","MenuJump");
            }
            TempData["LoginWrong"] = "登录错误";
            return RedirectToAction("Login");
        }
        
        public ActionResult SideBar()
        {
            var admin = (AdminModel)Session["admin"];
            var menu = _backstageService.GetUserMenu(admin.UserId);
            List<MenuListModel> menuList = menu.Select(a => new MenuListModel
            {
                code = a.code,
                menuFatherId = a.menuFatherId,
                menuId = a.menuId,
                menuName = a.menuName,
                url = a.url,
                children = _mappingService.GetMenuChildren(a.children)
            }).ToList();
            return PartialView(menuList);
        }
        //发货
        public ActionResult Order()
        {
            var OrderId = Convert.ToInt32(Request.Form["submit"]);
            var admin = (AdminModel)Session["admin"];
            var ifOrder= _backstageService.SetDeliverOrderStage(OrderId, admin.UserId);
            Result result = new Result();
            result.url = "/MenuJump/Deliver";
            if (ifOrder == true)
            {
                result.result = "发货成功";
            }
            else
            {
                result.result = "发货失败";
            }
            return View("~/Views/Shared/Result.cshtml", result);
        }
        
        //上架保存
        public ActionResult PutawaySave(Goods goods)
        {
            HttpPostedFileBase hpf = Request.Files["goods[Img]"];
            string fileName = hpf.FileName;
            string path = "/Img/" + fileName;
            string mapPath = Server.MapPath(path);
           
            hpf.SaveAs(mapPath);

            var admin = (AdminModel)Session["admin"];
            var ifPutaway = _backstageService.PutawayNewGoods(goods, path, admin.UserId);
            
            Result result = new Result();
            result.url = "/MenuJump/Putaway";
            if (ifPutaway == true)
            {
                result.result = "上架成功"; 
            }
            else
            {
                result.result = "上架失败";
            }
            return View("~/Views/Shared/Result.cshtml", result);
        }
        //上下架商品
        public ActionResult Unshelve()
        {
            var goodsId = Convert.ToInt32(Request.Form["submit"]);
            var admin = (AdminModel)Session["admin"];
            _backstageService.Unshelve(goodsId, admin.UserId);
            return RedirectToAction("GoodsManage", "MenuJump");
        }
        //商品入库
        public ActionResult InventoryAdd()
        {
            var goodsId = Convert.ToInt32(Request.Form["goodsIdAdd"]);
            var addCount = Convert.ToInt32(Request.Form["AddInventory"]);
            var admin = (AdminModel)Session["admin"];
            var addSuccess = _backstageService.ChangeInventory(goodsId,addCount, admin.UserId);
            Result result = new Result();
            result.url = "/MenuJump/Inventory";
            if (addSuccess == true)
            {
                result.result = "入库成功";
            }
            else
            {
                result.result = "入库失败";
            }
            return View("~/Views/Shared/Result.cshtml", result);
        }
        //商品出库
        public ActionResult InventorySub()
        {
            var subCount = Convert.ToInt32(Request.Form["SubInventory"]);
            var goodsId = Convert.ToInt32(Request.Form["goodsIdSub"]);
            var admin = (AdminModel)Session["admin"];
            var subSuccess = _backstageService.ChangeInventory(goodsId,0 - subCount, admin.UserId);
            Result result = new Result();
            result.url = "/MenuJump/Inventory";
            if (subSuccess == true)
            {
                result.result = "出库成功";
            }
            else
            {
                result.result = "出库失败";
            }
            return View("~/Views/Shared/Result.cshtml", result);
            
        }
        //修改商品信息
        public ActionResult ChangeGoodsInfo(Goods goods)
        {
            HttpPostedFileBase hpf = Request.Files["goodsImg"];
            string fileName = hpf.FileName;
            string path = "/Img/" + fileName;
            string mapPath = Server.MapPath(path);
            var changeGoodsInfo = false;
            try
            {
                hpf.SaveAs(mapPath);
                var admin = (AdminModel)Session["admin"];
                changeGoodsInfo = _backstageService.ChangeGoodsInfo(goods, path, admin.UserId);
                Result result = new Result();
                result.url = "/MenuJump/GoodsManage";
                if (changeGoodsInfo == true)
                {
                    result.result = "修改成功";
                }
                else
                {
                    result.result = "修改失败";
                }
                return View("~/Views/Shared/Result.cshtml", result);
               
            }
            catch(Exception e)
            {
                var admin = (AdminModel)Session["admin"];
                changeGoodsInfo = _backstageService.ChangeGoodsInfo(goods, goods.Img, admin.UserId);
                Result result = new Result();
                result.url = "/MenuJump/GoodsManage";
                if (changeGoodsInfo == true)
                {
                    result.result = "修改成功";
                }
                else
                {
                    result.result = "修改失败";
                }
                return View("~/Views/Shared/Result.cshtml", result);
            }
           
           

        }
        public ActionResult ResetPassword(string UserId)
        {
            _backstageService.ResetPasswordById(Convert.ToInt32(UserId));
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //修改头像
        public ActionResult ChangeAdminImg()
        {
            HttpPostedFileBase hpf = Request.Files["goods[Img]"];
            string fileName = hpf.FileName;
            string path = "/Img/" + fileName;
            string mapPath = Server.MapPath(path);

            hpf.SaveAs(mapPath);
            var userId = Convert.ToInt32(Request.Form["adminId"]);
            _backstageService.ChangeAdminImg(userId, path);
            ((AdminModel)Session["admin"]).UserImg = path;
            return RedirectToAction("PersonInfo", "MenuJump");
        }
        //修改密码
        public ActionResult ChangePassword()
        {
            var userId = Convert.ToInt32(Request.Form["adminId"]);
            var password = Request.Form["password"];
            _backstageService.ChangePassword(userId, password);
            return RedirectToAction("PersonInfo", "MenuJump");
        }

        //获取所有权限
        public ActionResult GetAllRoles(string userId)
        {
            
            var roleTree = _mappingService.GetPermissonAndRoleTree(_backstageService.GetAllPerMissions(), _backstageService.GetUserPerMission(Convert.ToInt32(userId)));
            var json = JsonConvert.SerializeObject(roleTree);
            return Json(json,JsonRequestBehavior.AllowGet);
        }
        public ActionResult PermissionManage(string userId,string[] permissions)
        {
            var result = _backstageService.PermissManage(Convert.ToInt32(userId), permissions);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeRecommendImg(string id)
        {
            HttpPostedFileBase hpf = Request.Files["file"];
            string fileName = hpf.FileName;
            string path = "/Img/" + fileName;
            string mapPath = Server.MapPath(path);
            hpf.SaveAs(mapPath);
            var result = _backstageService.ChangeRemcommendImg(Convert.ToInt32(id), path);
            if (!result)
            {
                return null;
            }
            return Json(JsonConvert.SerializeObject(path), JsonRequestBehavior.AllowGet);
        }
    }
}