using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Test : MonoBehaviour
{
    [SerializeField]
    OrganMask wantedCurrent;
    [SerializeField]
    OrganMask wantedOriginal;
    [SerializeField]
    CustomerSpriteTable tbl;

    private void Update()
    {
        GetComponent<SpriteRenderer>().sprite = tbl.GetSprite(wantedCurrent, wantedOriginal);
    }
}
