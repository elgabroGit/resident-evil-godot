using Godot;
using System;
using System.Diagnostics;

public partial class WeaponManager : Node3D
{
    [Export] public Weapon currentWeapon;
    [Export] Weapon[] weaponList;
    private int weaponIndex;
    private Character characterNode;

    public override void _Ready()
    {
        base._Ready();
        SetWeapon();
        weaponIndex = 0;
        characterNode = GetOwner<Character>();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if(characterNode.ControllerNode.shot || characterNode.ControllerNode.aim){
            return;
        }
        if(Input.IsActionJustPressed(GameConstants.INPUT_SWITCH_UP)){
            weaponIndex = Math.Clamp(weaponIndex + 1, 0, weaponList.Length - 1);
        }
        if(Input.IsActionJustPressed(GameConstants.INPUT_SWITCH_DOWN)){
            weaponIndex = Math.Clamp(weaponIndex - 1, 0, weaponList.Length - 1);
        }
        currentWeapon = weaponList[weaponIndex];
        SetWeapon();
    }

    public void SetWeapon()
    {
        foreach(Weapon w in weaponList){
            w.Hide();
        }
        currentWeapon.Show();
    }

}
