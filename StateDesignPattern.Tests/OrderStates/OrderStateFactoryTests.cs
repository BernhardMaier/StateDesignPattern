using FluentAssertions;
using StateDesignPattern.Logic.OrderStates;
using Xunit;

namespace StateDesignPattern.Tests.OrderStates
{
  public class OrderStateFactoryTests
  {
    [Theory]
    [InlineData(-1, nameof(New), OrderStateType.New)]
    [InlineData(0, nameof(New), OrderStateType.New)]
    [InlineData(1, nameof(Active), OrderStateType.Active)]
    [InlineData(2, nameof(Completed), OrderStateType.Completed)]
    [InlineData(3, nameof(Canceled), OrderStateType.Canceled)]
    public void Create_returns_correct_class(
      int stateTypeId, string expectedStateName, OrderStateType expectedStateType)
    {
      var state = OrderStateFactory.Create(stateTypeId);
      state.Name.Should().Be(expectedStateName);
      state.Type.Should().Be(expectedStateType);
    }
  }
}