using Godot;

// Base Class for every state
public abstract partial class State : Node
{
    [Signal] public delegate void StateTransitionEventHandler(State state, string newStateName);

    public abstract void Enter();

    public virtual void Exit()
    {

    }

    public virtual void Update(double delta)
    {

    }

    public virtual void PhysicsUpdate(double delta)
    {

    }

    public void TransitionToState(State currentState, string newStateName)
    {
        EmitSignal(SignalName.StateTransition, currentState, newStateName);
    }
}