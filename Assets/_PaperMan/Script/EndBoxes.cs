using System.Collections;
using UnityEngine;

public class EndBoxes : MonoBehaviour
{
    [SerializeField] private int nbTvsToDestroy = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private string animTriggerName = "Destroy";
    private int disabledTvs = 0;
    [SerializeField] private ParticleSystem[] boxesParticles;
    [SerializeField] private GameObject[] boxesToDisable;

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
        Com.IsartDigital.PaperMan.Camera.Instance.ChangePOI(3, new Vector3(254.2427f, 38.05067f, -2.329961f));

        yield return new WaitForSeconds(1);

        // explose particles
        foreach (var item in boxesParticles)
        {
            item.Play();
        }
        foreach (var item in boxesToDisable)
        {
            item.SetActive(false);
        }


        animator.SetTrigger(animTriggerName);
        Com.IsartDigital.PaperMan.Camera.Instance.Shake(screenshakeDuration, screenshakeForce);
    }
}