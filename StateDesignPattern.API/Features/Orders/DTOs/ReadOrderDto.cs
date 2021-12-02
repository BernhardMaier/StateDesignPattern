using System;
using System.Collections.Generic;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.API.Features.Orders.DTOs
{
  public class ReadOrderDto
  {
    public ReadOrderDto(IOrder order)
    {
      Id = order.Id;
      CurrentState = order.CurrentState;
      Customer = order.Customer;
      Vehicle = order.Vehicle;
      Items = order.Items;
    }
    
    public Guid Id { get; set; }
    public string CurrentState { get; init; } = string.Empty;
    public string Customer { get; init; } = string.Empty;
    public string Vehicle { get; init; } = string.Empty;
    public List<string> Items { get; init; } = new();
  }
}