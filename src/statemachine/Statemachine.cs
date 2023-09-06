using Godot;
using System.Collections.Generic;

// Handling the states of a statemachine
// States need to be children of the Statemachine Node
[GlobalClass]
public partial class Statemachine : Node
{
    [Export] private State InitalState;

    private Dictionary<string, State> states;
    private State currentState;

    public override void _Ready()
    {
        states = new Dictionary<string, State>();

        // Get all states
        foreach (Node child in GetChildren())
        {
            if (child is State state)
            {
                states.Add(state.Name.ToString().ToLower(), state);
                state.StateTransition += OnChildTransition;
            }
        }

        if (InitalState is not null)
        {
            currentState = InitalState;
            InitalState.Enter();
        }
    }

    public override void _Process(double delta)
    {
        if (currentState is null) return;

        currentState.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (currentState is null) return;

        currentState.PhysicsUpdate(delta);
    }

    private void OnChildTransition(State state, string newStateName)
    {
        if (currentState != state) return;

        State newState = states[newStateName.ToLower()];
        if (newState is null) return;

        currentState?.Exit();

        newState.Enter();

        currentState = newState;
    }
}