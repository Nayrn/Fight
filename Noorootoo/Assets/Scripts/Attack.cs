using UnityEngine;
using System.Collections;


public class Attack : MonoBehaviour
{
    public JoystickNum Joystick;
    public GameObject[] colliders;
    PlayerValues thisPlayer;
    public bool isPunching;
    public bool isKicking;
    public float colliderTime;
    public Animator anim;
    public GameObject child;
    // Use this for initialization
    void Start()
    {
        colliderTime = 0.5f;
        isKicking = false;
        isPunching = false;
        colliders[0].GetComponent<BoxCollider>().enabled = false;
        colliders[1].GetComponent<BoxCollider>().enabled = false;
        colliders[2].GetComponent<BoxCollider>().enabled = false;
        colliders[3].GetComponent<BoxCollider>().enabled = false;
        anim = child.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E) || Input.GetButton(Joystick + "Punch"))           // punch
        {
            // set colliders to active
            isPunching = true;
            Debug.Log("isPunching");
            CollidersOn();
            // punch animation  
            anim.SetBool("isPunching", true);
        }

        if (Input.GetKey(KeyCode.Q)|| Input.GetButton(Joystick + "Kick"))   // Kick
        {
            // set colliders to active
            isKicking = true;
            CollidersOn();
            // kick animation
            anim.SetBool("isKicking", true);
        }

        if (isKicking|| isPunching)
        {
            colliderTime -= Time.deltaTime;


            if (colliderTime < 0.0f)
            {
                colliders[0].GetComponent<BoxCollider>().enabled = false;
                colliders[1].GetComponent<BoxCollider>().enabled = false;
                colliders[2].GetComponent<BoxCollider>().enabled = false;

                colliderTime = 0.5f;
                isKicking = false;
                isPunching = false;

                //turn animation off
                anim.SetBool("isKicking", false);
                anim.SetBool("isPunching", false);
            }
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "LeftHand" || col.gameObject.tag == "RightHand")
        {
            thisPlayer.m_Health = thisPlayer.m_Health - 10;
            // hit animation
            Debug.Log("col with hands");
            anim.SetBool("isHit", true);

        }

        if (col.gameObject.tag == "LeftFoot" || col.gameObject.tag == "RightFoot")
        {
            thisPlayer.m_Health = thisPlayer.m_Health - 20;
            // hit animation
            Debug.Log("col with feet");
            anim.SetBool("isHit", true);

        }

    }

    public void CollidersOn()
    {
        if (isKicking == true)
        {
            colliders[1].GetComponent<BoxCollider>().enabled = true;
            colliders[2].GetComponent<BoxCollider>().enabled = true;
            

        }

        if (isPunching == true)
        {
            colliders[0].GetComponent<BoxCollider>().enabled = true;           
        }
        // switch colliders on for this amount of time



    }

    void FixedUpdate()
    {
        if (colliderTime < 0.0f)
        {
            colliders[0].GetComponent<BoxCollider>().enabled = false;
            colliders[1].GetComponent<BoxCollider>().enabled = false;
            colliders[2].GetComponent<BoxCollider>().enabled = false;
            colliders[3].GetComponent<BoxCollider>().enabled = false;

            colliderTime = 0.5f;
            isKicking = false;
            isPunching = false;

            //turn animation off
            anim.SetBool("isKicking", false);
            anim.SetBool("isPunching", false);
        }
    }
}
