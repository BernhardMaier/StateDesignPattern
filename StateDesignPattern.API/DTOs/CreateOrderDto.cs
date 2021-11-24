using System.Collections.Generic;

namespace StateDesignPattern.API.DTOs
{
  public class CreateOrderDto
  {
    public string Customer { get; set; }
    public string Vehicle { get; set; }
  }
}