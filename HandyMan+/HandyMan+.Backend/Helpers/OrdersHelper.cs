using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shered.Entities;
using HandyMan_.Shered.Enums;
using HandyMan_.Shered.Responses;

namespace HandyMan_.Backend.Helpers
{
    public class OrdersHelper : IOrdersHelper
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly ITemporalOrdersUnitOfWork _temporalOrdersUnitOfWork;
        private readonly IServicesUnitOfWork _servicesUnitOfWork;
        private readonly IOrdersUnitOfWork _ordersUnitOfWork;

        public OrdersHelper(IUsersUnitOfWork usersUnitOfWork, ITemporalOrdersUnitOfWork temporalOrdersUnitOfWork, IServicesUnitOfWork servicesUnitOfWork, IOrdersUnitOfWork ordersUnitOfWork)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _temporalOrdersUnitOfWork = temporalOrdersUnitOfWork;
            _servicesUnitOfWork = servicesUnitOfWork;
            _ordersUnitOfWork = ordersUnitOfWork;
        }

        public async Task<ActionResponse<bool>> ProcessOrderAsync(string email)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = "Usuario no válido"
                };
            }

            var actionTemporalOrders = await _temporalOrdersUnitOfWork.GetAsync(email);
            if (!actionTemporalOrders.WasSuccess)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = "No hay detalle en la orden"
                };
            }

            var temporalOrders = actionTemporalOrders.Result as List<TemporalOrder>;
            var response = new ActionResponse<bool>() { WasSuccess = true };
            if (!response.WasSuccess)
            {
                return response;
            }

            var order = new Order
            {
                Date = DateTime.UtcNow,
                User = user,
                OrderDetails = new List<OrderDetail>(),
                OrderStatus = OrderStatus.Created,
                Total = 0,
            };

            foreach (var temporalOrder in temporalOrders!)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    Service = temporalOrder.Service,
                });

                //var actionProduct = await _servicesUnitOfWork.GetAsync(temporalOrder.Service!.Id);
                //if (actionProduct.WasSuccess)
                //{
                //    var product = actionProduct.Result;
                //    if (product != null)
                //    {
                //        product.Stock -= temporalOrder.Quantity;
                //        await _productsUnitOfWork.UpdateAsync(product);
                //    }
                //}

                await _temporalOrdersUnitOfWork.DeleteAsync(temporalOrder.Id);
            }

            //await _ordersUnitOfWork.AddAsync(order);
            _ = _ordersUnitOfWork.AddOrderAsync(order);
            return response;
        }

        private async Task<ActionResponse<bool>> CheckInventoryAsync(List<TemporalOrder> temporalOrders)
        {
            var response = new ActionResponse<bool>() { WasSuccess = true };
            foreach (var temporalOrder in temporalOrders)
            {
                var actionProduct = await _servicesUnitOfWork.GetAsync(temporalOrder.Service!.Id);
                if (!actionProduct.WasSuccess)
                {
                    response.WasSuccess = false;
                    response.Message = $"El producto {temporalOrder.Service!.Id}, ya no está disponible";
                    return response;
                }

                var product = actionProduct.Result;
                if (product == null)
                {
                    response.WasSuccess = false;
                    response.Message = $"El producto {temporalOrder.Service!.Id}, ya no está disponible";
                    return response;
                }

                //if (product.Stock < temporalOrder.Quantity)
                //{
                //    response.WasSuccess = false;
                //    response.Message = $"Lo sentimos no tenemos existencias suficientes del producto {temporalOrder.Product!.Name}, para tomar su pedido. Por favor disminuir la cantidad o sustituirlo por otro.";
                //    return response;
                //}
            }
            return response;
        }
    }
}