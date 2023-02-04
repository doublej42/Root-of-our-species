using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    private GameObject SmallerGameObject = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Astroid Hit {collision.gameObject.name}");
        if(SmallerGameObject != null)
        {
            var numberOfChilderen = Random.Range(1, 4);
            for(var i = 0; i < numberOfChilderen;i++) {
                Instantiate(SmallerGameObject, transform.position, transform.rotation, transform.parent);
            }
        }
        Destroy(gameObject);

    }
}
