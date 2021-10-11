using Sandbox;
using System;
using System.Linq;

namespace ChewyDeathmatch
{
	partial class DeathmatchPlayer : Player
	{
		public Clothing.Container Clothing = new();
		private DamageInfo lastDamage;

		[Net, Predicted] public ICamera MainCamera { get; set; }

		public ICamera LastCamera { get; set; }

		public DeathmatchPlayer()
		{
			Inventory = new Inventory( this );
		}

		public DeathmatchPlayer( Client cl ) : this()
		{
			Clothing.LoadFromClient( cl );
		}

		public override void Spawn()
		{
			MainCamera = new FirstPersonCamera();
			LastCamera = MainCamera;

			Clothing.DressEntity( this );

			base.Spawn();
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			Controller = new DeathmatchWalkController();
			Animator = new StandardPlayerAnimator();

			MainCamera = LastCamera;
			Camera = MainCamera;

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			Clothing.DressEntity( this );

			Inventory.Add( new WaterPipe(), true );
			Inventory.Add( new Pistol() );

			base.Respawn();
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			SimulateActiveChild( cl, ActiveChild );

			if ( IsServer && Input.Pressed( InputButton.Slot1 ) )
			{
				Inventory.SetActiveSlot( 0, false );
			}

			if ( IsServer && Input.Pressed( InputButton.Slot2 ) )
			{
				Inventory.SetActiveSlot( 1, false );
			}
		}

		public override void OnKilled()
		{
			base.OnKilled();
			
			//LastCamera = MainCamera;
			//MainCamera = new SpectateRagdollCamera();
			//Camera = MainCamera;
			Controller = null;

			EnableAllCollisions = false;
			EnableDrawing = false;

			Inventory.DeleteContents();
		}

		public override void TakeDamage( DamageInfo info )
		{
			if ( GetHitboxGroup( info.HitboxIndex ) == 1 )
			{
				info.Damage *= 3.0f;
			}

			lastDamage = info;

			TookDamage( lastDamage.Flags, lastDamage.Position, lastDamage.Force );

			base.TakeDamage( info );
		}

		[ClientRpc]
		public void TookDamage( DamageFlags damageFlags, Vector3 forcePos, Vector3 force )
		{
		}

		[ServerCmd( "inventory_current" )]
		public static void SetInventoryCurrent( string entName )
		{
			var target = ConsoleSystem.Caller.Pawn;
			if ( target == null ) return;

			var inventory = target.Inventory;
			if ( inventory == null )
				return;

			for ( int i = 0; i < inventory.Count(); ++i )
			{
				var slot = inventory.GetSlot( i );
				if ( !slot.IsValid() )
					continue;

				if ( !slot.ClassInfo.IsNamed( entName ) )
					continue;

				inventory.SetActiveSlot( i, false );

				break;
			}
		}
	}
}
