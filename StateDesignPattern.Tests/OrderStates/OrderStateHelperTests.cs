using FluentAssertions;
using StateDesignPattern.Logic.OrderStates;
using Xunit;

namespace StateDesignPattern.Tests.OrderStates
{
  public class OrderStateHelperTests
  {
    [Theory]
    [InlineData(-1, nameof(New), OrderStateType.New)]
    [InlineData(0, nameof(New), OrderStateType.New)]
    [InlineData(1, nameof(Active), OrderStateType.Active)]
    [InlineData(2, nameof(Completed), OrderStateType.Completed)]
    [InlineData(3, nameof(Canceled), OrderStateType.Canceled)]
    public void Instantiate_creates_correct_classes(
      int stateTypeId, string expectedStateName, OrderStateType expectedStateType)
    {
      var state = OrderStateHelper.Instantiate(stateTypeId);
      state.Name.Should().Be(expectedStateName);
      state.Type.Should().Be(expectedStateType);
    }
  }
}