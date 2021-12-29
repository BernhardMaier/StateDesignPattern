using System;
using System.Collections.Generic;
using StateDesignPattern.Logic.Interfaces;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace StateDesignPattern.API.Features.Orders.DTOs;

public class ReadOrderDto : IHasGuid
{
  public ReadOrderDto(IOrder order)
  {
    Id = order.Id;
    CurrentState = order.CurrentState;
    Customer = order.Customer;
    Vehicle = order.Vehicle;
    Items = order.Items;
  }
    
  public Guid Id { get; }
  public string CurrentState { get; }
  public string Customer { get; }
  public string Vehicle { get; }
  public List<string> Items { get; }
}