using Sandbox;

// Taken from https://github.com/Facepunch/sandbox/

[Library( "weapon_smg", Title = "SMG", Spawnable = true )]
partial class SMG : Weapon
{
	public override string ViewModelPath => "weapons/rust_smg/v_rust_smg.vmdl";

	public override int MaxAmmoCount => 30;
	public override float PrimaryRate => 11.0f;
	public override float SecondaryRate => 1.0f;
	public override float ReloadTime => 5.2f;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_smg/rust_smg.vmdl" );

		AmmoCount = MaxAmmoCount;
	}

	public override void AttackPrimary()
	{
		if ( AmmoCount != 0 )
		{
			TimeSincePrimaryAttack = 0;
			TimeSinceSecondaryAttack = 0;

			(Owner as AnimEntity)?.SetAnimBool( "b_attack", true );

			ShootEffects();
			PlaySound( "rust_smg.shoot" );

			ShootBullet( 0.1f, 1.5f, 9.0f, 3.0f );

			AmmoCount = AmmoCount - 1;
		}
		else
		{
			//PlaySound( "rust_smg.dryfire" );
		}
	}

	public override void AttackSecondary()
	{

	}

	[ClientRpc]
	protected override void ShootEffects()
	{
		Host.AssertClient();

		Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );
		Particles.Create( "particles/pistol_ejectbrass.vpcf", EffectEntity, "ejection_point" );

		if ( Owner == Local.Pawn )
		{
			new Sandbox.ScreenShake.Perlin( 0.5f, 4.0f, 1.0f, 0.5f );
		}

		ViewModelEntity?.SetAnimBool( "fire", true );
		CrosshairPanel?.CreateEvent( "fire" );
	}

	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetParam( "holdtype", 2 );
		anim.SetParam( "aimat_weight", 1.0f );
	}
}
