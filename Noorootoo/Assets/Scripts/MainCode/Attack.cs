using UnityEngine;
using System.Collections;

public struct Move
{
    public string name;

    public int damage;
    public ElemTrait element;

	public Collider strikeBox;
};

public class Attack : MonoBehaviour
{
	public PlayerValues Player;
	public GameObject[] colliders;

	public string[] PrimaryCombos;
	public string[] SecondaryCombos;
	public int PrimaryCount = 0;
	public int SecondaryCount = 0;

	private AnimatorStateInfo CurrentState;

	public float leewayTime = 4.5f;
	public float colliderTime;

    // Use this for initialization
    void Start()
    {
        colliderTime = 0.5f;
        Player.SecondaryAttack = false;
        Player.PrimaryAttack = false;
        Player.isAttacking = false;
        for (int i = 0; i < colliders.Length; i++ )
			colliders[i].GetComponent<BoxCollider>().enabled = false;
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
				for (int i = 0; i < colliders.Length; i++)
					colliders[i].GetComponent<BoxCollider>().enabled = false;

				Player.SecondaryAttack = false;
				Player.PrimaryAttack = false;
                Player.isAttacking = false;

                PrimaryCount = 0;
				SecondaryCount = 0;
			}
		}
    }

	void ActionUpdate()
	{
        //-----BLOCK CODE-----//
        if(Player.isGrounded && Input.GetButtonDown(Player.Joystick + "Block"))
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
		else
		{
			//Airdoge
		}

		if (!Player.isBlocking)
        {
			//-----LIGHT ATTACK CODE-----//
			if (Input.GetButtonDown(Player.Joystick + "Primary") && Player.SecondaryAttack == false)// punch
            {
                //Setting Attack bool to true
                Player.isAttacking = true;
                Player.PrimaryAttack = true;

				//Enabling colliders
				CollidersOn();

				//Activating the animation trigger
				Player.PlayerAnimation.SetTrigger("PrimaryTrigger");

				//Get the current state and set the time
				CurrentState = Player.PlayerAnimation.GetCurrentAnimatorStateInfo(0);
				colliderTime = CurrentState.normalizedTime % 1;

				//Aerial stasis
				if (!Player.isGrounded)
				{
                    Player.gravityEdit = 0.5f;

                    //if (!Player.AerialBool)
                    //{
                    //	Player.FrozenY = transform.position;
                    //	Player.AerialBool = true;
                    //}
                    //else
                    //	Player.FrozenY = transform.position;
                }
			}


			//-----HEAVY ATTACK CODE-----//


			if (Input.GetButtonDown(Player.Joystick + "Secondary") && Player.PrimaryAttack == false)// punch
			{
                Player.isAttacking = true;
                Player.SecondaryAttack = true;

				CollidersOn();

				Player.PlayerAnimation.SetTrigger("SecondaryTrigger");

				CurrentState = Player.PlayerAnimation.GetCurrentAnimatorStateInfo(0);

				colliderTime = CurrentState.normalizedTime % 1;

				if(!Player.isGrounded)
				{
                    Player.gravityEdit = 0.5f;
				}
					/*
					//-----Getting current Animation state-----//
					CurrentState = Player.PlayerAnimation.GetCurrentAnimatorStateInfo(0);

					//-----Setting attack to true OR increasing Attackcount-----//
					if (!Player.SecondaryAttack)
					{
						Player.SecondaryAttack = true;
						colliderTime = 3.0f;

						++SecondaryCount;
						// set colliders to active
						CollidersOn();
						//Kick Animation 
						Player.PlayerAnimation.SetTrigger("SecondaryTrigger");
					}
					else if (Player.SecondaryAttack && CurrentState.IsName(SecondaryCombos[SecondaryCount]))
					{
						++SecondaryCount;
						CollidersOn();
						Player.PlayerAnimation.SetTrigger("SecondaryTrigger");
					}
						//-----------------------------------------------------------//

					//-----Setting the animation time and triggers if not at max combo-----//
					if (SecondaryCount < SecondaryCombos.Length)
					{

						if (CurrentState.IsName(SecondaryCombos[SecondaryCount]))
							colliderTime = CurrentState.length + leewayTime;
					}
					else if (CurrentState.IsName(SecondaryCombos[SecondaryCombos.Length - 1]))
						colliderTime = 0;

					//-----Resetting the count to 0-----//
					if (SecondaryCount == SecondaryCombos.Length && CurrentState.IsName(SecondaryCombos[SecondaryCombos.Length - 1]))// && CurrentState.IsName(SecondaryCombos[SecondaryCount]))
						SecondaryCount = 0;
					*/

			}
        }
	}

    public void CollidersOn()
    {
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
        // switch colliders on for this amount of time
    }

    public void CollidersOff()
    {
        if (Player.PrimaryAttack)
            colliders[0].GetComponent<BoxCollider>().enabled = false;
        else if (Player.SecondaryAttack)
            colliders[0].GetComponent<BoxCollider>().enabled = false;
        // switch colliders on for this amount of time
    }
}
