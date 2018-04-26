using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        private Entities2 db;
        public UserService() {

            db = new Entities2();
        }
        public List<Order> GetDeliverOrderByUserId(int userId)
        {
                var user = db.Users.SingleOrDefault(aUser => aUser.UserId == userId);
                return user.Order.Where(order => order.OrderState == "未发货").ToList();
        }
        public List<Order> GetReceiptOrderByUserId(int userId)
        {
            
                var user = db.Users.SingleOrDefault(aUser => aUser.UserId == userId);
                return user.Order.Where(order => order.OrderState == "未收货").ToList();
            
        }
        public List<Order> GetGoodsRecordByUserId(int userId)
        {
            
                var user = db.Users.SingleOrDefault(aUser => aUser.UserId == userId);
                return user.Order.Where(order => order.OrderState == "已完成").ToList();
            
        }
        public bool ReceiptGoods(int userId,int orderId,int goodsId)
        {
                db.Users.SingleOrDefault(aUser => aUser.UserId == userId)
                    .Order.SingleOrDefault(order => order.OrderId == orderId).OrderState = "已完成";
               var thisOrder = db.Users.SingleOrDefault(aUser => aUser.UserId == userId)
                    .Order.SingleOrDefault(order => order.OrderId == orderId);
                db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).SalesCount += thisOrder.SalesCount;
                db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).GoodsCount -= thisOrder.SalesCount;
            db.Order.SingleOrDefault(order => order.OrderId == orderId).ReceiptGoodsTime = DateTime.Now;
                if (db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).GoodsCount == 0)
                {
                    db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).IfSale = false;
                }
                db.SaveChanges();
            return true;
        }
        public void AddCart(int userId, int goodsId, int count)
        {
            ShoppingCart cart = new ShoppingCart()
            {
                GoodsCount = count,
                GoodsId = goodsId,
                UserId = userId,
            };
            db.Users.SingleOrDefault(user => user.UserId == userId).ShoppingCart.Add(cart);
            db.SaveChanges();
        }
        public List<ShoppingCart> GetCart(int userId)
        {
            return db.Users.SingleOrDefault(user => user.UserId == userId).ShoppingCart.ToList();
        }
        public void ChangeUserImg(string Imgpath, int userId)
        {
            db.Users.SingleOrDefault(user => user.UserId == userId).UserImg = Imgpath;
            db.SaveChanges();
        }
        public Users GetUserByUserId(int userId)
        {
            return db.Users.SingleOrDefault(user => user.UserId == userId);
        }
        public AddressInfomation GetAddressByAddressId(int addressId)
        {
            return db.AddressInfomation.SingleOrDefault(address => address.InfoId == addressId);
        }
        public bool SaveOrderByCartId(int userId, string[] cartIdArray,int addressId)
        {
            var user = GetUserByUserId(userId);
            var cartList = user.ShoppingCart;
            if (cartIdArray == null)
                return false;
            foreach (var cartid in cartIdArray)
            {
                var cartId = Convert.ToInt32(cartid);
                var address = GetAddressByAddressId(addressId);
                if (cartList.SingleOrDefault(cart => cart.CartId == cartId) != null)
                {
                    var cartItem = cartList.SingleOrDefault(cart => cart.CartId == cartId);
                    db.ShoppingCart.Remove(cartItem);
                    try
                    {
                        //添加订单
                        var newOrder = new Order();
                        newOrder.Address = address.Address;
                        newOrder.Consignee = address.Consignee;
                        newOrder.GoodsId = cartItem.GoodsId;
                        newOrder.GoodsName = db.Goods.SingleOrDefault(goods => goods.GoodsId == cartItem.GoodsId).GoodsName;
                        newOrder.OrderTime = DateTime.Now;
                        newOrder.Price =db.Goods.SingleOrDefault(goods=>goods.GoodsId == cartItem.GoodsId).Price;
                        newOrder.SalesCount = cartItem.GoodsCount;
                        newOrder.Tel = address.Tel;
                        newOrder.UserId = address.UserId;
                        newOrder.OrderState = "未发货";
                        db.Order.Add(newOrder);
                        db.SaveChanges();
                        continue;
                    }catch (Exception e)
                    {
                        return false;
                    }
                }
                else return false;
            }
            return true;
        }
        public bool AddAddress(int userId,AddressInfomation address)
        {
            try
            {
                var addressInfo = new AddressInfomation();
                addressInfo.Address = address.Address;
                addressInfo.Consignee = address.Consignee;
                addressInfo.Tel = address.Tel;
                addressInfo.UserId = userId;
                db.AddressInfomation.Add(addressInfo);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }
        public bool DeleteAddress(AddressInfomation address)
        {
            try
            {
                var deleteAddress= db.AddressInfomation.SingleOrDefault(addressInfo => addressInfo.InfoId == address.InfoId);
                db.AddressInfomation.Remove(deleteAddress);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool ChangeNickName(int userId,string newNickName)
        {
            try
            {
                db.Users.SingleOrDefault(user => user.UserId == userId).Name = newNickName;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }
        public bool ChangePassword(int userId,string password,string newPassword)
        {
            try
            {
                db.Users.SingleOrDefault(user => user.UserId == userId && user.Password == password).Password = newPassword;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
