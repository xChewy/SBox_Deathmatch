using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChewyDeathmatch
{
	public partial class ChewyDeathmatch : Sandbox.Game
	{
		public DeathmatchHud DeathmatchHud;

		public ChewyDeathmatch()
		{
			if ( IsServer )
			{
				new DeathmatchHud();
			}

			if ( IsClient )
			{
				DeathmatchHud = new DeathmatchHud();
			}
		}

		[Event.Hotload]
		public void HotloadUpdate()
		{
			if ( !IsClient ) return;
			DeathmatchHud?.Delete();
			DeathmatchHud = new DeathmatchHud();
		}

		public override void ClientJoined( Client cl )
		{
			base.ClientJoined( cl );

			var player = new DeathmatchPlayer(cl);
			cl.Pawn = player;

			player.Respawn();
		}
	}

}
