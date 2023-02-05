using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    private GameObject SmallerGameObject = null;

    [SerializeField]
    public float angularVelocity = 50f;

    [SerializeField]
    public int size;
 


    //[SerializeField]
    //public float speed = 1.0f;

    //public Vector2 velocity;

    private float invulnerableTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var rigid = GetComponent<Rigidbody2D>();
        //rotate it to a random spot and start it spinning
        transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        rigid.angularVelocity = angularVelocity;
        //Debug.Log($"Asteroid Up {transform.up.x}, {transform.up.y}, {transform.up.z}");
        //rigid.velocity = velocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        invulnerableTime -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invulnerableTime > 0)
        {
            return;
        }
        Debug.Log($"Astroid Hit {collision.gameObject.name}");
        if (collision.gameObject.layer == gameObject.layer)
        {

            if(collision.gameObject.TryGetComponent<AsteroidScript>(out var targetScript) && targetScript.size < size)
            {
                return; //ignore hitting smaller items;
            }
        }
        if(SmallerGameObject != null)
        {
            var numberOfChilderen = Random.Range(2, 6);
            var ship = GameObject.FindGameObjectWithTag("Player");
            float radius = 1f;
    
            for (var i = 0; i < numberOfChilderen;i++) {
                float angle = i * Mathf.PI * 2f / numberOfChilderen;
                var newPos = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius,0);
                var newItem = Instantiate(SmallerGameObject, newPos, transform.rotation, transform.parent);
     
                var vectorToShip3D = (ship.transform.position - newPos).normalized;
                var vectorToShip2D = new Vector2(vectorToShip3D.x, vectorToShip3D.y);
                newItem.GetComponent<Rigidbody2D>().velocity = vectorToShip2D;
            }
        }
        Destroy(gameObject);

    }
}
