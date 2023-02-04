using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChildPrefab : MonoBehaviour
{

    [SerializeField]
    List<GameObject> Prefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        var randomPrefab = Prefabs[Random.Range(0, Prefabs.Count)];
        Instantiate(randomPrefab, transform.position, transform.rotation, transform);
    }

}
