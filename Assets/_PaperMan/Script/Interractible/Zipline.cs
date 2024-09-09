using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    //PALCEHORLDLER 
    bool activated = false;


    public void Activate() { activated = true; }

    private void Update()
    {
        if (!activated)
            return;

        transform.position += Vector3.up * 3 * Time.deltaTime;
    }
}
