using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic;

namespace StateDesignPattern.API.Features.Orders.Repository;

public interface IOrderRepository
{
  IEnumerable<Order> GetAllOrders();
  Result AddOrder(Order order);
  Maybe<Order> GetOrderById(Guid id);
}