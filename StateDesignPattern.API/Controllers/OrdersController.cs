﻿using System;
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
    private static readonly IList<Order> Orders = new List<Order>();

    public OrdersController(ILogger<OrdersController> logger)
    {
      _logger = logger;

      _logger.Log(LogLevel.Information, "OrdersController initialized");
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadOrderDto>> GetOrders()
    {
      return Orders.Select(order => MapToReadOrderDto(order).Value).ToList();
    }

    [HttpPost]
    public ActionResult<ReadOrderDto> CreateOrder(CreateOrderDto input)
    {
      var newOrder = new Order();
      newOrder.ChangeCustomer(input.Customer);
      newOrder.ChangeVehicle(input.Vehicle);
      
      Orders.Add(newOrder);
      
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
      return GetOrderById(id)
        .ChangeCustomer(input.Customer)
        .Envelope();
    }

    [HttpDelete]
    [Route("{id:Guid}/customer")]
    public ActionResult RemoveCustomer(Guid id)
    {
      return GetOrderById(id)
        .RemoveCustomer()
        .Envelope();
    }

    [HttpPut]
    [Route("{id:Guid}/vehicle")]
    public ActionResult ChangeVehicle(Guid id, ChangeVehicleDto input)
    {
      return GetOrderById(id)
        .ChangeVehicle(input.Vehicle)
        .Envelope();
    }

    [HttpDelete]
    [Route("{id:Guid}/vehicle")]
    public ActionResult RemoveVehicle(Guid id)
    {
      return GetOrderById(id)
        .RemoveVehicle()
        .Envelope();
    }

    [HttpPut]
    [Route("{id:Guid}/items")]
    public ActionResult Get(Guid id, ChangeItemsDto input)
    {
      return GetOrderById(id)
        .UpdateItems(input.Items)
        .Envelope();
    }

    private IOrder GetOrderById(Guid id) => Orders.FirstOrDefault(o => o.Id == id) ?? NoOrder.Instance(id);

    private static Result<ReadOrderDto> MapToReadOrderDto(IOrder order)
    {
      return order.CanBeMapped
        .Map(() => new ReadOrderDto
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