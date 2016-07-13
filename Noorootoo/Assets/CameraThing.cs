using UnityEngine;
using System.Collections;

public class CameraThing : MonoBehaviour {

    private float m_horizontal;
    private float m_vertical;
    public float m_speed;
    Rigidbody rigidBody;

    private CursorLockMode lockMode;

    public float horizontalSpeed = 2.0f;


    // Use this for initialization
    void Start ()
    {
        m_speed = 8.0f;
        rigidBody = GetComponent<Rigidbody>();
        lockMode = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        MoveTurn();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchCursorMode();
        }
        
        Cursor.lockState = lockMode;

    }

    void MoveTurn()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(m_horizontal, 0.0f, m_vertical);
        movement *= m_speed * Time.deltaTime;
        rigidBody.transform.Translate(movement);


        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);
    }

    void SwitchCursorMode()
    {
        switch (Cursor.lockState)
        {
            case CursorLockMode.Locked:
                lockMode = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case CursorLockMode.None:
                lockMode = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }
}
