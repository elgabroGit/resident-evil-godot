using Godot;
using System;

public partial class MenuInventory : Panel
{
    [Export] public Character character;

    public override void _Ready()
    {
        base._Ready();
        Hide();
    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(GameConstants.INPUT_MENU))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        if(GameManager.Instance.gameState == GameConstants.GameState.MENU)
        {
            Hide();
            GameManager.Instance.gameState = GameConstants.GameState.RUNNING;
            GetTree().Paused = false;
            
        }else{
            Show();
            GameManager.Instance.gameState = GameConstants.GameState.MENU;
            GetTree().Paused = true;
            ProcessMode = ProcessModeEnum.Always;
        }
    }

}
