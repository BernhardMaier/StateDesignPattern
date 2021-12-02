using System.Collections.Generic;

namespace StateDesignPattern.API.Features.Orders.DTOs
{
  public class ChangeItemsDto
  {
    public List<string> Items { get; set; } = new();
  }
}