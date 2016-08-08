using UnityEngine;
using System.Collections;

public class CollisionSoul : MonoBehaviour
{
    public PlayerValues player1;
    public PlayerValues player2;

    public bool p1IsHit;
    public bool p2IsHit;
	// Use this for initialization
	void Start ()
    {
        p1IsHit = false;
        p2IsHit = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(player2.m_soulAmount);
 

	}

    void OnTriggerEnter(Collider col)
    {
        // fill up soul for p1
        if (col.gameObject.tag == "PrimaryAttack")
        {
            p2IsHit = true;
            player1.m_soulAmount = player1.m_soulAmount + 16.0f;
        }
 


        //fill up soul for p2
        if (col.gameObject.tag == "PrimaryP2")
        {
            p1IsHit = true;
            player2.m_soulAmount = player2.m_soulAmount + 16.0f;
        }



    }
}
