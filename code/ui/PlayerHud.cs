using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class PlayerHud : Panel
{
	private Label FragsIcon;
	private Label Frags;
	private Label DeathsIcon;
	private Label Deaths;
	private Label HealthIcon;
	private Label Health;
	private Label AmmoIcon;
	private Label Ammo;

	public PlayerHud()
	{
		StyleSheet.Load( "/ui/PlayerHud.scss" );

		Panel fragsBack = Add.Panel( "fragsBG" );
		Panel deathsBack = Add.Panel( "deathsBG" );
		Panel healthBack = Add.Panel( "healthBG" );
		Panel ammoBack = Add.Panel( "ammoBG" );

		FragsIcon = fragsBack.Add.Label( "Kills: ", "text2" );
		Frags = fragsBack.Add.Label( "0", "text" );

		DeathsIcon = deathsBack.Add.Label( "Deaths: ", "text2" );
		Deaths = deathsBack.Add.Label( "0", "text" );
		
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

		Frags.Text = $"{Local.Client.GetInt( "Kills" )}";

		Deaths.Text = $"{Local.Client.GetInt( "Deaths" )}";

		Health.Text = $"{Local.Pawn.Health.CeilToInt()}";

		Ammo.Text = $"{weapon.AmmoCount}";
	}
}
