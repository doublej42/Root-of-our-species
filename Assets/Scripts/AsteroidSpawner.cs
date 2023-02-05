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
    [SerializeField]
    private int MaxAsteroids = 50;

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
        if (transform.childCount >= MaxAsteroids)
        {
            return;
        }
        float radius = 15f;
        float angle = Random.Range(0, Mathf.PI * 2);
        var newPos = Ship.transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        Debug.Log($"New Pos {newPos.x} , {newPos.y}");
        var newItem = Instantiate(ToSpawn, newPos, Ship.transform.rotation,transform);
        newItem.GetComponent<Rigidbody2D>().velocity = (Ship.transform.position - newPos).normalized * speed;
    }
}
