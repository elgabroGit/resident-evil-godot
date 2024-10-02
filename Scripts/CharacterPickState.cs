using Godot;
using System;

public partial class CharacterPickState : State
{
    [Export] private CanvasLayer choisePanel;
    [Export] private AudioStreamPlayer open;
    [Export] private AudioStreamPlayer accept;
    [Export] private AudioStreamPlayer decline;
    private Character player;
    private bool choise = false;
    private Item item;
    private MeshInstance3D model;
    Button btnYes;
    Button btnNo;
    Control controlNode;
    
    float distanceInFrontOfCamera = 2.0f;
    
    protected override void EnterState()
    {
        player = (Character)characterNode;

        item = player.itemToPick;
        model = (MeshInstance3D)item.model.Duplicate();
        
        model.Visible = true;
        GetTree().Root.AddChild(model);   
        btnYes = GetNode<Button>("CanvasLayer/Control/Panel/VBoxContainer/ButtonConfirm");
        btnNo = GetNode<Button>("CanvasLayer/Control/Panel/VBoxContainer/ButtonDeny");
        controlNode = GetNode<Control>("CanvasLayer/Control");
        RichTextLabel label = GetNode<RichTextLabel>("CanvasLayer/Control/Panel/Label");
        
        Camera3D camera = GameManager.Instance.ActiveCamera;
        Vector3 cameraPosition = camera.GlobalPosition;
        Vector3 cameraForward = camera.GlobalTransform.Basis.Z.Normalized(); 

        model.GlobalPosition = cameraPosition - cameraForward * distanceInFrontOfCamera;
        
        // Imposta la scala iniziale a 0.1
        model.Scale = new Vector3(0.1f, 0.1f, 0.1f);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
        btnYes.Pressed += HandleButtonConfirmation;
        btnNo.Pressed += HandleButtonDecline; 

        label.Text = "Ho trovato [color=red]" + item.itemName + "[/color].\nRaccogli?";

        btnYes.GrabFocus();
        btnYes.Disabled = true;
        

        DisplayChoise();
        
        if (player.IsInventoryFull())
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
        model.QueueFree();
        controlNode.Hide();
    }

    public void DisplayChoise()
    {
        open.Play();
        GetTree().Paused = true;
        controlNode.Show();
        model.SetProcess(true);
        
    }

    private void HandleButtonDecline()
    {
        GetTree().Paused = false;
        choise = false;
        decline.Play();
        model.Visible = false;
        controlNode.Hide();
        characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
    }

    private void HandleButtonConfirmation()
    {
        GetTree().Paused = false;
        choise = true;
        accept.Play();
        model.Visible = false;
        controlNode.Hide();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (IsInstanceValid(model) && model != null && model.Visible)
        {
            if(model.Scale.X < 1f){
                model.Scale *= 1.07f;
            }else{
                btnYes.Disabled = false;
            }


            model.RotateY(Mathf.DegToRad(30 * (float)delta));  
        }
    }
}
