using CSharpFunctionalExtensions;
using FluentAssertions;
using StateDesignPattern.Logic.Interfaces;
using StateDesignPattern.Logic.OrderStates;
using Xunit;

namespace StateDesignPattern.Tests.OrderStates
{
  public class CompletedTests
  {
    [Fact]
    public void Property_name_returns_class_name()
    {
      var completedState = new Completed();

      completedState.Name.Should().Be(nameof(Completed));
    }

    [Fact]
    public void Property_type_returns_correct_enum_value()
    {
      var completedState = new Completed();

      completedState.Type.Should().Be(OrderStateType.Completed);
    }

    public class Activate
    {
      private readonly IOrderState _completedState;

      public Activate()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.Activate(
          null!,
          null!);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State can not be changed to 'Active'.");
      }
    }

    public class Complete
    {
      private readonly IOrderState _completedState;

      public Complete()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.Complete(string.Empty, 0, null!);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State is already 'Completed'.");
      }
    }

    public class Cancel
    {
      private readonly IOrderState _completedState;

      public Cancel()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.Cancel(null!);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State can not be changed to 'Canceled'.");
      }
    }

    public class UpdateItems
    {
      private readonly IOrderState _completedState;

      public UpdateItems()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.UpdateItems(Result.Success);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Items can not be updated in state 'Completed'.");
      }
    }

    public class ChangeCustomer
    {
      private readonly IOrderState _completedState;

      public ChangeCustomer()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.ChangeCustomer(Result.Success);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Customer can not be changed in state 'Completed'.");
      }
    }

    public class RemoveCustomer
    {
      private readonly IOrderState _completedState;

      public RemoveCustomer()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.RemoveCustomer(Result.Success);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Customer can not be removed in state 'Completed'.");
      }
    }

    public class ChangeVehicle
    {
      private readonly IOrderState _completedState;

      public ChangeVehicle()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.ChangeVehicle(Result.Success);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Vehicle can not be changed in state 'Completed'.");
      }
    }

    public class RemoveVehicle
    {
      private readonly IOrderState _completedState;

      public RemoveVehicle()
      {
        _completedState = new Completed();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _completedState.RemoveVehicle(Result.Success);

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Vehicle can not be removed in state 'Completed'.");
      }
    }
  }
}