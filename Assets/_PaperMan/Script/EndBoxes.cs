using UnityEngine;

public class EndBoxes : MonoBehaviour
{
    [SerializeField] private int nbTvsToDestroy = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private string animTriggerName = "Destroy";
    private int disabledTvs = 0;

    public void DisableTv()
    {
        if (++disabledTvs == nbTvsToDestroy)
            animator.SetTrigger(animTriggerName);
    }
}
