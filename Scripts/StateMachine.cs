using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
	[Export] private Node currentState;
	[Export] private State[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

	public void SwitchState<T>() 
	{
		State newState = states.Where((state) => state is T).FirstOrDefault();
		
		if(newState == null) { return; }

		if(currentState is T) { return; }

		if(!newState.CanTransition()) { return; }

		currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
		currentState = newState;
		currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
	}
}
