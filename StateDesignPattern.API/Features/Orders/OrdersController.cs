using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StateDesignPattern.API.Features.Orders.DTOs;
using StateDesignPattern.API.Features.Orders.Repository;
using StateDesignPattern.API.Utils;
using StateDesignPattern.Logic;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.API.Features.Orders;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
  // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
  private readonly ILogger<OrdersController> _logger;
  private readonly IOrderRepository _orderRepository;

  public OrdersController(ILogger<OrdersController> logger)
  {
    _orderRepository = OrderRepository.Instance;
    _logger = logger;

    _logger.Log(LogLevel.Information, "OrdersController initialized");
  }

  [HttpGet]
  public ActionResult<IEnumerable<ReadOrderDto>> GetOrders() => 
    _orderRepository
      .GetAllOrders()
      .Select(order => order.Map(ToReadOrderDto))
      .Where(mapResult => mapResult.IsSuccess)
      .Select(mapResult => mapResult.Value)
      .ToList();

  [HttpPost]
  public ActionResult<ReadOrderDto> CreateOrder(CreateOrderDto input) =>
    Order
      .Create()
      .Check(order => order.ChangeCustomer(input.Customer))
      .Check(order => order.ChangeVehicle(input.Vehicle))
      .Check(order => _orderRepository.AddOrder(order))
      .Map(ToReadOrderDto)
      .EnvelopeAsCreated();

  [HttpGet]
  [Route("{id:Guid}")]
  public ActionResult<ReadOrderDto> GetOrder(Guid id) =>
    _orderRepository
      .GetOrderById(id)
      .ToResult(HttpStatusCode.NotFound.ToString())
      .Map(ToReadOrderDto)
      .EnvelopeAsOkObject();

  [HttpPut]
  [Route("{id:Guid}/customer")]
  public ActionResult ChangeCustomer(Guid id, ChangeCustomerDto input) =>
    _orderRepository
      .GetOrderById(id)
      .ToResult(HttpStatusCode.NotFound.ToString())
      .Check(order => order.ChangeCustomer(input.Customer))
      .EnvelopeAsOk();

  [HttpDelete]
  [Route("{id:Guid}/customer")]
  public ActionResult RemoveCustomer(Guid id) =>
    _orderRepository
      .GetOrderById(id)
      .ToResult(HttpStatusCode.NotFound.ToString())
      .Check(order => order.RemoveCustomer())
      .EnvelopeAsOk();

  [HttpPut]
  [Route("{id:Guid}/vehicle")]
  public ActionResult ChangeVehicle(Guid id, ChangeVehicleDto input) =>
    _orderRepository
      .GetOrderById(id)
      .ToResult(HttpStatusCode.NotFound.ToString())
      .Check(order => order.ChangeVehicle(input.Vehicle))
      .EnvelopeAsOk();

  [HttpDelete]
  [Route("{id:Guid}/vehicle")]
  public ActionResult RemoveVehicle(Guid id) =>
    _orderRepository
      .GetOrderById(id)
      .ToResult(HttpStatusCode.NotFound.ToString())
      .Check(order => order.RemoveVehicle())
      .EnvelopeAsOk();

  [HttpPut]
  [Route("{id:Guid}/items")]
  public ActionResult UpdateItems(Guid id, ChangeItemsDto input) =>
    _orderRepository
      .GetOrderById(id)
      .ToResult(HttpStatusCode.NotFound.ToString())
      .Check(order => order.UpdateItems(input.Items))
      .EnvelopeAsOk();

  private static ReadOrderDto ToReadOrderDto(IOrder order) => new(order);
}