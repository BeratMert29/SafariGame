using System.Collections.Generic;
using System;
using UnityEngine;

public class StateMachine
{
    public IState currentState;

    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> currentTransitions = new List<Transition>();
    private List<Transition> anyTransitions = new List<Transition>();


    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }

        currentState?.Tick();
    }


    public void SetState(IState state, int? value = null)
    {
        //if (state == currentState)
        //    return;

        currentState?.OnExit();

        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);
        if (currentTransitions == null)
            currentTransitions = EmptyTransitions;

        currentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (!transitions.TryGetValue(from.GetType(), out var _transitions))
        {
            _transitions = new List<Transition>();
            transitions[from.GetType()] = _transitions;
        }

        _transitions.Add(new Transition(to, predicate));
    }

    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        anyTransitions.Add(new Transition(state, predicate));
    }


#nullable enable
    public void TriggerActionTransition(IState to, int? value = null, IState? from = null)
    {
        if (from != null && currentState != from)
        {
            return;
        }
        SetState(to, value);
    }
#nullable disable

    private Transition GetTransition()
    {
        // Check any state transitions first, without calling triggers
        foreach (var transition in anyTransitions)
            if (transition.Condition())
                return transition;

        // Check current state transitions
        foreach (var transition in currentTransitions)
            if (transition.Condition())
                return transition;

        return null;
    }

    public class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

}
