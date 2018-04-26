using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{

    public class GoodsService
    {
        private Entities2 db;
        public GoodsService()
        {

            db = new Entities2();
        }
        public List<Goods> GetGoods(string style)
        {
            
                List<Goods> goodsList = new List<Goods>();
                goodsList = db.Goods.Where(goods => goods.GoodsStyle == style && goods.IfSale == true&&goods.GoodsCount !=0).ToList();
                return goodsList;
            
        }
        //通过id找商品
        public  Goods GetGoodsById(int goodsId)
        {
            
                return db.Goods.SingleOrDefault(goodsA => goodsA.GoodsId == goodsId);
            
        }
        //
        public  Users Login(string account, string password)
        {
            
                var theUser = db.Users.SingleOrDefault(aUser => aUser.Account == account);
                if (theUser != null)
                {
                    var userRoles = theUser.Roles;
                    ICollection<Permissions> allPermissions = theUser.Permissions;
                    foreach (var userRole in userRoles)
                    {
                        foreach (Permissions permission in userRole.Permissions)
                        {
                            if (!allPermissions.Contains(permission))
                                allPermissions.Add(permission);
                        }
                    }

                    if (allPermissions.SingleOrDefault(a => a.Name == "用户") != null)
                    {
                        return db.Users.ToList().SingleOrDefault(someUser => someUser.Account == account && someUser.Password == password);
                    }
                }


                return null;
        }
        //
        public  bool Register(Users user)
        {
           
                if (db.Users.SingleOrDefault(goodsA => goodsA.Account == user.Account) != null)
                {
                    return false;
                }

                var newUser = new Users();
                newUser.UserImg = "/Img/默认头像.jpg";
                newUser.Account = user.Account;
                newUser.Name = user.Name;
                newUser.Password = user.Password;
                newUser.CreationTime = DateTime.Now;


                var role = db.Roles.ToList().Find(per => per.Name == "用户");
                newUser.Roles.Add(role);
                db.Users.Add(newUser);
                db.SaveChanges();
                return true;
            
        }
        public  List<AddressInfomation> GetAddress(int userId)
        {
            
                var addressInfo = db.Users.SingleOrDefault(c => c.UserId == userId).AddressInfomation.ToList();
                if (addressInfo != null)
                    return addressInfo;
                else return null;
           
        }
        public  bool SaveOrder(Order order)
        {
           
                try
                {
                    var newOrder = new Order();
                    newOrder.Address = order.Address;
                    newOrder.Consignee = order.Consignee;
                    newOrder.GoodsId = order.GoodsId;
                    newOrder.GoodsName = order.GoodsName;
                    newOrder.OrderTime = DateTime.Now;
                    newOrder.Price = order.Price;
                    newOrder.SalesCount = order.SalesCount;
                    newOrder.Tel = order.Tel;
                    newOrder.UserId = order.UserId;
                    newOrder.OrderState = "未发货";
                    db.Order.Add(newOrder);
                    db.SaveChanges();
                    return true;
                }catch(Exception e)
                {
                    return false;
                }
                
        }
        public List<Goods> Serch(string serchItem)
        {
           
                var allGoodsList = db.Goods.ToList();
                List<Goods> goodsList = new List<Goods>();
                foreach (var goods in allGoodsList)
                {
                    if (goods.GoodsName.Contains(serchItem))
                    {
                        goodsList.Add(goods);
                        continue;
                    }
                    if (goods.GoodsIntro.Contains(serchItem))
                    {
                        goodsList.Add(goods);
                        continue;
                    }
                }
                return goodsList;
            }
        
    }
}