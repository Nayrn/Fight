using UnityEngine;
using System.Collections;

public class CollisionSoul : MonoBehaviour
{
    public PlayerValues player1;
    public PlayerValues player2;
    private bool soulUp;
    private bool soulUp2;
    private float soulTime = 0.5f;
    private float soulTime2 = 0.5f;
    // Use this for initialization
    void Start ()
    {
        soulUp = false;
        soulUp2 = false;
    }
	
	// Update is called once per frame
	void Update ()
    {

        
       

        if (soulUp == true)
        {
            soulTime -= Time.deltaTime;
            player1.m_soulAmount = player1.m_soulAmount + 20.0f * Time.deltaTime;     
        }

        
        if (soulTime < 0)
        {
            soulUp = false;
            soulTime = 0.5f;
        }


        if (soulUp2 == true)
        {
            soulTime2 -= Time.deltaTime;
            player2.m_soulAmount = player2.m_soulAmount + 20.0f * Time.deltaTime;
        }


        if (soulTime < 0)
        {
            soulUp2 = false;
            soulTime = 0.5f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // fill up soul for p1 and stop p2 movement
        if (col.gameObject.tag == "PrimaryAttack")
        {
            //player1.m_soulAmount = player1.m_soulAmount + 16.0f;
            soulUp = true;
            player2.particle.Play();
            //-----WAS TRYING TO KEEP THE PLAYER STILL  
            player2.MakePlayerStunned(0.5f);
        }
     
        

        if (col.gameObject.tag == "SecondaryAttack")
        {
            soulUp = true;
            player2.MakePlayerStunned(0.5f);
        }
        



        //fill up soul for p2 and stop p1 movements
        if (col.gameObject.tag == "PrimaryP2")
        {
            soulUp2 = true;
            player1.MakePlayerStunned(0.5f);
            player1.particle.Play();
        }
      
        if (col.gameObject.tag == "SecondaryP2")
        {
            soulUp2 = true;
            player1.MakePlayerStunned(0.5f);
        }

    }


}
