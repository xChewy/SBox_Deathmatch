using Sandbox;

// Taken from https://github.com/Facepunch/sandbox/

[Library( "weapon_pistol", Title = "Pistol", Spawnable = true )]
partial class Pistol : Weapon
{
	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

	public override int MaxAmmoCount => 10;
	public override float PrimaryRate => 15.0f;
	public override float SecondaryRate => 1.0f;
	public override float ReloadTime => 3.4f;

	public TimeSince TimeSinceDischarge { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );

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
			PlaySound( "rust_pistol.shoot" );

			ShootBullet( 0.05f, 1.5f, 11.5f, 3.0f );

			AmmoCount = AmmoCount - 1;
		}
		else
		{
			//PlaySound( "rust_smg.dryfire" );
		}
	}

	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetParam( "holdtype", 1 );
		anim.SetParam( "aimat_weight", 1.0f );
		anim.SetParam( "holdtype_handedness", 0 );
	}
}
