﻿using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
	public PlayerValues Player;
    public CollisionSoul soulAttack;
	public GameObject[] colliders;

	public string[] PrimaryCombos;
	public string[] SecondaryCombos;
	public int PrimaryCount = 0;
	public int SecondaryCount = 0;

	private AnimatorStateInfo CurrentState;

	public float leewayTime = 4.5f;
	public float colliderTime;
    public float blockTime;
    // Use this for initialization
    void Start()
    {
        colliderTime = 0.5f;
        Player.SecondaryAttack = false;
        Player.PrimaryAttack = false;
        Player.isAttacking = false;
        for (int i = 0; i < colliders.Length; i++ )
			colliders[i].GetComponent<BoxCollider>().enabled = false;
        soulAttack.GetComponent<CollisionSoul>();
    }

    // Update is called once per frame
    void Update()
    {
		ActionUpdate();
    }

    void FixedUpdate()
    {
		if (Player.SecondaryAttack || Player.PrimaryAttack)
		{
			if (colliderTime > 0 && (Player.PrimaryAttack == true || Player.SecondaryAttack == true))
				colliderTime -= Time.deltaTime;
			else if (colliderTime <= 0.0f)
				colliderTime = 0;
			
			if(colliderTime == 0)
			{
				//for (int i = 0; i < colliders.Length; i++)
				//	colliders[i].GetComponent<BoxCollider>().enabled = false;

				Player.SecondaryAttack = false;
				Player.PrimaryAttack = false;
                Player.isAttacking = false;

                PrimaryCount = 0;
				SecondaryCount = 0;
			}
		}
        

        // --- ELEMENT ATTACK CODE ----



    }

	void ActionUpdate()
	{
      
        //-----BLOCK CODE-----//
        if (Player.isGrounded && Input.GetButtonDown(Player.Joystick + "Block"))
        {      
            Player.isBlocking = true;
            Player.PlayerAnimation.SetBool("isBlocking", Player.isBlocking);
            Player.PlayerAnimation.SetTrigger("Block");
        }
		else if (Input.GetButtonUp(Player.Joystick + "Block"))
		{
            Player.isBlocking = false;
            Player.PlayerAnimation.SetBool("isBlocking", Player.isBlocking);
        }
		else if(!Player.isGrounded && Input.GetButtonDown(Player.Joystick + "Block"))
        {
			//Airdoge
		}

        //-----------------------//
        //-----ATTACK SCRIPT-----//
        //-----------------------//
		if (!Player.isBlocking)
        {
			//-----LIGHT ATTACK CODE-----//
			if (Input.GetButtonDown(Player.Joystick + "Primary") && Player.SecondaryAttack == false)// punch
            {
                //Setting Attack bool to true
                Player.isAttacking = true;
                Player.PrimaryAttack = true;

				//Activating the animation trigger
				Player.PlayerAnimation.SetTrigger("PrimaryTrigger");
			}


			//-----HEAVY ATTACK CODE-----//


			if (Input.GetButtonDown(Player.Joystick + "Secondary") && Player.PrimaryAttack == false)// punch
			{
                Player.isAttacking = true;
                Player.SecondaryAttack = true;

				Player.PlayerAnimation.SetTrigger("SecondaryTrigger");
			}
        }
	}

    public void CollidersOn()
    {
		Debug.Log("Colliders On");
		if (Player.PrimaryAttack)
		{
			colliders[0].GetComponent<BoxCollider>().enabled = true;
			colliders[0].tag = "PrimaryAttack";
		}
		else if (Player.SecondaryAttack)
		{
			colliders[0].GetComponent<BoxCollider>().enabled = true;
			colliders[0].tag = "SecondaryAttack";
		}
    }

    public void CollidersOff()
    {
		Player.SecondaryAttack = false;
		Player.PrimaryAttack = false;
		Player.isAttacking = false;

		Debug.Log("Colliders Off");

        colliders[0].GetComponent<BoxCollider>().enabled = false;
    }
}
