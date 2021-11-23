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
      public void returns_expected_failure_result()
      {
        var result = _activeState.Activate("John Doe");

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
      public void returns_the_expected_failure_result_if_prerequisites_are_not_met()
      {
        var result = _activeState.Complete(string.Empty, 0, null!);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(
          "Customer must be set and at least one item must be present to change from 'Active' to 'Completed'.");
      }

      [Fact]
      public void returns_the_expected_success_result_if_prerequisites_are_met()
      {
        var result = _activeState.Complete(
          "John Doe", 1,
          () => Result.Success(new Invoice()));

        result.IsSuccess.Should().BeTrue();
        result.Value.State.Type.Should().Be(OrderStateType.Completed);
        result.Value.Invoice.Should().BeAssignableTo<IInvoice>();
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
      public void returns_the_expected_result()
      {
        var result = _activeState.Cancel();

        result.IsSuccess.Should().BeTrue();
        result.Value.Type.Should().Be(OrderStateType.Canceled);
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
      public void returns_the_expected_result()
      {
        var result = _activeState.UpdateItems(Result.Success);

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
      public void returns_the_expected_result()
      {
        var result = _activeState.ChangeCustomer(Result.Success);

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
      public void returns_the_expected_result()
      {
        var result = _activeState.RemoveCustomer(Result.Success);

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
      public void returns_the_expected_result()
      {
        var result = _activeState.ChangeVehicle(Result.Success);

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
      public void returns_the_expected_result()
      {
        var result = _activeState.RemoveVehicle(Result.Success);

        result.IsSuccess.Should().BeTrue();
      }
    }
  }
}