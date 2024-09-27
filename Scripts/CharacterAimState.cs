using Godot;
using System;

public partial class CharacterAimState : State
{
    [Export(PropertyHint.Range, "0,20,0.1")] public float lerp = 2;
    [Export] private WeaponManager weaponManager;
    private Node3D currentTarget = null;
    public static bool canAim = true;
    
    public override void _PhysicsProcess(double delta)
    {
        // Crea un vettore 3D per la direzione del movimento
        Vector3 direction3D = new Vector3(characterNode.ControllerNode.direction.X, 0, characterNode.ControllerNode.direction.Y);

        // Ottieni la telecamera attiva dal GameManager
        Camera3D activeCamera = GameManager.Instance.ActiveCamera;

        // Verifica se la telecamera attiva esiste

        // Ottieni la direzione "in avanti" e "a destra" della telecamera
        Vector3 cameraForward = activeCamera.GlobalTransform.Basis.Z.Normalized();
        Vector3 cameraRight = activeCamera.GlobalTransform.Basis.X.Normalized();

        // Trasforma il movimento del personaggio secondo la direzione della telecamera
        Vector3 moveDirection = (cameraRight * direction3D.X) + (cameraForward * direction3D.Z);

        // Calcola l'angolo target in base alla direzione di movimento relativa alla telecamera
        if (moveDirection != Vector3.Zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.X, moveDirection.Z);
            float currentAngle = characterNode.Rotation.Y;

            // Interpolazione dell'angolo con un fattore di velocità di rotazione
            float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, (float)delta * lerp);

            // Imposta la nuova rotazione interpolata
            characterNode.Rotation = new Vector3(0, smoothAngle, 0);
        }
        

    }

    public override void _Ready()
    {
        base._Ready();
        characterNode.EnemyDetectionArea.BodyEntered += HandleEnemyEntered;
        characterNode.EnemyDetectionArea.BodyExited += HandleEnemyExited;


    }

    public override void _Input(InputEvent @event)
    {
        if(characterNode.ControllerNode.shot)
        {
            canAim = false;

            if(weaponManager.currentWeapon.fireType == GameConstants.FireType.MANUAL)
            {
                characterNode.StateMachineNode.SwitchState<CharacterShotState>(); 
            }

            if(weaponManager.currentWeapon.fireType == GameConstants.FireType.AUTOMATIC)
            {
                characterNode.StateMachineNode.SwitchState<CharacterAutomaticShotState>(); 
            }
            
        }

        if(!characterNode.ControllerNode.aim)
        {
            canAim = true;
            characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
        }
    }

	protected override void EnterState()
	{
		base.EnterState();
		characterNode.AnimPlayerNode.Play(weaponManager.currentWeapon.animationIdle);
        if(canAim){ AimToLocalEnemy(); }
       
        
	}

    private void HandleEnemyEntered(Node3D body)
    {
        currentTarget = body;
    }

        private void HandleEnemyExited(Node3D body)
    {
        currentTarget = null;
    }

    private void AimToLocalEnemy()
    {
        if(currentTarget != null){
            Vector3 directionToEnemy = currentTarget.GlobalPosition - characterNode.GlobalPosition;
            directionToEnemy.Y = 0; // Mantieni solo l'asse orizzontale (X e Z)
            directionToEnemy = directionToEnemy.Normalized();

            // Calcola la rotazione necessaria per guardare verso il nemico lungo l'asse Y
            float targetRotationY = Mathf.Atan2(directionToEnemy.X, directionToEnemy.Z);

            // Applica la rotazione solo sull'asse Y
            characterNode.Rotation = new Vector3(0, targetRotationY, 0);
        }
    }

}
