using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StateDesignPattern.API.DTOs;
using StateDesignPattern.API.Utils;
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
      
      _logger.Log(LogLevel.Information, "OrdersController initialized");
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadOrderDto>> GetOrders()
    {
      var dtoList = new List<ReadOrderDto>();
      
      foreach (var order in _orders)
        dtoList.Add(MapToReadOrderDto(order));
      
      return dtoList;
    }

    [HttpPost]
    public ActionResult<ReadOrderDto> CreateOrder(CreateOrderDto input)
    {
      var newOrder = new Order();
      newOrder.ChangeCustomer(input.Customer);
      newOrder.ChangeVehicle(input.Vehicle);
      
      _orders.Add(newOrder);
      
      return new CreatedResult(newOrder.Id.ToString(), MapToReadOrderDto(newOrder));
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public ActionResult<ReadOrderDto> GetOrder(Guid id)
    {
      var order = GetOrderById(id);
      if (order is null) return NotFound();
      
      return MapToReadOrderDto(order);
    }

    [HttpPut]
    [Route("{id:Guid}/customer")]
    public ActionResult ChangeCustomer(Guid id, ChangeCustomerDto input)
    {
      var order = GetOrderById(id);
      if (order is null) return NotFound();

      var result = order.ChangeCustomer(input.Customer);
      
      return result.Envelope();
    }

    [HttpDelete]
    [Route("{id:Guid}/customer")]
    public ActionResult RemoveCustomer(Guid id)
    {
      var order = GetOrderById(id);
      if (order is null) return NotFound();

      var result = order.RemoveCustomer();
      
      return result.Envelope();
    }

    [HttpPut]
    [Route("{id:Guid}/vehicle")]
    public ActionResult ChangeVehicle(Guid id, ChangeVehicleDto input)
    {
      var order = GetOrderById(id);
      if (order is null) return NotFound();

      var result = order.ChangeVehicle(input.Vehicle);
      
      return result.Envelope();
    }

    [HttpDelete]
    [Route("{id:Guid}/vehicle")]
    public ActionResult RemoveVehicle(Guid id)
    {
      var order = GetOrderById(id);
      if (order is null) return NotFound();

      var result = order.RemoveVehicle();
      
      return result.Envelope();
    }

    [HttpPut]
    [Route("{id:Guid}/items")]
    public ActionResult Get(Guid id, ChangeItemsDto input)
    {
      var order = GetOrderById(id);
      if (order is null) return NotFound();

      var result = order.UpdateItems(input.Items);
      
      return result.Envelope();
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