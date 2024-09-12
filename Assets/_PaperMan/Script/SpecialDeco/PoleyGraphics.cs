using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.IsartDigital.PaperMan;

public class PoleyGraphics : Interactable
{
    public static List<PoleyGraphics> Poleys = new List<PoleyGraphics>();

    protected override void Start()
    {
        base.Start();

        Poleys.Add(this);
    }

    public void DesacOutline()
    {
        ChangeOutlineSizeAllChildrens(transform, 1);
    }

    private void OnDestroy()
    {
        Poleys.Remove(this);
    }
}
