using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    public GameObject projBall;
    private Collider projCollider;
    public PlayerValues Player;
    public GameObject Air;
    public GameObject Earth;
    public GameObject Water;
    public GameObject Fire;
    private float dist;
    public GameObject projDest;
    private Vector3 getPos;
    private float m_speed;
    // Use this for initialization
    void Start ()
    {
        projCollider = projBall.GetComponentInParent<Collider>();
        m_speed = 2.5f;
        projBall.SetActive(false);    
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        

        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown(Player.Joystick + "Projectile")) && projBall.activeSelf == false) // leftClick or Y on controller
        {
            projBall.SetActive(true);
            projBall.transform.position = Player.transform.position;
            GetPos();

            
            //checking elements
            if (Player.Attribute == ElemTrait.AIR)
            {
                Air.SetActive(true);
            }
            if (Player.Attribute == ElemTrait.EARTH )
            {
                Earth.SetActive(true);
            }
            if (Player.Attribute == ElemTrait.WATER)
            {
                Water.SetActive(true);
            }
            if (Player.Attribute == ElemTrait.FIRE)
            {
                Fire.SetActive(true);
            }           
            

            if(Player.Attribute == ElemTrait.UNASPECTED)
            {
                Air.SetActive(false);
                Earth.SetActive(false);
                Water.SetActive(false);
                Fire.SetActive(false);
                projBall.SetActive(false);
            }

      


        }

        if (projBall.activeSelf)
        {
            Debug.Log("Projectile");
            dist = Vector3.Distance(Player.transform.position, projBall.transform.position);
            projBall.transform.position = Vector3.Lerp(projBall.transform.position, getPos, Time.deltaTime * m_speed);
            if (dist > 6.5f)
                projBall.SetActive(false);

            float dist2 = Vector3.Distance(projBall.transform.position, Player.Opponent.transform.position);
            if (dist2 <= 0)
            {
                projBall.SetActive(false);
            }


            float dist3 = Vector3.Distance(projBall.transform.position, getPos);
            if (dist3 <= 0.5f)
                projBall.SetActive(false);

        }





    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player.Opponent)
        {
            projBall.SetActive(false);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == Player.Opponent)
        {
            projBall.SetActive(false);
        }
    }

    private void GetPos()
    {
        getPos = new Vector3(projDest.transform.position.x, projDest.transform.position.y, projDest.transform.position.z);
    }
}
