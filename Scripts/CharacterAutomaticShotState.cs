using Godot;
using System;

public partial class CharacterAutomaticShotState : State
{
	[Export] private WeaponManager weaponManager;
	[Export(PropertyHint.Range, "0,20,0.1")] public float lerp = 2;
	public Entity entity = null;

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

	protected override void EnterState()
	{
        FireShot();
	}

    public void FireShot()
    {
        characterNode.AnimPlayerNode.Play(weaponManager.currentWeapon.animationShot);
		weaponManager.currentWeapon.audioStreamPlayer3D.Play();

		// Attiva il raycast per il colpo
		weaponManager.currentWeapon.rayCast3D.Enabled = true;
		weaponManager.currentWeapon.rayCast3D.ForceRaycastUpdate();  
		entity = weaponManager.currentWeapon.rayCast3D.GetCollider() as Entity;
		
		if(entity != null){
			TriggerDamage(weaponManager.currentWeapon);
		}

		weaponManager.currentWeapon.reloadTimer.Timeout += HandleShotTimeout;
		weaponManager.currentWeapon.reloadTimer.Start();

        if(characterNode.ControllerNode.shooting == false)
        {
            weaponManager.currentWeapon.rayCast3D.Enabled = false;
            characterNode.StateMachineNode.SwitchState<CharacterAimState>();
        }
    }


    private void TriggerDamage(Weapon weapon)
	{
		entity.TakeDamage(weapon);
	}

	private void HandleShotTimeout()
	{
		weaponManager.currentWeapon.rayCast3D.Enabled = false;
		FireShot();
	}

    protected override void ExitState()
    {
        weaponManager.currentWeapon.reloadTimer.Timeout -= HandleShotTimeout;
    }
}
