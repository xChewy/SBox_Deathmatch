using Sandbox;

// Taken from https://github.com/Facepunch/sandbox/

[Library( "weapon_pistol", Title = "Waterpipe Shotgun", Spawnable = true )]
partial class WaterPipe : Weapon
{
	public override string ViewModelPath => "weapons/rust_shotgun/v_rust_shotgun.vmdl";

	public override int MaxAmmoCount => 1;
	public override float PrimaryRate => 15.0f;
	public override float SecondaryRate => 1.0f;
	public override float ReloadTime => 5.1f;

	public TimeSince TimeSinceDischarge { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_shotgun/rust_shotgun.vmdl" );

		AmmoCount = MaxAmmoCount;
	}

	public override bool CanPrimaryAttack()
	{
		return base.CanPrimaryAttack() && Input.Pressed( InputButton.Attack1 );
	}

	public override void AttackPrimary()
	{
		if ( AmmoCount != 0 )
		{
			TimeSincePrimaryAttack = 0;
			TimeSinceSecondaryAttack = 0;
			
			(Owner as AnimEntity)?.SetAnimBool( "b_attack", true );

			ShootEffects();
			PlaySound( "rust_shotgun.shoot" );

			ShootBullets( 6, 0.20f, 1.5f, 18.0f, 3.0f );

			AmmoCount = AmmoCount - 1;
		}
		else
		{
			//PlaySound( "rust_smg.dryfire" );
		}
	}

	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetParam( "holdtype", 3 );
		anim.SetParam( "aimat_weight", 1.0f );
		anim.SetParam( "holdtype_handedness", 0 );
	}
}
