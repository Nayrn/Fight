using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    public GameObject projBall;
    private Collider projCollider;
    public PlayerValues player;
    public GameObject Air;
    public GameObject Earth;
    public GameObject Water;
    public GameObject Fire;
    private float dist;


    // Use this for initialization
    void Start ()
    {
        projCollider = projBall.GetComponentInParent<Collider>();
   
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        dist = Vector3.Distance(player.transform.position, projBall.transform.position);
        if (Input.GetMouseButtonDown(0) || Input.GetAxis(player.Joystick + "Projectile") > 0) // leftClick or Y on controller
        {
            Debug.Log("HadOUUUKen");
            projBall.SetActive(true);
            projBall.transform.position = player.transform.position;
            projBall.transform.forward = player.transform.forward;
         

            //checking elements
            if (player.Attribute == ElemTrait.AIR && player.m_soulAmount > 10.0f)
            {
                Air.SetActive(true);
            }
            if (player.Attribute == ElemTrait.EARTH && player.m_soulAmount > 10.0f)
            {
                Earth.SetActive(true);
            }
            if (player.Attribute == ElemTrait.WATER && player.m_soulAmount > 10.0f)
            {
                Water.SetActive(true);
            }
            if (player.Attribute == ElemTrait.FIRE && player.m_soulAmount > 10.0f)
            {
                Fire.SetActive(true);
            }           


        }

        if (projBall.activeSelf)
        {
            projBall.transform.position -= Vector3.forward * Time.deltaTime * 5;          
        }

        if (dist > 10)
            projBall.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player.Opponent)
        {
            projBall.SetActive(false);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player.Opponent)
        {
            projBall.SetActive(false);
        }
    }
}
