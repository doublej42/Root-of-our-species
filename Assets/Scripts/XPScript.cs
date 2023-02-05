using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPScript : MonoBehaviour
{
    public int value;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent.GetComponent<XPHolderScript>().pendingXP += value;
        Explode();
    }

    [SerializeField]
    private List<AudioClip> Sounds = new List<AudioClip>();

    void Explode()
    {
        var randomClip = Sounds[Random.Range(0, Sounds.Count)];
        AudioSource.PlayClipAtPoint(randomClip, transform.position);
        Destroy(gameObject);
    }
}
