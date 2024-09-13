using Com.IsartDigital.PaperMan;

public class EndAnimTrigger : Interactable
{
    protected override void PlayerEntered()
    {
        base.PlayerEntered();
        EndVideoManager.instance.StartVideo();
        Player.Instance.SetModVoid();
    }
}
