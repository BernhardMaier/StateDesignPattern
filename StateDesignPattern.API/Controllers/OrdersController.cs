using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StateDesignPattern.API.DTOs;
using StateDesignPattern.Logic;

namespace StateDesignPattern.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly ILogger<OrdersController> _logger;
    private readonly List<Order> _orders;

    public OrdersController(ILogger<OrdersController> logger)
    {
      _logger = logger;
      _orders = new List<Order>();
    }

    [HttpGet]
    public IEnumerable<ReadOrderDto> GetOrders()
    {
      var dtoList = new List<ReadOrderDto>();
      
      foreach (var order in _orders)
        dtoList.Add(MapToReadOrderDto(order));
      
      return dtoList;
    }

    [HttpPost]
    public ReadOrderDto CreateOrder(CreateOrderDto input)
    {
      var newOrder = new Order();
      newOrder.ChangeCustomer(input.Customer);
      newOrder.ChangeVehicle(input.Vehicle);
      
      _orders.Add(newOrder);
      
      return MapToReadOrderDto(newOrder);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public ReadOrderDto GetOrder(Guid id)
    {
      var order = GetOrderById(id);

      if (order is null)
        return null;
      
      return MapToReadOrderDto(order);
    }

    [HttpPut]
    [Route("{id:Guid}/customer")]
    public ReadOrderDto ChangeCustomer(Guid id, ChangeCustomerDto input)
    {
      var order = GetOrderById(id);

      if (order is null)
        return null;

      order.ChangeCustomer(input.Customer);
      
      return MapToReadOrderDto(order);
    }

    [HttpDelete]
    [Route("{id:Guid}/customer")]
    public ReadOrderDto RemoveCustomer(Guid id)
    {
      var order = GetOrderById(id);

      if (order is null)
        return null;

      order.RemoveCustomer();
      
      return MapToReadOrderDto(order);
    }

    [HttpPut]
    [Route("{id:Guid}/vehicle")]
    public ReadOrderDto ChangeVehicle(Guid id, ChangeVehicleDto input)
    {
      var order = GetOrderById(id);

      if (order is null)
        return null;

      order.ChangeVehicle(input.Vehicle);
      
      return MapToReadOrderDto(order);
    }

    [HttpDelete]
    [Route("{id:Guid}/vehicle")]
    public ReadOrderDto RemoveVehicle(Guid id)
    {
      var order = GetOrderById(id);

      if (order is null)
        return null;

      order.RemoveVehicle();
      
      return MapToReadOrderDto(order);
    }

    [HttpPut]
    [Route("{id:Guid}/items")]
    public ReadOrderDto Get(Guid id, ChangeItemsDto input)
    {
      var order = GetOrderById(id);

      if (order is null)
        return null;

      order.UpdateItems(input.Items);
      
      return MapToReadOrderDto(order);
    }

    private Order? GetOrderById(Guid id) => _orders.FirstOrDefault(o => o.Id == id);

    private static ReadOrderDto MapToReadOrderDto(Order order)
    {
      return new ReadOrderDto
      {
        Id = order.Id,
        CurrentState = order.CurrentState,
        Customer = order.Customer,
        Vehicle = order.Vehicle,
        Items = order.Items
      };
    }
  }
}