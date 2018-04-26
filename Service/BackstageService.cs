using System;
using System.Collections.Generic;
using System.Linq;


namespace Service
{
    public class BackstageService
    {
        private Entities2 db;
        public BackstageService()
        {

            db = new Entities2();
        }

        //获取用户的所有权限
        public ICollection<Permissions> GetUserPerMissions(int userId)
        {

            var theUser = db.Users.SingleOrDefault(aUser => aUser.UserId == userId);
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

            return allPermissions;

        }
        //获取所有权限
        public List<Roles> GetAllPerMissions()
        {
            List<Roles> allRoles = db.Roles.ToList();
            return allRoles;
        }
        //获取用户权限
        public List<Permissions> GetUserPerMission(int userId)
        {
            return db.Users.SingleOrDefault(user => user.UserId == userId).Permissions.ToList();
        }
        //获取用户应该有的菜单
        public List<MenuList> GetUserMenu(int userId)
        {
            var allPermissions = GetUserPerMissions(userId);

            List<Menu> thisUserMenu = new List<Menu>();

            List<Menu> menus = db.Menu.ToList();
            foreach (var menu in menus)
            {
                if (allPermissions.ToList().Find(per => per.Code.StartsWith(menu.code)) != null)
                {
                    if (!thisUserMenu.Contains(menu))
                    {
                        thisUserMenu.Add(menu);
                    }
                }
            }

            var userMenu = GetUserMenu(thisUserMenu);
            return userMenu;
        }
        //将获取到的菜单处理成树形
        public List<MenuList> GetUserMenu(List<Menu> thisUserMenu)
        {
            if (thisUserMenu.Count == 0)
                return new List<MenuList>();
            List<MenuList> list = new List<MenuList>();
            foreach (var item in thisUserMenu)
            {
                if (item.fatherId == 0)
                {
                    MenuList menu = new MenuList();
                    menu.menuId = item.id;
                    menu.code = item.code;
                    menu.url = item.url;
                    menu.menuName = item.menuName;
                    menu.menuFatherId = item.fatherId;
                    menu.children = GetUserMenu(menu, thisUserMenu);
                    list.Add(menu);
                }

            }
            return list;
        }
        //将获取到的菜单处理成树形递归方法
        public List<MenuList> GetUserMenu(MenuList menu, List<Menu> thisUserMenu)
        {
            var menuChildrenDatas = thisUserMenu.Where(menu1 => menu1.fatherId == menu.menuId).ToList();
            if (menuChildrenDatas.Count == 0)
                return new List<MenuList>();
            List<MenuList> list = new List<MenuList>();
            foreach (var menuChildData in menuChildrenDatas)
            {
                MenuList menuChild = new MenuList();
                menuChild.menuId = menuChildData.id;
                menuChild.code = menuChildData.code;
                menuChild.menuName = menuChildData.menuName;
                menuChild.url = menuChildData.url;
                menuChild.menuFatherId = menuChildData.fatherId;
                menu.children.AddRange(GetUserMenu(menuChild, thisUserMenu));
                list.Add(menuChild);
            }
            return list;
        }
        //登录
        public Users Login(string InputName, string InputPassword)
        {

            var theUser = db.Users.SingleOrDefault(aUser => aUser.Account == InputName);
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

                if (allPermissions.SingleOrDefault(a => a.Name == "后台") != null)
                {
                    return db.Users.ToList().SingleOrDefault(someUser => someUser.Account == InputName && someUser.Password == InputPassword);
                }
            }
            return null;

        }

