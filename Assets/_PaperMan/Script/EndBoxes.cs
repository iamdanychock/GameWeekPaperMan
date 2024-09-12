using UnityEngine;

public class EndBoxes : MonoBehaviour
{
    [SerializeField] private int nbTvsToDestroy = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private string animTriggerName = "Destroy";
    private int disabledTvs = 0;

    [Space]
    [SerializeField] private float screenshakeDuration = .9f;
    [SerializeField] private float screenshakeForce = .5f;

    public void DisableTv()
    {
        if (++disabledTvs == nbTvsToDestroy)
        {
            animator.SetTrigger(animTriggerName);
            Com.IsartDigital.PaperMan.Camera.Instance.Shake(screenshakeDuration, screenshakeForce);
        }
    }
}