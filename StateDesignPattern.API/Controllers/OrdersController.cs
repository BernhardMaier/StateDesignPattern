using System.Collections.Generic;
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

    public OrdersController(ILogger<OrdersController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IEnumerable<OrderDto> Get()
    {
      var orderDtos = new List<OrderDto>();
      for (var i = 0; i < 10; i++)
      {
        var order = new Order();
        order.ChangeCustomer("John Doe");
        order.ChangeVehicle("Mercedes");

        orderDtos.Add(new OrderDto()
        {
          Id = i,
          CurrentState = order.CurrentState,
          Customer = order.Customer,
          Vehicle = order.Vehicle,
          Items = order.Items
        });
      }
      return orderDtos;
    }

    [HttpGet]
    [Route("{id:int}")]
    public OrderDto Get(int id)
    {
      var order = new Order();
      return new OrderDto()
      {
        Id = id,
        CurrentState = order.CurrentState,
        Customer = order.Customer,
        Vehicle = order.Vehicle,
        Items = order.Items
      };
    }
  }
}