using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ToSpawn;
    [SerializeField]
    private GameObject Ship;

    [SerializeField]
    private float frequency = 1.0f;
    private float spawnTimeLeft = 0;

    [SerializeField]
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //spawn();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimeLeft -= Time.deltaTime;
        if (spawnTimeLeft <= 0)
        {
            spawnTimeLeft = frequency;
            spawn();
        }
    }

    private void spawn()
    {
        float radius = 20f;
        float angle = Random.Range(0, Mathf.PI * 2);
        var newPos = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        var newItem = Instantiate(ToSpawn, newPos, Ship.transform.rotation);
        newItem.GetComponent<Rigidbody2D>().velocity = (Ship.transform.position - newPos).normalized * speed;
    }
}
