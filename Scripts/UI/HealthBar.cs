using Godot;
using System;

public partial class HealthBar : ProgressBar
{
    protected MenuInventory mainMenu;
    protected StyleBoxFlat styleBox = new();
    public override void _Ready()
    {
        base._Ready();
        mainMenu = (MenuInventory) GetParent();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Value = (double) mainMenu.character.HealthValue / mainMenu.character.MaxHealthValue;
        
        if(Value > 0.5d)
        {
            styleBox.BgColor = Colors.Green;
            
        }else if(Value > 0.3d)
        {
            styleBox.BgColor = Colors.Yellow;
        }
        else{
            styleBox.BgColor = Colors.Red;
        }
        ShaderMaterial shaderMaterial = (ShaderMaterial) Material;
        shaderMaterial.SetShaderParameter("value", Value / MaxValue);
        AddThemeStyleboxOverride("fill", styleBox);
        GD.Print(Value);
    }
}
