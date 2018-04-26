using System;
using Service;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SClub.ShopSystem.Web.Models;
using System.IO;
using System.Collections.Generic;

namespace SClub.ShopSystem.Web.Service
{
    public class MappingService
    {
        private UserService _userService = new UserService();
        private GoodsService _goodsService = new GoodsService();
        public string RenderPartialView(ControllerContext controllerContext, ViewDataDictionary viewData,
            TempDataDictionary tempData, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controllerContext.RouteData.GetRequiredString("action");
            }
            viewData.Model = model;
            using (var stringWriter = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                var result = stringWriter.GetStringBuilder().ToString();
                return result;
            }
        }

        public List<CartModel> GetCartModedlList(List<ShoppingCart> cartList)
        {
            var cartModel = new List<CartModel>();
            for (int i = 0; i < cartList.Count; i++)
            {
                var goods = _goodsService.GetGoodsById(cartList[i].GoodsId);
                CartModel cart = new CartModel()
                {
                    CartId = cartList[i].CartId,
                    Count = cartList[i].GoodsCount,
                    SaleTime = goods.SaleTime.ToString(),
                    GoodsStyle = goods.GoodsStyle,
                    GoodsId = goods.GoodsId,
                    GoodsName = goods.GoodsName,
                    Img = goods.Img,
                    Price = goods.Price,
                    UserId = cartList[i].UserId
                };
                cartModel.Add(cart);
            }
            return cartModel;
        }
        public List<MenuListModel> GetMenuChildren(List<MenuList> menu)
        {
            List<MenuListModel> menuList = menu.Select(a => new MenuListModel
            {
                code = a.code,
                menuFatherId = a.menuFatherId,
                menuId = a.menuId,
                menuName = a.menuName,
                url = a.url,
                children = GetMenuChildren(a.children)
            }).ToList();
            return menuList;
        }
        public List<OrderModel> OrderToOrderModel(List<Order> orderList)
        {
            var orderModel = orderList.Select(aOrder => new OrderModel
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
            return orderModel;
        }
        public Order OrderModelToOrder(OrderModel orderModel)
        {
            var order = new Order()
            {
                Address = orderModel.Address,
                Consignee = orderModel.Consignee,
                GoodsId = orderModel.GoodsId,
                GoodsName = orderModel.GoodsName,
                SalesCount = orderModel.SalesCount,
                Tel = orderModel.Tel,
                UserId = orderModel.UserId,
                Price = orderModel.Price,

            };
            return order;
        }
        public AdminModel GetAdminModel(List<Permissions> perMissions, Users admin)
        {
            AdminModel adminModel = new AdminModel()
            {
                AllPermissions = perMissions.Select(a => new PermissionsModel
                {
                    Code = a.Code,
                    CreationTime = a.CreationTime,
                    Name = a.Name,
                    PermissionId = a.PermissionId
                }).ToList(),
                Account = admin.Account,
                CreationTime = admin.CreationTime.ToString(),
                Name = admin.Name,
                Password = admin.Password,
                UserId = admin.UserId,
                UserImg = admin.UserImg
            };
            return adminModel;
        }
        public OrderDetails GetOrderDetails(List<AddressInfomation> address, Goods goods, int salesCount)
        {
            if (address.Count != 0)
            {
                OrderDetails orderDetails = new OrderDetails
                {
                    AddressList = address.Select(a => new AddressInfo
                    {
                        InfoId = a.InfoId,
                        Address = a.Address,
                        Tel = a.Tel,
                        Consignee = a.Consignee,
                    }).ToList(),
                    UserId = address[0].UserId,
                    GoodsId = goods.GoodsId,
                    GoodsName = goods.GoodsName,
                    Price = goods.Price,
                    Img = goods.Img,
                    BuyCount = salesCount
                };
                return orderDetails;
            }
            else
            {
                OrderDetails orderDetails = new OrderDetails
                {


                    GoodsId = goods.GoodsId,
                    GoodsName = goods.GoodsName,
                    Price = goods.Price,
                    Img = goods.Img,
                    BuyCount = salesCount
                };
                return orderDetails;
            }


        }

        public List<Permission> GetPermissonAndRoleTree(List<Roles> roles,List<Permissions> permissionList)
        {
            List<Permission> permissionTree = new List<Permission>();
            int id = 1;
            foreach (var role in roles)
            {
                if (role.Name == "用户" || role.Name == "会员")
                {
                    continue;
                }
                var permission = new Permission();
                permission.name = role.Name;
                permission.id = id;
                permission.pId = 0;
                permission.open = true;
                id++;
                permissionTree.Add(permission);
                foreach (var aPermisson in role.Permissions)
                {
                    if (aPermisson.Name == "后台")
                    {
                        continue;
                    }
                    var newPermission = new Permission();
                    newPermission.name = aPermisson.Name;
                    newPermission.id = id;
                    newPermission.pId = permission.id;
                    id++;
                    permissionTree.Add(newPermission);

                }
                
            }
            foreach(var tree in permissionTree)
            {
                foreach(var permission in permissionList)
                {
                    if(permission.Name == tree.name)
                    {
                        tree.@checked = true;
                    }
                }
            }
            return permissionTree;
            
        }
    }
}