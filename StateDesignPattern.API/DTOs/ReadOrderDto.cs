using System;
using System.Collections.Generic;

namespace StateDesignPattern.API.DTOs
{
  public class ReadOrderDto
  {
    public Guid Id { get; set; }
    public string CurrentState { get; set; }
    public string Customer { get; set; }
    public string Vehicle { get; set; }
    public List<string> Items { get; set; }
  }
}