using Com.IsartDigital.PaperMan;
using Com.IsartDigital.PaperMan.Sound;

public class EndAnimTrigger : Interactable
{
    protected override void PlayerEntered()
    {
        base.PlayerEntered();
        EndVideoManager.instance.StartVideo();
        Player.Instance.SetModVoid();
        AudioManager.instance.OnGameEnd();
    }
}
