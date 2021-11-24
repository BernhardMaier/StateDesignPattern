using System.Collections.Generic;

namespace StateDesignPattern.API.DTOs
{
  public class UpdateOrderDto
  {
    public string Customer { get; set; }
    public string Vehicle { get; set; }
    public List<string> Items { get; set; }
  }
}