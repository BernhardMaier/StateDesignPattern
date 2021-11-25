using System.Collections.Generic;

namespace StateDesignPattern.API.DTOs
{
  public class ChangeItemsDto
  {
    public List<string> Items { get; set; } = new();
  }
}