using Service;
using SClub.ShopSystem.Web.Models;
using System;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace SClub.ShopSystem.Web.Controllers
{
    public class MenuJumpController : Controller
    {
        private BackstageService _backstageService = new BackstageService();
        // GET: MenuJump
        
        public ActionResult PersonInfo()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            return View("~/Views/MenuJump/PersonInfo.cshtml");
        }
        public ActionResult CountInfo()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            return View("~/Views/MenuJump/CountInfo.cshtml");
        }
        public ActionResult Order_Deliver(string style)
        {
            var order = _backstageService.GetDeliverOrder(style);
            List<OrderModel> orderModel = order.Select(aOrder => new OrderModel
            {
                Address = aOrder.Address,
                Consignee = aOrder.Consignee,
                GoodsId = aOrder.GoodsId,
                GoodsName= aOrder.GoodsName,
                OrderId = aOrder.OrderId,
                OrderState = aOrder.OrderState,
                OrderTime = aOrder.OrderTime.ToString(),
                Price = aOrder.Price,
                SalesCount = aOrder.SalesCount,
                Tel = aOrder.Tel,
                DeliveryTime = aOrder.DeliveryTime.ToString(),
                UserId = aOrder.UserId,
                ReceiptGoodsTime = aOrder.ReceiptGoodsTime.ToString()
            }).ToList();
            
            if (orderModel.Count == 0)
            {
                return null;
            }
            var json = JsonConvert.SerializeObject(orderModel);
            
            return Json(json,JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult Order_Receipt(string style)
        {
            var order = _backstageService.GetReceiptOrder(style);
            List<OrderModel> orderModel = order.Select(aOrder => new OrderModel
            {
                Address = aOrder.Address,
                Consignee = aOrder.Consignee,
                GoodsId = aOrder.GoodsId,
                GoodsName = aOrder.GoodsName,
                OrderId = aOrder.OrderId,
                OrderState = aOrder.OrderState,
                OrderTime = aOrder.OrderTime.ToString(),
                Price = aOrder.Price,
                SalesCount = aOrder.SalesCount,
                Tel = aOrder.Tel,
                DeliveryTime = aOrder.DeliveryTime.ToString(),
                UserId = aOrder.UserId,
                ReceiptGoodsTime = aOrder.ReceiptGoodsTime.ToString()
            }).ToList();
            
            if (orderModel.Count == 0)
            {
                return null;
            }
            var json = JsonConvert.SerializeObject(orderModel);

            return Json(json, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Order_Record(string style)
        {
            var order = _backstageService.GetGoodsRecord(style);
            List<OrderModel> orderModel = order.Select(aOrder => new OrderModel
            {
                Address = aOrder.Address,
                Consignee = aOrder.Consignee,
                GoodsId = aOrder.GoodsId,
                GoodsName = aOrder.GoodsName,
                OrderId = aOrder.OrderId,
                OrderState = aOrder.OrderState,
                OrderTime = aOrder.OrderTime.ToString(),
                Price = aOrder.Price,
                SalesCount = aOrder.SalesCount,
                Tel = aOrder.Tel,
                DeliveryTime = aOrder.DeliveryTime.ToString(),
                UserId = aOrder.UserId,
                ReceiptGoodsTime = aOrder.ReceiptGoodsTime.ToString()
            }).ToList();
           
            if (orderModel.Count == 0)
            {
                return null;
            }
            var json = JsonConvert.SerializeObject(orderModel);

            return Json(json, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Deliver()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            return View("~/Views/MenuJump/Deliver.cshtml");
        }
        public ActionResult ReceiptGoods()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            return View("~/Views/MenuJump/ReceiptGoods.cshtml");
        }
        public ActionResult GoodsRecord()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            
            return View();
        }
        //商品管理
        
        public ActionResult GoodsManage_ChangeTable(string style)
        {
            var goods = _backstageService.GetGoodsByStyle(style);
            List<GoodsModel> goodsModel = goods.Select(a => new GoodsModel
            {
                GoodsId = a.GoodsId,
                GoodsName = a.GoodsName,
                Price = a.Price,
                Img = a.Img,
                SalesCount = a.SalesCount,
                SaleTime = a.SaleTime.ToString(),
                GoodsStyle = a.GoodsStyle,
                GoodsCount = a.GoodsCount,
                GoodsIntro = a.GoodsIntro,
                IfSale = a.IfSale,
                PurchasePrice = a.PurchasePrice,
            }).ToList();
            
            var json = JsonConvert.SerializeObject(goodsModel);
            return Json(json, JsonRequestBehavior.AllowGet);

        }
       
        public ActionResult ChangeGoodsInfo(string goodsId)
        {
            var goods = _backstageService.getGoodsById(Convert.ToInt32(goodsId));
            GoodsModel goodsModel = new GoodsModel()
            {
                GoodsId = goods.GoodsId,
                GoodsName = goods.GoodsName,
                Price = goods.Price,
                Img = goods.Img,
                SalesCount = goods.SalesCount,
                SaleTime = goods.SaleTime.ToString(),
                GoodsStyle = goods.GoodsStyle,
                GoodsCount = goods.GoodsCount,
                GoodsIntro = goods.GoodsIntro,
                IfSale = goods.IfSale,
                PurchasePrice = goods.PurchasePrice
            };
            return View(goodsModel);
        }
        public ActionResult GoodsManage()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            
            
            return View("~/Views/MenuJump/GoodsManage.cshtml");
        }
        public ActionResult Putaway()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            return View();
        }
        public ActionResult GoodsRecommend()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            List<CarouselModel> carouselModel = _backstageService.GetCarousel().Select(carousel => new CarouselModel
            {
                GoodsId = carousel.GoodsId,
                Id = carousel.Id,
                Img = carousel.Img
            }).ToList();
            return View(carouselModel);
        }

        public ActionResult Inventory_ChangeTable(string style)
        {
            var goods = _backstageService.GetAllGoodsByStyle(style);
            List<GoodsModel> goodsModel = goods.Select(a => new GoodsModel
            {
                GoodsId = a.GoodsId,
                GoodsName = a.GoodsName,
                Price = a.Price,
                Img = a.Img,
                SalesCount = a.SalesCount,
                SaleTime = a.SaleTime.ToString(),
                GoodsStyle = a.GoodsStyle,
                GoodsCount = a.GoodsCount,
                GoodsIntro = a.GoodsIntro,
                IfSale = a.IfSale,
                PurchasePrice = a.PurchasePrice,
            }).ToList();
            var json = JsonConvert.SerializeObject(goodsModel);
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult Inventory()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            
            return View("~/Views/MenuJump/Inventory.cshtml");
        }
        public ActionResult UserManage_GoPage()
        {
            List<UserManageModel> adminList = _backstageService.GetAllAdmin().Select(admin => new UserManageModel
            {
                Account = admin.Account,
                CreationTime = admin.CreationTime.ToString(),
                Name = admin.Name,
                UserId = admin.UserId,
                AllRoles = _backstageService.GetAllRolesById(admin.UserId).Select(role => new RoleModel
                {
                    Code = role.Code,
                    CreationTime = role.CreationTime.ToString(),
                    Name = role.Name,
                    RoleId = role.RoleId
                }).ToList()
            }).ToList();
            var json = JsonConvert.SerializeObject(adminList);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserManage()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }

            
            
            
            return View();
        }
        public ActionResult Record_GoPage()
        {
            var admin = (AdminModel)Session["admin"];
            var record = _backstageService.GetRecordByAdminId(admin.UserId);
            List<AdminRecordModel> recordModel = record.Select(newRecord => new AdminRecordModel
            {
                GoodsId = newRecord.GoodsId,
                OrderId = newRecord.OrderId,
                RecordId = newRecord.RecordId,
                RecordName = newRecord.RecordName,
                RecordTime = newRecord.RecordTime.ToString(),
                UserId = newRecord.UserId
            }).ToList();
            var json = JsonConvert.SerializeObject(recordModel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Recording()
        {
            if (Session["admin"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入后台管理";
                result.url = "/Backstage/Login";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            
            return View();
        }
    }
}