using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPScript : MonoBehaviour
{
    public int value;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent.GetComponent<XPHolderScript>().pendingXP += value;
        Destroy(gameObject);
    }
}
