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
    public IEnumerable<ReadOrderDto> Get()
    {
      var dtos = new List<ReadOrderDto>();
      
      foreach (var order in _orders)
      {
        dtos.Add(new ReadOrderDto()
        {
          Id = order.Id,
          CurrentState = order.CurrentState,
          Customer = order.Customer,
          Vehicle = order.Vehicle,
          Items = order.Items
        });
      }
      
      return dtos;
    }

    [HttpPost]
    public ReadOrderDto Get(CreateOrderDto input)
    {
      var newOrder = new Order();
      newOrder.ChangeCustomer(input.Customer);
      newOrder.ChangeVehicle(input.Vehicle);
      
      _orders.Add(newOrder);
      
      return new ReadOrderDto()
      {
        Id = Guid.NewGuid(),
        CurrentState = newOrder.CurrentState,
        Customer = newOrder.Customer,
        Vehicle = newOrder.Vehicle,
        Items = newOrder.Items
      };
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public ReadOrderDto Get(Guid id)
    {
      var order = _orders.FirstOrDefault(o => o.Id == id);

      if (order is null)
        return null;
      
      return new ReadOrderDto()
      {
        Id = id,
        CurrentState = order.CurrentState,
        Customer = order.Customer,
        Vehicle = order.Vehicle,
        Items = order.Items
      };
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public ReadOrderDto Get(Guid id, UpdateOrderDto input)
    {
      var order = _orders.FirstOrDefault(o => o.Id == id);

      if (order is null)
        return null;

      order.ChangeCustomer(input.Customer);
      order.ChangeVehicle(input.Vehicle);
      order.UpdateItems(input.Items);
      
      return new ReadOrderDto()
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