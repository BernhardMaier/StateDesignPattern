using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic;

namespace StateDesignPattern.API.Features.Orders.Repository;

public class OrderRepository : IOrderRepository
{
  private static readonly Lazy<OrderRepository> Lazy = new(() => new OrderRepository());
  public static OrderRepository Instance => Lazy.Value;
  
  private readonly IList<Order> _orders;

  public OrderRepository()
  {
    _orders = new List<Order>();
  }

  public IEnumerable<Order> GetAllOrders() => _orders;

  public Result AddOrder(Order order) =>
    Result.Success()
      .Tap(() => _orders.Add(order));

  public Maybe<Order> GetOrderById(Guid id)
  {
    var order = _orders.FirstOrDefault(o => o.Id == id);
    return order is null
      ? Maybe<Order>.None
      : Maybe<Order>.From(order);
  }
}