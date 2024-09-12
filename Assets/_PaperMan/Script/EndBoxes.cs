using System.Collections;
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
        if (++disabledTvs != nbTvsToDestroy)
            return;


        StartCoroutine(ChangePOICoroutine());
    }

    IEnumerator ChangePOICoroutine()
    {
        Com.IsartDigital.PaperMan.Camera.Instance.ChangePOI(3, new Vector3(228, 40, -2));

        yield return new WaitForSeconds(1);

        
        animator.SetTrigger(animTriggerName);
        Com.IsartDigital.PaperMan.Camera.Instance.Shake(screenshakeDuration, screenshakeForce);
    }
}