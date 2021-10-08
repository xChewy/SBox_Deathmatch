using Sandbox;
using System;
using System.Linq;

namespace ChewyDeathmatch
{
	partial class DeathmatchPlayer : Player
	{
		public Clothing.Container Clothing = new();

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
			Clothing.DressEntity( this );

			base.Spawn();
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			Controller = new WalkController();

			Animator = new StandardPlayerAnimator();

			Camera = new FirstPersonCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			Clothing.DressEntity( this );

			Inventory.Add( new Pistol(), true );

			base.Respawn();
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			SimulateActiveChild( cl, ActiveChild );
		}

		public override void OnKilled()
		{
			base.OnKilled();

			EnableDrawing = false;

			Inventory.DeleteContents();
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
