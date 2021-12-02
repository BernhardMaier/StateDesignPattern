﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StateDesignPattern.API.Features.Orders.DTOs;
using StateDesignPattern.API.Utils;
using StateDesignPattern.Logic;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.API.Features.Orders
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly ILogger<OrdersController> _logger;
    private static readonly IList<Order> Orders = new List<Order>();

    public OrdersController(ILogger<OrdersController> logger)
    {
      _logger = logger;

      _logger.Log(LogLevel.Information, "OrdersController initialized");
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadOrderDto>> GetOrders() => 
      Orders
        .Select(order =>
          order.Map(ToReadOrderDto).Value)
        .ToList();

    [HttpPost]
    public ActionResult<ReadOrderDto> CreateOrder(CreateOrderDto input)
    {
      var newOrder = new Order();
      newOrder.ChangeCustomer(input.Customer);
      newOrder.ChangeVehicle(input.Vehicle);
      
      Orders.Add(newOrder);

      var dto = newOrder.Map(ToReadOrderDto).Value;

      return new CreatedResult(newOrder.Id.ToString(), dto);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public ActionResult<ReadOrderDto> GetOrder(Guid id) =>
      GetOrderById(id)
        .Map(ToReadOrderDto)
        .Envelope();

    [HttpPut]
    [Route("{id:Guid}/customer")]
    public ActionResult ChangeCustomer(Guid id, ChangeCustomerDto input) =>
      GetOrderById(id)
        .ChangeCustomer(input.Customer)
        .Envelope();

    [HttpDelete]
    [Route("{id:Guid}/customer")]
    public ActionResult RemoveCustomer(Guid id) =>
      GetOrderById(id)
        .RemoveCustomer()
        .Envelope();

    [HttpPut]
    [Route("{id:Guid}/vehicle")]
    public ActionResult ChangeVehicle(Guid id, ChangeVehicleDto input) =>
      GetOrderById(id)
        .ChangeVehicle(input.Vehicle)
        .Envelope();

    [HttpDelete]
    [Route("{id:Guid}/vehicle")]
    public ActionResult RemoveVehicle(Guid id) =>
      GetOrderById(id)
        .RemoveVehicle()
        .Envelope();

    [HttpPut]
    [Route("{id:Guid}/items")]
    public ActionResult UpdateItems(Guid id, ChangeItemsDto input) =>
      GetOrderById(id)
        .UpdateItems(input.Items)
        .Envelope();

    private IOrder GetOrderById(Guid id) =>
      Orders.FirstOrDefault(o => o.Id == id) ?? NoOrder.Instance(id);

    private static ReadOrderDto ToReadOrderDto(IOrder order) =>
      new(order);
  }
}