using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Ship;
    [SerializeField]
    private SpriteRenderer Jets;


   

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
    

    private Controls Controls;

    private Vector2 MoveDirection = Vector2.zero;

    [Header("Container")]
    [SerializeField]
    private GameObject BuletHolder;


    [Header("Prefab")]   
    [SerializeField]
    private GameObject WeaponPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnEnable()
    {
        Controls.Enable();
        Jets.enabled = false;
    }

    private void OnDisable()
    {
        Controls.Disable();
    }

    private void Awake()
    {
        Controls = new Controls();
        Controls.Gameplay.Move.performed += Move_performed;
        Controls.Gameplay.Move.canceled += context => { MoveDirection = Vector2.zero; Jets.enabled = false; };
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
        Jets.enabled = true;
        //Debug.Log("Moving " + MoveDirection.x + "^" + MoveDirection.y);

    }

    void Update()
    {
        //Ship.transform.rotation
        if (MoveDirection != Vector2.zero)
        {
            var maxRotationAngle = 180 * Time.deltaTime / RotationSpeed;
            var currentAngle = MakeAnglePossitive(Ship.transform.rotation.eulerAngles.z);
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
            Ship.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //Ship.transform.up = MoveDirection;
            Ship.transform.position += new Vector3(MoveSpeed * Time.deltaTime * MoveDirection.x, MoveSpeed * Time.deltaTime * MoveDirection.y);
            Camera.transform.position = new Vector3(Ship.transform.position.x,Ship.transform.position.y,Camera.transform.position.z);
        }

        TimeToNextFire -= Time.deltaTime;
        if (TimeToNextFire <= 0)
        {
            var newbullet  = Instantiate(WeaponPrefab, Ship.transform.position, Ship.transform.rotation,BuletHolder.transform);
            newbullet.GetComponent<BulletScript>().speed = FireSpeed;
            TimeToNextFire = FireRate;
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