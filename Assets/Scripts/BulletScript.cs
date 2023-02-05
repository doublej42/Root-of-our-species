using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour 
{

    public float speed = 1.0f;
    public float lifeTimeRemaining = 3.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        //var rigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeRemaining -= Time.deltaTime;
        if (lifeTimeRemaining < 0)
        {
            //Destroy(this.gameObject);
            //return;
        }
        //Debug.Log(transform.up.x + ", " + transform.up.y + ", " + transform.up.z);
        //transform.position += transform.up * Time.deltaTime * speed;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Bullet collision {collision.gameObject.name}");
        Destroy(this.gameObject);
    }
}
