using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    private GameObject SmallerGameObject = null;

    [SerializeField]
    public int size;

    [SerializeField]
    private GameObject xp;

    public float angularVelocity = 50f;

 


    //[SerializeField]
    //public float speed = 1.0f;

    //public Vector2 velocity;

    private float invulnerableTime = 0.25f;
    
    private int MaxAsteroids = 50;

    private GameObject Ship;
    
    private float MaxDistance = 50f;
    [SerializeField]
    private float MinDistance = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        var rigid = GetComponent<Rigidbody2D>();
        //rotate it to a random spot and start it spinning
        transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        rigid.angularVelocity = angularVelocity;
        //Debug.Log($"Asteroid Up {transform.up.x}, {transform.up.y}, {transform.up.z}");
        //rigid.velocity = velocity;
        Ship = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        invulnerableTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position,Ship.transform.position) > MaxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invulnerableTime > 0)
        {
            return;
        }
        //Debug.Log($"Astroid Hit {collision.gameObject.name}");
        if (collision.gameObject.layer == gameObject.layer)
        {

            if(collision.gameObject.TryGetComponent<AsteroidScript>(out var targetScript) && targetScript.size < size)
            {
                return; //ignore hitting smaller items;
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            //Debug.LogWarning("spawning XP");
            var xpHolder = GameObject.FindGameObjectWithTag("XPHolder");
            var xpGem = Instantiate(xp,transform.position,transform.rotation,xpHolder.transform);
            xpGem.GetComponent<XPScript>().value = size;
        }
        if(SmallerGameObject != null && transform.parent.childCount < MaxAsteroids)
        {
            var numberOfChilderen = Random.Range(2, 6);
            
            float radius = 0.5f * size;
        
            for (var i = 0; i < numberOfChilderen;i++) {
                float angle = i * Mathf.PI * 2f / numberOfChilderen;
                var newPos = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius,0);
                if (Vector3.Distance(newPos, Ship.transform.position) > MinDistance)
                {
                    var newItem = Instantiate(SmallerGameObject, newPos, transform.rotation, transform.parent);
                    var vectorToShip3D = (Ship.transform.position - newPos).normalized;
                    var vectorToShip2D = new Vector2(vectorToShip3D.x, vectorToShip3D.y);
                    newItem.GetComponent<Rigidbody2D>().velocity = vectorToShip2D;
                }
                
            }
            
        }
        Destroy(gameObject);


    }
}
