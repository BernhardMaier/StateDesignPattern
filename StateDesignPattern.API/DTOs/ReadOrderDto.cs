using System;
using System.Collections.Generic;

namespace StateDesignPattern.API.DTOs
{
  public class ReadOrderDto
  {
    public Guid Id { get; set; }
    public string CurrentState { get; init; } = string.Empty;
    public string Customer { get; init; } = string.Empty;
    public string Vehicle { get; init; } = string.Empty;
    public List<string> Items { get; init; } = new();
  }
}