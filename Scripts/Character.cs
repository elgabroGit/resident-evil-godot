using Godot;
using System;

public partial class Character : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode {get; private set; } 
    [Export] public StateMachine StateMachineNode {get; private set; }
    [Export] public Controller ControllerNode {get; private set; }
    [Export] public Area3D EnemyDetectionArea {get; private set; }

}
