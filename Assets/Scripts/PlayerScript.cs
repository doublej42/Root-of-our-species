using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerScript : MonoBehaviour
{
    //[SerializeField]
    //private GameObject Ship;
    [SerializeField]
    private SpriteRenderer Jets;
    [SerializeField]
    private SpriteRenderer Graphics;



    [SerializeField] 
    private GameObject Camera;

    [Header("Speeds")]
    [SerializeField]
    private float MoveSpeed = 1;
    [SerializeField]
    private float RotationSpeed = 1;
    [SerializeField]
    private float FireRate = 1;
    private float TimeToNextFire = 0;
    [SerializeField]
    private float FireSpeed = 0.2f;

    private bool dead = false;
    

    private Controls Controls;

    private Vector2 MoveDirection = Vector2.zero;

    [Header("Container")]
    [SerializeField]
    private GameObject BuletHolder;


    [Header("Prefab")]   
    [SerializeField]
    private GameObject WeaponPrefab;

    public GameObject XPHolder { get; private set; }

    private XPHolderScript XpHolderScript;

    //private Rigidbody2D Rigid;

    // Start is called before the first frame update
    void Start()
    {
        //Rigid = gameObject.GetComponent<Rigidbody2D>(); 
        XPHolder = GameObject.FindGameObjectWithTag("XPHolder");
        XpHolderScript = XPHolder.GetComponent<XPHolderScript>();
    }


    private void OnEnable()
    {
        if (Controls != null)
        {
            Controls.Enable();
        }
        
        Jets.enabled = false;
    }

    private void OnDisable()
    {
        if (Controls != null)
        {
            Controls.Disable();
        }
    }

    private void Awake()
    {
        Controls = new Controls();
        Controls.Gameplay.Move.performed += Move_performed;
        Controls.Gameplay.Move.canceled += context => { MoveDirection = Vector2.zero; Jets.enabled = false; };
        Controls.Gameplay.Exit.performed += Exit_performed;
    }

    private void Exit_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Exit");
        if (dead)
        {
            //Debug.Log("Calling quit");
            Application.Quit();
        }
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        if (dead) return;
        
        MoveDirection = context.ReadValue<Vector2>();
        //Rigid.velocity = context.ReadValue<Vector2>();
        Jets.enabled = true;
        //Debug.Log("Moving " + MoveDirection.x + "^" + MoveDirection.y);

    }

    void Update()
    {
        if (dead)
        {
            MoveDirection = Vector2.zero;
            return;
        }
        if (MoveDirection != Vector2.zero)
        {
            var maxRotationAngle = 180 * Time.deltaTime / RotationSpeed;
            var currentAngle = MakeAnglePossitive(transform.rotation.eulerAngles.z);
            var angle = MakeAnglePossitive(Mathf.Atan2(MoveDirection.y, MoveDirection.x) * Mathf.Rad2Deg - 90 );
            var diff = MakeAnglePossitive(currentAngle - angle);

            //do we have to animate this or can we just go with it?
            if (diff > maxRotationAngle)
            {
                if (diff > 180)
                {
                    angle = currentAngle + maxRotationAngle;
                }
                else
                {
                    angle = currentAngle - maxRotationAngle;
                }
            }

            //Debug.Log($"target {targetAngle} max rotation {maxRotationAngle} pre diff {currentAngle - angle} diff {diff}");
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //Ship.transform.up = MoveDirection;
            transform.position += new Vector3(MoveSpeed * Time.deltaTime * MoveDirection.x, MoveSpeed * Time.deltaTime * MoveDirection.y);
            Camera.transform.position = new Vector3(transform.position.x,transform.position.y,Camera.transform.position.z);
        }

        TimeToNextFire -= Time.deltaTime;
        if (TimeToNextFire <= 0)
        {
            var newbullet  = Instantiate(WeaponPrefab, transform.position, transform.rotation,BuletHolder.transform);
            newbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.up.x, transform.up.y) * FireSpeed;
            //newbullet.GetComponent<BulletScript>().speed = FireSpeed;
            TimeToNextFire = FireRate;
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var asteroidScript = collision.gameObject.GetComponent<AsteroidScript>();
            
            //xpHolderScript.pendingXP -= asteroidScript.size;
            var xpToLoose = ((XpHolderScript.pendingXP / 5) + 1) * asteroidScript.size;
            XpHolderScript.pendingXP -= xpToLoose;


            if (XpHolderScript.pendingXP < 0)
            {
                //Destroy(transform.GetChild(0).gameObject);
                Graphics.enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                dead = true;
            }
        }
        
    }
    private float MakeAnglePossitive(float input)
    {
        while (input < 0)
        {
            input += 360;
        }

        while (input >= 360)
        {
            input -= 360;
        }
        return input;
    }
}