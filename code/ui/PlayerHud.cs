using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class PlayerHud : Panel
{
	private Label HealthIcon;
	private Label Health;

	public PlayerHud()
	{
		StyleSheet.Load( "/ui/PlayerHud.scss" );

		Panel healthBack = Add.Panel( "healthBG" );
		
		HealthIcon = healthBack.Add.Label( "Health: ", "healthText2" );
		Health = healthBack.Add.Label( "0", "healthText" );
	}

	public override void Tick()
	{
		base.Tick();

		if ( Local.Pawn == null ) return;

		Health.Text = $"{Local.Pawn.Health.CeilToInt()}";
	}
}
