using CSharpFunctionalExtensions;
using FluentAssertions;
using StateDesignPattern.Logic.Interfaces;
using StateDesignPattern.Logic.OrderStates;
using Xunit;

namespace StateDesignPattern.Tests.OrderStates
{
  public class NewTests
  {
    [Fact]
    public void Property_name_returns_class_name()
    {
      var newState = new New();

      newState.Name.Should().Be(nameof(New));
    }

    [Fact]
    public void Property_type_returns_correct_enum_value()
    {
      var newState = new New();

      newState.Type.Should().Be(OrderStateType.New);
    }

    public class Activate
    {
      private readonly IOrderState _newState;

      public Activate()
      {
        _newState = new New();
      }

      [Fact]
      public void returns_the_expected_failure_result_if_prerequisites_are_not_met()
      {
        var result = _newState.Activate(string.Empty);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Customer must be set to change from 'New' to 'Active'.");
      }

      [Fact]
      public void returns_the_expected_success_result_if_prerequisites_are_met()
      {
        var result = _newState.Activate("John Doe");

        result.IsSuccess.Should().BeTrue();
        result.Value.Type.Should().Be(OrderStateType.Active);
      }
    }

    public class Complete
    {
      private readonly IOrderState _newState;

      public Complete()
      {
        _newState = new New();
      }

      [Fact]
      public void returns_the_expected_failure_result()
      {
        var result = _newState.Complete(string.Empty, 0, null!);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State can not be changed to 'Complete'.");
      }
    }

    public class Cancel
    {
      private readonly IOrderState _newState;

      public Cancel()
      {
        _newState = new New();
      }

      [Fact]
      public void returns_the_expected_success_result_if_prerequisites_are_met()
      {
        var result = _newState.Cancel();

        result.IsSuccess.Should().BeTrue();
        result.Value.Type.Should().Be(OrderStateType.Canceled);
      }
    }

    public class UpdateItems
    {
      private readonly IOrderState _newState;

      public UpdateItems()
      {
        _newState = new New();
      }

      [Fact]
      public void returns_the_expected_failure_result()
      {
        var result = _newState.UpdateItems(Result.Success);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Items can not be updated in state 'New'.");
      }
    }

    public class ChangeCustomer
    {
      private readonly IOrderState _newState;

      public ChangeCustomer()
      {
        _newState = new New();
      }

      [Fact]
      public void returns_the_expected_result()
      {
        var result = _newState.ChangeCustomer(Result.Success);

        result.IsSuccess.Should().BeTrue();
      }
    }

    public class RemoveCustomer
    {
      private readonly IOrderState _newState;

      public RemoveCustomer()
      {
        _newState = new New();
      }


      [Fact]
      public void returns_the_expected_result()
      {
        var result = _newState.RemoveCustomer(Result.Success);

        result.IsSuccess.Should().BeTrue();
      }
    }

    public class ChangeVehicle
    {
      private readonly IOrderState _newState;

      public ChangeVehicle()
      {
        _newState = new New();
      }


      [Fact]
      public void returns_the_expected_result()
      {
        var result = _newState.ChangeVehicle(Result.Success);

        result.IsSuccess.Should().BeTrue();
      }
    }

    public class RemoveVehicle
    {
      private readonly IOrderState _newState;

      public RemoveVehicle()
      {
        _newState = new New();
      }


      [Fact]
      public void returns_the_expected_result()
      {
        var result = _newState.RemoveVehicle(Result.Success);

        result.IsSuccess.Should().BeTrue();
      }
    }
  }
}