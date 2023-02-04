using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ToSpawn;
    [SerializeField]
    private GameObject Ship;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ToSpawn, new Vector3(Ship.transform.position.x+3, Ship.transform.position.y+3, 0), Ship.transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
