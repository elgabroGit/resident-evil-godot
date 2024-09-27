using Godot;
using System;

public abstract partial class Entity : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode {get; private set; } 
    [Export] public StateMachine StateMachineNode {get; private set; }
    [Export] public Area3D EnemyDetectionArea {get; private set; }
    [Export] public Controller ControllerNode {get; private set; }

    [ExportGroup("Stats")]
    [Export] public int MaxHealthValue {get; private set; }
    public int HealthValue; 
    public int damageReceived = 0;
    

    public float originalSphereRadius;
    public Node3D enemyGrabbed;
    public Vector3 lastGrabPositionBodySaved;

    public override void _Ready()
    {
        HealthValue = MaxHealthValue;
        SphereShape3D sphere = (SphereShape3D) EnemyDetectionArea.GetChild<CollisionShape3D>(0).Shape;
        originalSphereRadius = sphere.Radius;
    }

    public override void _Process(double delta)
    {
        if(HealthValue <= 0)
        {
            Die();
        }
    }

    public void EnlargeDetectionArea()
    {
        SphereShape3D sphere = (SphereShape3D) EnemyDetectionArea.GetChild<CollisionShape3D>(0).Shape;
        sphere.Radius *= 1.1f;
        EnemyDetectionArea.GetChild<CollisionShape3D>(0).Shape = sphere;
    }

    public void RestoreDetectionArea()
    {
        SphereShape3D sphere = (SphereShape3D) EnemyDetectionArea.GetChild<CollisionShape3D>(0).Shape;
        sphere.Radius = originalSphereRadius;
        EnemyDetectionArea.GetChild<CollisionShape3D>(0).Shape = sphere;
    }

    public virtual void TakeDamage(Weapon weapon){}
    public virtual void Die(){}
}
