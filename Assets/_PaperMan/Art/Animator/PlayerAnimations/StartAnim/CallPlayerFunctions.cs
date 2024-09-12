using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPlayerFunctions : MonoBehaviour
{

    bool babbby = true;
    private void Update()
    {
        if (!babbby)
            return;

        Player.Instance.transform.localPosition = Vector3.zero;
    }

    public void SetModFlying()
    {
        Player.Instance.SetAnimFlying();
    }

    public void SetModIdle()
    {
        Player.Instance.SetAnimIdle();

        //Player.Instance.gameObject.SetActive(false);
    }

    public void Liberate()
    {
        Player.Instance.SetModNormal();
        babbby = false;
    }

    public void SmashGround()
    {
        Player.Instance.SmashGround();
    }
}
