using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class WeaponMenu : Panel
{
    private bool IsOpen = false;
    private TimeSince LastOpen;

	public WeaponMenu()
	{
		StyleSheet.Load( "/ui/WeaponMenu.scss" );

        Panel MenuPanel = Add.Panel( "menu" );
        Label Title = MenuPanel.Add.Label( "Choose Weapons", "title" );
	}

	public override void Tick()
	{
		base.Tick();

        if ( Input.Pressed(InputButton.Menu) && LastOpen >= 0.1f )
        {
            IsOpen = !IsOpen;
            LastOpen = 0;
        }

        SetClass("open", IsOpen);
	}
}
