using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SClub.ShopSystem.Web.Models;
using Service;
using Newtonsoft.Json;
using SClub.ShopSystem.Web.Service;

namespace SClub.ShopSystem.Web.Controllers
{
    public class GoodsController : Controller
    {
        private MappingService _mappingService = new MappingService();
        private BackstageService _backstageService = new BackstageService();
        private GoodsService _goodsService = new GoodsService();
        // GET: Goods
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GoodsList(string style)
        {
            if (style == "home")
                return RedirectToAction("Index");
            else
            {
                Result result = new Result();
                result.result = style;
                return View(result);
            }   
        }
       public ActionResult ShowGoodsList(string style)
        {
            var goodsList = _goodsService.GetGoods(style);
            List<GoodsModel> goodsModelList = goodsList.Select(goods => new GoodsModel
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
                PurchasePrice = goods.PurchasePrice,
            }).ToList();
            var json = JsonConvert.SerializeObject(goodsModelList);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login(string account,string password)
        {
            Users aUser = _goodsService.Login(account,password);
            if (aUser != null)
            {
                UserModel user = new UserModel()
                {
                    Account = aUser.Account,
                    CreationTime = aUser.CreationTime.ToString(),
                    Name = aUser.Name,
                    Password = aUser.Password,
                    UserId = aUser.UserId,
                    UserImg = aUser.UserImg

                };
                Session.Add("user", user);
                string json = JsonConvert.SerializeObject(user);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return null;

        }
        public ActionResult Logout()
        {
            try
            {
                Session.Remove("user");
            }
            catch(Exception e) {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult SaveRegister(Users user)
        {
            var register = _goodsService.Register(user);
            Result result = new Result();
            if (register)
            {
                result.result = "注册成功！点击回到主页";
                result.url = "/Goods/Index";
            }
            else
                result.result = "注册失败,请重新输入";
                result.url = "/Goods/Register";
            return View("~/Views/Shared/Result.cshtml", result);
        }
        public ActionResult Carousel()
        {
            var list = _backstageService.GetCarousel();
            List<CarouselModel> carousleList = list.Select(carousle => new CarouselModel
            {
                GoodsId = carousle.GoodsId,
                Id = carousle.Id,
                Img = carousle.Img
            }).ToList();
            return PartialView(carousleList);
        }
        public ActionResult GoodsDetails(string id)
        {
            var goodsId = Convert.ToInt32(id);
            try
            {
                 var goods = _goodsService.GetGoodsById(goodsId);
                 GoodsModel goodsModel = new GoodsModel
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
                    PurchasePrice = goods.PurchasePrice,
                };
                return View(goodsModel);
            }catch(Exception e)
            {
                return RedirectToAction("homePage");
            }
           
            
        }
        public ActionResult Serch()
        {
            Result result = new Result();
            result.result = Request.Form["serch"];
            return View(result);

        }
        public ActionResult BuyGoods(OrderModel orderModel)
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进行购买操作";
                result.url = "/Goods/GoodsDetails?id=" + Convert.ToString(orderModel.GoodsId);
                return View("~/Views/Shared/Result.cshtml", result);
            }
            var address = _goodsService.GetAddress(orderModel.UserId);
            var goods = _goodsService.GetGoodsById(orderModel.GoodsId);
            OrderDetails orderDetails = _mappingService.GetOrderDetails(address, goods, orderModel.SalesCount);
            return View(orderDetails);
        }
        public ActionResult SerchItem(string serchItem)
        {
            var goodsList = _goodsService.Serch(serchItem);
            List<GoodsModel> goodsModelList = goodsList.Select(goods => new GoodsModel
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
                PurchasePrice = goods.PurchasePrice,
            }).ToList();
            var json = JsonConvert.SerializeObject(goodsModelList);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}