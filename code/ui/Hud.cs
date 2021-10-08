using Sandbox;
using Sandbox.UI;

namespace ChewyDeathmatch
{
	public partial class DeathmatchHud : Sandbox.HudEntity<RootPanel>
	{
		public DeathmatchHud()
		{
			if ( !IsClient ) return;

			RootPanel.AddChild<PlayerHud>();
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<KillFeed>();
			RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		}
	}
}
