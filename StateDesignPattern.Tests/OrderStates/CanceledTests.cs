using CSharpFunctionalExtensions;
using FluentAssertions;
using StateDesignPattern.Logic;
using StateDesignPattern.Logic.Interfaces;
using StateDesignPattern.Logic.OrderStates;
using Xunit;

namespace StateDesignPattern.Tests.OrderStates
{
  public class CanceledTests
  {
      [Fact]
      public void Property_name_returns_class_name()
      {
        var canceledState = new Canceled();
        
        canceledState.Name.Should().Be(nameof(Canceled));
      }
    
      [Fact]
      public void Property_type_returns_correct_enum_value()
      {
        var canceledState = new Canceled();
        
        canceledState.Type.Should().Be(OrderStateType.Canceled);
      }

    public class Activate
    {
      private readonly IOrderState _canceledState;
      public Activate()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.Activate(
          null!, 
          null!);
      
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State can not be changed to 'Active'.");
      }
    }
    
    public class Complete
    {
      private readonly IOrderState _canceledState;
      public Complete()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.Complete(
          null!, 
          null!);
      
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State can not be changed to 'Completed'.");
      }
    }
    
    public class Cancel
    {
      private readonly IOrderState _canceledState;
      public Cancel()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.Cancel(
          null!, 
          null!);
      
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State is already 'Canceled'.");
      }
    }
    
    public class UpdateItems
    {
      private readonly IOrderState _canceledState;
      public UpdateItems()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.UpdateItems(Result.Success);
        
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Items can not be updated in state 'Canceled'.");
      }
    }
    
    public class ChangeCustomer
    {
      private readonly IOrderState _canceledState;
      public ChangeCustomer()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.ChangeCustomer(Result.Success);
        
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Customer can not be changed in state 'Canceled'.");
      }
    }
    
    public class RemoveCustomer
    {
      private readonly IOrderState _canceledState;
      public RemoveCustomer()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.RemoveCustomer(Result.Success);
        
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Customer can not be removed in state 'Canceled'.");
      }
    }
    
    public class ChangeVehicle
    {
      private readonly IOrderState _canceledState;
      public ChangeVehicle()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.ChangeVehicle(Result.Success);
        
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Vehicle can not be changed in state 'Canceled'.");
      }
    }
    
    public class RemoveVehicle
    {
      private readonly IOrderState _canceledState;
      public RemoveVehicle()
      {
        _canceledState = new Canceled();
      }

      [Fact]
      public void returns_the_state_itself_and_failure_result()
      {
        var (state, result) = _canceledState.RemoveVehicle(Result.Success);
        
        state.Type.Should().Be(OrderStateType.Canceled);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Vehicle can not be removed in state 'Canceled'.");
      }
    }
  }
}