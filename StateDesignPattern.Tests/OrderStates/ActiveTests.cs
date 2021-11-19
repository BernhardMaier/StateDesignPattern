using CSharpFunctionalExtensions;
using FluentAssertions;
using StateDesignPattern.Logic;
using StateDesignPattern.Logic.Interfaces;
using StateDesignPattern.Logic.OrderStates;
using Xunit;

namespace StateDesignPattern.Tests.OrderStates
{
  public class ActiveTests
  {
    [Fact]
    public void Property_name_returns_class_name()
    {
      var activeState = new Active();

      activeState.Name.Should().Be(nameof(Active));
    }

    [Fact]
    public void Property_type_returns_correct_enum_value()
    {
      var activeState = new Active();

      activeState.Type.Should().Be(OrderStateType.Active);
    }

    public class Activate
    {
      private readonly IOrderState _activeState;

      public Activate()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _activeState.Activate(
          null!,
          null!);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State is already 'Active'.");
      }
    }

    public class Complete
    {
      private readonly IOrderState _activeState;

      public Complete()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result_if_prerequisites_are_not_met()
      {
        var (state, result) = _activeState.Complete(
          () => false,
          null!);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Preconditions not met to change from 'Active' to 'Completed'.");
      }

      [Fact]
      public void returns_the_completed_state_and_expected_success_result_if_prerequisites_are_met()
      {
        var (state, result) = _activeState.Complete(
          () => true,
          () => Result.Success(new Invoice()));

        state.Type.Should().Be(OrderStateType.Completed);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<Invoice>();
      }
    }

    public class Cancel
    {
      private readonly IOrderState _activeState;

      public Cancel()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result_if_prerequisites_are_not_met()
      {
        var (state, result) = _activeState.Cancel(
          () => false,
          null!);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Preconditions not met to change from 'Active' to 'Canceled'.");
      }

      [Fact]
      public void returns_the_canceled_state_and_success_result_if_prerequisites_are_met()
      {
        var (state, result) = _activeState.Cancel(
          () => true,
          () => Result.Success());

        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsSuccess.Should().BeTrue();
      }
    }

    public class UpdateItems
    {
      private readonly IOrderState _activeState;

      public UpdateItems()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_the_expected_result()
      {
        var (state, result) = _activeState.UpdateItems(Result.Success);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsSuccess.Should().BeTrue();
      }
    }

    public class ChangeCustomer
    {
      private readonly IOrderState _activeState;

      public ChangeCustomer()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_the_expected_result()
      {
        var (state, result) = _activeState.ChangeCustomer(Result.Success);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsSuccess.Should().BeTrue();
      }
    }

    public class RemoveCustomer
    {
      private readonly IOrderState _activeState;

      public RemoveCustomer()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_the_expected_result()
      {
        var (state, result) = _activeState.RemoveCustomer(Result.Success);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Customer can not be removed in state 'Active'.");
      }
    }

    public class ChangeVehicle
    {
      private readonly IOrderState _activeState;

      public ChangeVehicle()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_the_expected_result()
      {
        var (state, result) = _activeState.ChangeVehicle(Result.Success);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsSuccess.Should().BeTrue();
      }
    }

    public class RemoveVehicle
    {
      private readonly IOrderState _activeState;

      public RemoveVehicle()
      {
        _activeState = new Active();
      }

      [Fact]
      public void returns_the_state_itself_and_the_expected_result()
      {
        var (state, result) = _activeState.RemoveVehicle(Result.Success);

        state.Type.Should().Be(OrderStateType.Active);
        result.IsSuccess.Should().BeTrue();
      }
    }
  }
}