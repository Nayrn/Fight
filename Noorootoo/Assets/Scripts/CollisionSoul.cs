using UnityEngine;
using System.Collections;

public class CollisionSoul : MonoBehaviour
{
    public PlayerValues player1;
    public PlayerValues player2;

	// Use this for initialization
	void Start ()
    {
     
    }
	
	// Update is called once per frame
	void Update ()
    {
      
	}

    void OnTriggerEnter(Collider col)
    {
        // fill up soul for p1 and stop p2 movement
        if (col.gameObject.tag == "PrimaryAttack")
        {
            player1.m_soulAmount = player1.m_soulAmount + 16.0f;

            //-----WAS TRYING TO KEEP THE PLAYER STILL  
            player2.MakePlayerStunned(0.5f);
            //player1.MakePlayerStatic(2.5f);
        }
     
        

        if (col.gameObject.tag == "SecondaryAttack")
        {
            player1.m_soulAmount = player1.m_soulAmount + 16.0f;
            player2.isStasis = true;
        }
        



        //fill up soul for p2 and stop p1 movements
        if (col.gameObject.tag == "PrimaryP2")
        {           
            player2.m_soulAmount = player2.m_soulAmount + 16.0f;
            player1.isStasis = true;
        }
      
        if (col.gameObject.tag == "SecondaryP2")
        {
            player2.m_soulAmount = player2.m_soulAmount + 16.0f;
            player1.isStasis = true;    
        }

    }


}
