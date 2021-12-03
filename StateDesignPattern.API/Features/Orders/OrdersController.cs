using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
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
    private static readonly IList<IOrder> Orders = new List<IOrder>();

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
    public ActionResult<ReadOrderDto> CreateOrder(CreateOrderDto input) =>
      Order
        .Create()
        .Check(order => order.ChangeCustomer(input.Customer))
        .Check(order => order.ChangeVehicle(input.Vehicle))
        .Tap(order => Orders.Add(order))
        .Bind(order => order.Map(ToReadOrderDto))
        .EnvelopeAsCreated(dto => dto.Id.ToString());

    [HttpGet]
    [Route("{id:Guid}")]
    public ActionResult<ReadOrderDto> GetOrder(Guid id) =>
      GetOrderById(id)
        .Map(ToReadOrderDto)
        .EnvelopeAsOkObject();

    [HttpPut]
    [Route("{id:Guid}/customer")]
    public ActionResult ChangeCustomer(Guid id, ChangeCustomerDto input) =>
      GetOrderById(id)
        .ChangeCustomer(input.Customer)
        .EnvelopeAsOk();

    [HttpDelete]
    [Route("{id:Guid}/customer")]
    public ActionResult RemoveCustomer(Guid id) =>
      GetOrderById(id)
        .RemoveCustomer()
        .EnvelopeAsOk();

    [HttpPut]
    [Route("{id:Guid}/vehicle")]
    public ActionResult ChangeVehicle(Guid id, ChangeVehicleDto input) =>
      GetOrderById(id)
        .ChangeVehicle(input.Vehicle)
        .EnvelopeAsOk();

    [HttpDelete]
    [Route("{id:Guid}/vehicle")]
    public ActionResult RemoveVehicle(Guid id) =>
      GetOrderById(id)
        .RemoveVehicle()
        .EnvelopeAsOk();

    [HttpPut]
    [Route("{id:Guid}/items")]
    public ActionResult UpdateItems(Guid id, ChangeItemsDto input) =>
      GetOrderById(id)
        .UpdateItems(input.Items)
        .EnvelopeAsOk();

    private IOrder GetOrderById(Guid id) =>
      Orders.FirstOrDefault(o => o.Id == id) ?? NoOrder.Instance(id);

    private static ReadOrderDto ToReadOrderDto(IOrder order) =>
      new(order);
  }
}