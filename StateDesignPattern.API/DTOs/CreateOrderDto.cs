namespace StateDesignPattern.API.DTOs
{
  public class CreateOrderDto
  {
    public string Customer { get; set; } = string.Empty;
    public string Vehicle { get; set; } = string.Empty;
  }
}