        //获取未发货订单
        public List<Order> GetDeliverOrder(string goodsStyle)
        {

            if (goodsStyle == null || goodsStyle == "游戏")
            {
                return db.Order.Where(order => order.OrderState == "未发货" && order.Goods.GoodsStyle == "游戏").ToList();
            }
            else if (goodsStyle == "装备")
            {
                return db.Order.Where(order => order.OrderState == "未发货" && order.Goods.GoodsStyle == "装备").ToList();
            }
            else return db.Order.Where(order => order.OrderState == "未发货" && order.Goods.GoodsStyle == "周边").ToList();

        }
        public List<Order> GetReceiptOrder(string goodsStyle)
        {

            if (goodsStyle == null || goodsStyle == "游戏")
            {
                return db.Order.Where(order => order.OrderState == "未收货" && order.Goods.GoodsStyle == "游戏").ToList();
            }
            else if (goodsStyle == "装备")
            {
                return db.Order.Where(order => order.OrderState == "未收货" && order.Goods.GoodsStyle == "装备").ToList();
            }
            else return db.Order.Where(order => order.OrderState == "未收货" && order.Goods.GoodsStyle == "周边").ToList();


        }
        public List<Order> GetGoodsRecord(string goodsStyle)
        {

            if (goodsStyle == null || goodsStyle == "游戏")
            {
                return db.Order.Where(order => order.OrderState == "已完成" && order.Goods.GoodsStyle == "游戏").ToList();
            }
            else if (goodsStyle == "装备")
            {
                return db.Order.Where(order => order.OrderState == "已完成" && order.Goods.GoodsStyle == "装备").ToList();
            }
            else return db.Order.Where(order => order.OrderState == "已完成" && order.Goods.GoodsStyle == "周边").ToList();

        }
        //发货后订单状态修改(工作日志)
        public bool SetDeliverOrderStage(int orderId, int userId)
        {

            if (db.Order.SingleOrDefault(aorder => aorder.OrderId == orderId) == null)
            {
                return false;
            }
            db.Order.SingleOrDefault(aorder => aorder.OrderId == orderId).OrderState = "未收货";
            db.Order.SingleOrDefault(aorder => aorder.OrderId == orderId).DeliveryTime = DateTime.Now;
            db.SaveChanges();
            AddRecord(userId, "发货", orderId);
            return true;

        }
        public Goods getGoodsById(int goodsId)
        {

            return db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId);


        }
        //后台获取正在销售的商品列表
        public List<Goods> GetGoodsByStyle(string goodsStyle)
        {

            if (goodsStyle == "游戏")
            {
                return db.Goods.Where(goods => goods.GoodsStyle == "游戏" && goods.IfSale == true).ToList();
            }
            else if (goodsStyle == "装备")
            {
                return db.Goods.Where(goods => goods.GoodsStyle == "装备" && goods.IfSale == true).ToList();

            }
            else if (goodsStyle == "周边")
            {
                return db.Goods.Where(goods => goods.GoodsStyle == "周边" && goods.IfSale == true).ToList();
            }
            else return db.Goods.Where(goods => goods.IfSale == false).ToList();
            //  }


        }
        //后台获取所有商品列表
        public List<Goods> GetAllGoodsByStyle(string goodsStyle)
        {

            if (goodsStyle == "游戏")
            {
                return db.Goods.Where(goods => goods.GoodsStyle == "游戏").ToList();
            }
            else if (goodsStyle == "装备")
            {
                return db.Goods.Where(goods => goods.GoodsStyle == "装备").ToList();

            }
            else return db.Goods.Where(goods => goods.GoodsStyle == "周边").ToList();

            //  }


        }
        //上架新商品(工作日志)
        public bool PutawayNewGoods(Goods goods, string path, int userId)
        {

            if (db.Goods.SingleOrDefault(goodsA => goodsA.GoodsName == goods.GoodsName && goodsA.GoodsStyle == goods.GoodsStyle) != null)
            {
                return false;
            }

            Goods newGoods = new Goods();
            newGoods.GoodsName = goods.GoodsName;
            newGoods.Price = goods.Price;
            newGoods.PurchasePrice = goods.PurchasePrice;
            newGoods.GoodsCount = 0;
            newGoods.GoodsStyle = goods.GoodsStyle;
            newGoods.GoodsIntro = goods.GoodsIntro;
            newGoods.IfSale = true;
            newGoods.SalesCount = 0;
            newGoods.SaleTime = DateTime.Now;
            newGoods.Img = path;
            db.Goods.Add(newGoods);
            db.SaveChanges();
            try
            {
                var thisGoods = db.Goods.SingleOrDefault(aGoods => aGoods.GoodsStyle == goods.GoodsStyle && aGoods.GoodsName == goods.GoodsName);
                AddRecord(userId, "上新", thisGoods.GoodsId);
                return true;
            }
           
            catch(Exception e)
            {
                return false;
            }

        }
        //上下架商品(工作日志)
        public void Unshelve(int goodsId, int userId)
        {

            var ifSale = db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).IfSale;
            if (ifSale == false)
            {
                AddRecord(userId, "上架", goodsId);
                db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).IfSale = true;
                db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).SaleTime = DateTime.Now;
            }
            else
            {
                db.Goods.SingleOrDefault(goods => goods.GoodsId == goodsId).IfSale = false;
                AddRecord(userId, "下架", goodsId);
            }
            db.SaveChanges();

        }
        //修改库存(工作日志)
        public bool ChangeInventory(int goodsId, int count, int userId)
        {

            if (db.Goods.SingleOrDefault(nowGoods => nowGoods.GoodsId == goodsId) == null)
            {
                return false;
            }
            db.Goods.SingleOrDefault(nowGoods => nowGoods.GoodsId == goodsId).GoodsCount += count;
            db.SaveChanges();
            if (count > 0)
                AddRecord(userId, "入库" + count + "件", goodsId);
            else
                AddRecord(userId, "出库" + (-count) + "件", goodsId);
            return true;

        }
        //获取推荐图集
        public List<Carousel> GetCarousel()
        {

            return db.Carousel.ToList();

        }
        //修改商品信息(工作日志)
        public bool ChangeGoodsInfo(Goods goods, string path, int userId)
        {

            try
            {
                db.Goods.SingleOrDefault(goodsA => goodsA.GoodsId == goods.GoodsId).GoodsName = goods.GoodsName;
                db.Goods.SingleOrDefault(goodsA => goodsA.GoodsId == goods.GoodsId).Price = goods.Price;
                db.Goods.SingleOrDefault(goodsA => goodsA.GoodsId == goods.GoodsId).PurchasePrice = goods.PurchasePrice;
                db.Goods.SingleOrDefault(goodsA => goodsA.GoodsId == goods.GoodsId).GoodsIntro = goods.GoodsIntro;
                db.Goods.SingleOrDefault(goodsA => goodsA.GoodsId == goods.GoodsId).Img = path;

                db.SaveChanges();
                AddRecord(userId, "修改商品信息", goods.GoodsId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        //  添加工作日志
        public void AddRecord(int userId, string recordName, int goodsIdOrOrderId)
        {

            var record = new AdminRecord();
            record.UserId = userId;
            record.RecordName = recordName;
            record.RecordTime = DateTime.Now;
            if (recordName == "发货")
            {
                record.OrderId = goodsIdOrOrderId;
            }
            else record.GoodsId = goodsIdOrOrderId;
            //db.AdminRecord.Add(record);
            db.SaveChanges();

        }
        //获取管理员工作日志
        public List<AdminRecord> GetRecordByAdminId(int userId)
        {

            var permissions = GetUserPerMissions(userId);
            if (permissions.SingleOrDefault(a => a.Name == "员工管理      ") != null)
            {
                return db.AdminRecord.ToList();
            }
            return db.AdminRecord.Where(record => record.UserId == userId).ToList();

        }
        //获取所有管理员
        public List<Users> GetAllAdmin()
        {

            var users = db.Users.ToList();
            List<Users> userList = new List<Users>();
            foreach (var user in users)
            {
                var permissions = GetUserPerMissions(user.UserId);
                if (permissions.ToList().Find(permission => permission.Name == "后台") != null)
                {
                    userList.Add(user);
                }
            }
            return userList;

        }
        //获取管理员的角色
        public List<Roles> GetAllRolesById(int userId)
        {

            var user = db.Users.SingleOrDefault(aUser => aUser.UserId == userId);
            return user.Roles.ToList();

        }
        //重置密码为000000
        public void ResetPasswordById(int userId)
        {

            db.Users.SingleOrDefault(user => user.UserId == userId).Password = "000000";
            db.SaveChanges();

        }
        public void ChangeAdminImg(int userId, string path)
        {

            db.Users.SingleOrDefault(user => user.UserId == userId).UserImg = path;
            db.SaveChanges();

        }
        public void ChangePassword(int userId, string password)
        {

            db.Users.SingleOrDefault(user => user.UserId == userId).Password = password;
            db.SaveChanges();

        }
        public bool PermissManage(int userId, string[] permissions)
        {
            var userPermissonsById = db.Users.SingleOrDefault(user => user.UserId == userId).Permissions.ToList();
            if (userPermissonsById.Count == 0)
            {
                foreach (var permission in permissions)
                {
                    var permiss = db.Permissions.SingleOrDefault(per => per.Name == permission);
                    if (permiss != null)
                    {
                        db.Users.SingleOrDefault(user => user.UserId == userId).Permissions.Add(permiss);
                    }
                }
            }
            //权限不为空
            else
            {
                foreach (var userPermisson in userPermissonsById)
                {
                    var check = true;
                    if(permissions == null)
                    {
                        db.Users.SingleOrDefault(user => user.UserId == userId).Permissions.Remove(userPermisson);
                        continue;
                    }
                    foreach (var permission in permissions)
                    {
                        var permiss = db.Permissions.SingleOrDefault(per => per.Name == permission);
                        if (permiss != null)
                        {
                            db.Users.SingleOrDefault(user => user.UserId == userId).Permissions.Add(permiss);
                        }
                        if (userPermisson.Name == permission)
                        {
                            check = false;
                            continue;
                        }
                    }
                    if (check)
                    {
                        //删除该权限
                        db.Users.SingleOrDefault(user => user.UserId == userId).Permissions.Remove(userPermisson);
                    }
                }
            }
            var userPermissonsB = db.Users.SingleOrDefault(user => user.UserId == userId).Permissions.ToList();
            db.SaveChanges();
            return true;
        }

        public bool ChangeRemcommendImg(int imgId,string path)
        {
            try
            {

                db.Carousel.SingleOrDefault(img => img.Id == imgId).Img = path;
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
