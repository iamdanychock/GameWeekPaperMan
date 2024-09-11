using Com.IsartDigital.PaperMan;

public class Killzone : Interactable
{
    protected override void PlayerEntered()
    {
        base.PlayerEntered();
        Player.Instance.Kill();
    }
}
