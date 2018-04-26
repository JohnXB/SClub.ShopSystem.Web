using System;
using Service;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SClub.ShopSystem.Web.Models;
using System.IO;
using System.Collections.Generic;
using SClub.ShopSystem.Web.Service;
using Newtonsoft.Json;

namespace SClub.ShopSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService = new UserService();
        private GoodsService _goodsService = new GoodsService();
        private MappingService _mappingService = new MappingService();
        // GET: User
       
        public ActionResult SaveOrder(OrderModel orderModel)
        {
            var order = _mappingService.OrderModelToOrder(orderModel);
            if (Session["user"] == null)
            {
                var id = Convert.ToString(order.GoodsId);
                return RedirectToAction("GoodsDetails", "Goods", new { id });
            }
            var ifSave = _goodsService.SaveOrder(order);
            Result result = new Result();
            if (ifSave)
            {
                result.result = "购买成功";
            }
            else result.result = "购买失败";
            result.url = "/Goods/GoodsDetails?id=" + Convert.ToString(orderModel.GoodsId);
            return View("~/Views/Shared/Result.cshtml", result);
            
        }
        public ActionResult Logout()
        {
            Session.Remove("user");

            return RedirectToAction("homePage","Goods");
        }
        //******个人中心
        public ActionResult PersonCenter()
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            return View((UserModel)Session["User"]);
        }

       //待发货
        public ActionResult Deliver()
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
           
            return View();
        }
        public ActionResult DeliverPage()
        {
            var orderModel = _mappingService.OrderToOrderModel(_userService.GetDeliverOrderByUserId(((UserModel)Session["User"]).UserId));
            var json = JsonConvert.SerializeObject(orderModel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        //待收货
        public ActionResult ReciptGoods()
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            
            return View();
        }
        public ActionResult ReciptGoodsPage()
        {
            var orderModel = _mappingService.OrderToOrderModel(_userService.GetReceiptOrderByUserId(((UserModel)Session["User"]).UserId));
            var json = JsonConvert.SerializeObject(orderModel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserBoughtRecord()
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            return View();
        }
        public ActionResult RecordsPage()
        {
            var orderModel = _mappingService.OrderToOrderModel(_userService.GetGoodsRecordByUserId(((UserModel)Session["User"]).UserId));
            var json = JsonConvert.SerializeObject(orderModel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        //用户收货
        public ActionResult Recipt(string userId,string orderId,string goodsId)
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            var json=_userService.ReceiptGoods(Convert.ToInt32(userId), Convert.ToInt32(orderId), Convert.ToInt32(goodsId));
            return Json(json,JsonRequestBehavior.AllowGet);
        }
        //加入购物车
        public ActionResult AddCart(string userId,string goodsId,string count)
        {
            if(userId != "null")
            {
                _userService.AddCart(Convert.ToInt32(userId), Convert.ToInt32(goodsId), Convert.ToInt32(count));
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShoppingCart()
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            var cartList = _userService.GetCart(((UserModel)Session["User"]).UserId);
            var cartModel = _mappingService.GetCartModedlList(cartList);
            return View(cartModel);

        }
        public ActionResult BuyGoodsInCart(string[] cartIdArray,string addressId)
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            var json = _userService.SaveOrderByCartId(((UserModel)Session["user"]).UserId, cartIdArray, Convert.ToInt32(addressId));
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAddressInCart()
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            var address = _goodsService.GetAddress(((UserModel)Session["user"]).UserId);
            List<AddressInfo> addressList = address.Select(a => new AddressInfo
            {
                InfoId=a.InfoId,
                Address = a.Address,
                Tel = a.Tel,
                Consignee = a.Consignee,
            }).ToList();
            var json = JsonConvert.SerializeObject(addressList);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddAddressInfo(AddressInfomation address)
        {
            var result = _userService.AddAddress(((UserModel)Session["user"]).UserId, address);
           
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteAddressInfo(AddressInfomation address)
        {
            var result = _userService.DeleteAddress(address);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddressInfo()
        {
            if (Session["user"] == null)
            {
                Result result = new Result();
                result.result = "请先登录再进入个人中心";
                result.url = "/Goods/Index";
                return View("~/Views/Shared/Result.cshtml", result);
            }
            var address = _goodsService.GetAddress(((UserModel)Session["user"]).UserId);
            List<AddressInfo> addressList = address.Select(a => new AddressInfo
            {
                InfoId = a.InfoId,
                Address = a.Address,
                Tel = a.Tel,
                Consignee = a.Consignee,
            }).ToList();
            return View(addressList);
        }
        public ActionResult ChangeUserImg()
        {
            HttpPostedFileBase hpf = Request.Files["file"];
            string fileName = hpf.FileName;
            string path = "/Img/" + fileName;
            string mapPath = Server.MapPath(path);
            hpf.SaveAs(mapPath);
            _userService.ChangeUserImg(path, ((UserModel)Session["user"]).UserId);
            ((UserModel)Session["user"]).UserImg = path;
            var json = JsonConvert.SerializeObject(path);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeNickName(string newName)
        {
            var result = _userService.ChangeNickName(((UserModel)Session["user"]).UserId, newName);
            if (!result)
            {
                return null;
            }
            ((UserModel)Session["user"]).Name = newName;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePassword(string password,string newPassword)
        {
            var result = _userService.ChangePassword(((UserModel)Session["user"]).UserId, password, newPassword);
            if (!result)
            {
                return null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}