using Godot;
using System;

public partial class Controller : Node
{
    public Vector2 direction = new();
    public bool aim = false;
    public bool run = false;
    public bool interact = false;
    public bool shot = false;
    public bool moving = false;
    public bool shooting = false;

    private Vector2 previousDirection = Vector2.Zero; // Direzione precedente
    public Vector3 storedMoveDirection = Vector3.Zero;

    public override void _Input(InputEvent @event)
    {   
        // Chiama sistema operativo soltanto sugli input.
        direction = Input.GetVector(
            GameConstants.INPUT_LEFT, 
            GameConstants.INPUT_RIGHT, 
            GameConstants.INPUT_FORWARD, 
            GameConstants.INPUT_BACKWARD
        );

        aim = ToggleAction(GameConstants.INPUT_AIM);
        run = ToggleAction(GameConstants.INPUT_RUN);
        interact = ToggleAction(GameConstants.INPUT_INTERACT, true);
        shooting = ToggleAction(GameConstants.INPUT_INTERACT);

        if(aim){
            shot = ToggleAction(GameConstants.INPUT_INTERACT, true);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if(direction != Vector2.Zero){
            moving = true;
        }else{
            moving = false;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

    }

    // Metodo per controllare se la direzione è cambiata
    public bool HasDirectionChanged()
    {
        // Se la direzione attuale è zero, resetta la direzione memorizzata
        if (direction == Vector2.Zero)
        {
            previousDirection = Vector2.Zero;
            return false; // Non consideriamo un cambiamento di direzione
        }

        // Se la direzione è cambiata rispetto a quella precedente
        bool hasChanged = !direction.IsEqualApprox(previousDirection);

        // Aggiorna la direzione precedente se c'è stato un cambiamento
        if (hasChanged)
        {
            previousDirection = direction;
        }

        return hasChanged;
    }


    private bool ToggleAction(string actionString, bool oneclick = false)
    {
        if(oneclick)
        {
            if(Input.IsActionJustPressed(actionString)){
                return true;
            }else{
                return false;
            }
        }

        if(Input.IsActionPressed(actionString)){
            return true;
        }else{
            return false;
        }
    }
}
