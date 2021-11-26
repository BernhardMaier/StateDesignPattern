using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StateDesignPattern.API.DTOs;
using StateDesignPattern.API.Utils;
using StateDesignPattern.Logic;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
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
      return _orders.Select(order => MapToReadOrderDto(order).Value).ToList();
    }

    [HttpPost]
    public ActionResult<ReadOrderDto> CreateOrder(CreateOrderDto input)
    {
      var newOrder = new Order();
      newOrder.ChangeCustomer(input.Customer);
      newOrder.ChangeVehicle(input.Vehicle);
      
      _orders.Add(newOrder);
      
      return new CreatedResult(newOrder.Id.ToString(), MapToReadOrderDto(newOrder).Value);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public ActionResult<ReadOrderDto> GetOrder(Guid id)
    {
      var order = GetOrderById(id);
      return MapToReadOrderDto(order).Envelope();
    }

    [HttpPut]
    [Route("{id:Guid}/customer")]
    public ActionResult ChangeCustomer(Guid id, ChangeCustomerDto input)
    {
      var order = GetOrderById(id);
      var result = order.ChangeCustomer(input.Customer);
      return result.Envelope();
    }

    [HttpDelete]
    [Route("{id:Guid}/customer")]
    public ActionResult RemoveCustomer(Guid id)
    {
      var order = GetOrderById(id);
      var result = order.RemoveCustomer();
      return result.Envelope();
    }

    [HttpPut]
    [Route("{id:Guid}/vehicle")]
    public ActionResult ChangeVehicle(Guid id, ChangeVehicleDto input)
    {
      var order = GetOrderById(id);
      var result = order.ChangeVehicle(input.Vehicle);
      return result.Envelope();
    }

    [HttpDelete]
    [Route("{id:Guid}/vehicle")]
    public ActionResult RemoveVehicle(Guid id)
    {
      var order = GetOrderById(id);
      var result = order.RemoveVehicle();
      return result.Envelope();
    }

    [HttpPut]
    [Route("{id:Guid}/items")]
    public ActionResult Get(Guid id, ChangeItemsDto input)
    {
      var order = GetOrderById(id);
      var result = order.UpdateItems(input.Items);
      return result.Envelope();
    }

    private IOrder GetOrderById(Guid id) => _orders.FirstOrDefault(o => o.Id == id) ?? NoOrder.Instance(id);

    private static Result<ReadOrderDto> MapToReadOrderDto(IOrder order)
    {
      return order.CanBeMapped.Map(() => new ReadOrderDto
      {
        Id = order.Id,
        CurrentState = order.CurrentState,
        Customer = order.Customer,
        Vehicle = order.Vehicle,
        Items = order.Items
      });
    }
  }
}