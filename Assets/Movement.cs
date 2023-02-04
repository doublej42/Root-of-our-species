using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public GameObject Ship;

    public float MoveSpeed = 1;

    private Controls Controls;

    private Vector2 MoveDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        Controls.Gameplay.Disable();
    }

    private void Awake()
    {
        Controls = new Controls();
        Controls.Gameplay.Move.performed += Move_performed;
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
        Debug.Log("Moving " + MoveDirection.x + "^" + MoveDirection.y);

    }

    void FixedUpdate()
    {
        Ship.transform.position += new Vector3(MoveSpeed * Time.deltaTime * MoveDirection.x, MoveSpeed * Time.deltaTime * MoveDirection.y);
    }
}