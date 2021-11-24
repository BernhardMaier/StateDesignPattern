using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StateDesignPattern.API.DTOs;
using StateDesignPattern.Logic;
using StateDesignPattern.Logic.OrderStates;

namespace StateDesignPattern.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IEnumerable<ReadOrderDto> Get()
    {
      var dtos = new List<ReadOrderDto>();
      for (var i = 0; i < 10; i++)
      {
        var order = new Order();
        order.ChangeCustomer("John Doe");
        order.ChangeVehicle("Mercedes");

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
      var order = new Order();
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
    public ReadOrderDto Get(Guid id, ReadOrderDto input)
    {
      return new ReadOrderDto()
      {
        Id = id,
        CurrentState = input.CurrentState,
        Customer = input.Customer,
        Vehicle = input.Vehicle,
        Items = input.Items
      };
    }
  }
}