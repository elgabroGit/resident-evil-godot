using Godot;
using System;

public partial class CharacterPickState : State
{
    [Export] private CanvasLayer choisePanel;
    private Character player;
    private bool choise = false;
    private Item item;
    private MeshInstance3D model;
    Button btnYes;
    Button btnNo;
    Control controlNode;
    
    protected override void EnterState()
    {
        player = (Character)characterNode;
        item = player.itemToPick;
        GD.Print(player.itemToPick);
        btnYes = GetNode<Button>("CanvasLayer/Control/Panel/HBoxContainer/ButtonConfirm");
        btnNo = GetNode<Button>("CanvasLayer/Control/Panel/HBoxContainer/ButtonDeny");
        controlNode = GetNode<Control>("CanvasLayer/Control");

        

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
        btnYes.Pressed += HandleButtonConfirmation;
        btnNo.Pressed += HandleButtonDecline; 
        DisplayChoise();
        
        if(player.IsInventoryFull())
        {
            characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
        }
        else
        {
            characterNode.AnimPlayerNode.Play("pick");
        }            
    }

    public void ItemToInventory()
    {
        player.AddItem(player.itemToPick);
        player.itemToPick.QueueFree();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (choise)
        {
            ItemToInventory();
        }
        characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
    }

    protected override void ExitState()
    {  
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        btnYes.Pressed -= HandleButtonConfirmation;
        btnNo.Pressed -= HandleButtonDecline; 
        controlNode.Hide();
    }

    public void DisplayChoise()
    {
        GetTree().Paused = true;
        controlNode.Show();
        GD.Print("Prova");
    }

    private void HandleButtonDecline()
    {
        GetTree().Paused = false;
        choise = false;
        GD.Print("NO, oggetto non raccolto");
        controlNode.Hide();
        characterNode.StateMachineNode.SwitchState<CharacterIdleState>(); // Passa allo stato inattivo senza raccogliere l'oggetto
    }

    private void HandleButtonConfirmation()
    {
        GetTree().Paused = false;
        choise = true;
        GD.Print("SI, oggetto raccolto");
        controlNode.Hide();
        // L'animazione si occuperà di chiamare HandleAnimationFinished e di aggiungere l'oggetto
    }
}
