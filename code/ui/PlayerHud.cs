using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class PlayerHud : Panel
{
	private Label HealthIcon;
	private Label Health;
	private Label AmmoIcon;
	private Label Ammo;

	public PlayerHud()
	{
		StyleSheet.Load( "/ui/PlayerHud.scss" );

		Panel healthBack = Add.Panel( "healthBG" );
		Panel ammoBack = Add.Panel( "ammoBG" );
		
		HealthIcon = healthBack.Add.Label( "Health: ", "healthText" );
		Health = healthBack.Add.Label( "100", "text" );

		AmmoIcon = ammoBack.Add.Label( "Ammo: ", "ammoText" );
		Ammo = ammoBack.Add.Label( "0", "text" );
	}

	public override void Tick()
	{
		base.Tick();

		if ( Local.Pawn == null ) return;

		var weapon = Local.Pawn.ActiveChild as Weapon;
		SetClass( "active", weapon != null );

		if ( weapon == null ) return;

		Health.Text = $"{Local.Pawn.Health.CeilToInt()}";
		Ammo.Text = $"{weapon.AmmoCount}";
	}
}
