using UnityEngine;
using System.Collections;

public struct Move
{
    public string name;

    public int damage;
    public ElemTrait element;
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
		for (int i = 0; i < colliders.Length; i++ )
			colliders[i].GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		ActionUpdate();
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

    void FixedUpdate()
    {
		if (Player.SecondaryAttack || Player.PrimaryAttack)
		{
			if (colliderTime > 0 && (Player.PrimaryAttack == true || Player.SecondaryAttack == true))
				colliderTime -= Time.deltaTime;
			else if (colliderTime <= 0.0f)
			{
				for (int i = 0; i < colliders.Length; i++)
					colliders[i].GetComponent<BoxCollider>().enabled = false;

				colliderTime = 0;

				Player.PlayerAnimation.SetLayerWeight(2, 0);

				Player.SecondaryAttack = false;
				Player.PrimaryAttack = false;
				PrimaryCount = 0;
				SecondaryCount = 0;

				//turn animation off
				Player.PlayerAnimation.SetBool("SecondaryAttack", Player.SecondaryAttack);
				Player.PlayerAnimation.SetBool("PrimaryAttack", Player.PrimaryAttack);
			}
		}
    }

	void ActionUpdate()
	{
        //-----BLOCK CODE-----//
        if(Player.isGrounded && Input.GetButtonDown(Player.Joystick + "Block"))
        {
            Player.isStasis = true;
            Player.isBlocking = true;

			Player.PlayerAnimation.SetBool("isBlocking", Player.isBlocking);
			Player.PlayerAnimation.SetTrigger("Block");
		}
		else if (Input.GetButtonUp(Player.Joystick + "Block"))
		{
			Player.isStasis = false;
			Player.isBlocking = false;

			Player.PlayerAnimation.SetBool("isBlocking", Player.isBlocking);
		}
		else
		{
			//Airdodge
		}

		if (!Player.isBlocking)
        {
			//-----LIGHT ATTACK CODE-----//
			if (Input.GetButtonDown(Player.Joystick + "Primary") && Player.SecondaryAttack == false)// punch
            {
				Player.PlayerAnimation.SetLayerWeight(2, 1);

				if (Player.isGrounded)
				{
					Player.PrimaryAttack = true;

					CollidersOn();

					Player.PlayerAnimation.SetTrigger("PrimaryTrigger");

					CurrentState = Player.PlayerAnimation.GetCurrentAnimatorStateInfo(0);

					colliderTime = CurrentState.normalizedTime % 1;
				}
				else
				{
					//Aerial attacks
				}
            }


			//-----HEAVY ATTACK CODE-----//


			if (Input.GetButtonDown(Player.Joystick + "Secondary") && Player.PrimaryAttack == false)// punch
			{
				Player.PlayerAnimation.SetLayerWeight(2, 1);

				if (Player.isGrounded)
				{
					Player.SecondaryAttack = true;

					CollidersOn();

					Player.PlayerAnimation.SetTrigger("SecondaryTrigger");

					CurrentState = Player.PlayerAnimation.GetCurrentAnimatorStateInfo(0);

					colliderTime = CurrentState.normalizedTime % 1;
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
				else
				{
					//Aerial smash
				}
			}
        }
	}
}
