using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    List<GameObject> bulletPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var randomPrefab = bulletPrefabs[Random.Range(0, bulletPrefabs.Count)];
        Instantiate(randomPrefab, transform.position, transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
