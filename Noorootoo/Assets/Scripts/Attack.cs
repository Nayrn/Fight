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
    public JoystickNum Joystick = JoystickNum.Keyboard;
    public GameObject[] colliders;

	public string[] PrimaryCombos;
	public string[] SecondaryCombos;
	public int PrimaryCount = 0;
	public int SecondaryCount = 0;
	
	private float leewayTime = 4.5f;

	private AnimatorStateInfo CurrentState;

    public PlayerValues Player;

    public float colliderTime;

    public Animator anim;
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

				Player.SecondaryAttack = false;
				Player.PrimaryAttack = false;
				PrimaryCount = 0;
				SecondaryCount = 0;

				//turn animation off
				anim.SetBool("SecondaryAttack", Player.SecondaryAttack);
				anim.SetBool("PrimaryAttack", Player.PrimaryAttack);
			}
		}
    }

	void ActionUpdate()
	{
        //-----BLOCK CODE-----//
        if(Player.isGrounded && Input.GetButtonDown(Joystick + "Block"))
        {
            Player.isStasis = true;
            Player.isBlocking = true;

			anim.SetBool("isBlocking", Player.isBlocking);
			anim.SetTrigger("Block");
		}
		else if (Input.GetButtonUp(Joystick + "Block"))
		{
			Player.isStasis = false;
			Player.isBlocking = false;

			anim.SetBool("isBlocking", Player.isBlocking);
		}
		else
		{
			//Airdodge
		}

		if (!Player.isBlocking)
        {
			//-----LIGHT ATTACK CODE-----//
			if (Input.GetButtonDown(Joystick + "Primary") && Player.SecondaryAttack == false)// punch
            {
				if (Player.isGrounded)
				{
					//anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 0.5f);
					CurrentState = anim.GetCurrentAnimatorStateInfo(0);
					//-----Setting attack to true OR increasing Attackcount-----//
					if (!Player.PrimaryAttack)
					{
						Player.PrimaryAttack = true;
						colliderTime = 3.0f;

						++PrimaryCount;
						// set colliders to active
						CollidersOn();
						// punch animation  
						anim.SetBool("PrimaryAttack", Player.PrimaryAttack);
						anim.SetTrigger("AttackTrigger");
					}
					else if (Player.PrimaryAttack && CurrentState.IsName(PrimaryCombos[PrimaryCount]))
					{
						PrimaryCount++;
						CollidersOn();
					}
					//-----------------------------------------------------------//

					//-----Setting the animation time and triggers if not at max combo-----//
					if (PrimaryCount < PrimaryCombos.Length)
					{
						anim.SetInteger("PrimaryCombo", PrimaryCount);

						if (CurrentState.IsName(PrimaryCombos[PrimaryCount]))
							colliderTime = CurrentState.length + leewayTime;
					}
					else if (CurrentState.IsName(PrimaryCombos[PrimaryCombos.Length - 1]))
						colliderTime = 0;

					//-----Resetting the count to 0-----//
					if (PrimaryCount == PrimaryCombos.Length && CurrentState.IsName(PrimaryCombos[PrimaryCombos.Length - 1]))// && CurrentState.IsName(SecondaryCombos[SecondaryCount]))
						PrimaryCount = 0;
				}
				else
				{
					//Aerial attacks
				}
            }


			//-----HEAVY ATTACK CODE-----//


			if (Input.GetButtonDown(Joystick + "Secondary") && Player.PrimaryAttack == false)// punch
			{
				if (Player.isGrounded)
				{
					//-----Getting current Animation state-----//
					CurrentState = anim.GetCurrentAnimatorStateInfo(0);

					//-----Setting attack to true OR increasing Attackcount-----//
					if (!Player.SecondaryAttack)
					{
						Player.SecondaryAttack = true;
						colliderTime = 3.0f;

						++SecondaryCount;
						// set colliders to active
						CollidersOn();
						// punch animation  
						anim.SetBool("SecondaryAttack", Player.SecondaryAttack);
						anim.SetTrigger("AttackTrigger");
					}
					else if (Player.SecondaryAttack && CurrentState.IsName(SecondaryCombos[SecondaryCount]))
					{
						++SecondaryCount;
						CollidersOn();
					}
						//-----------------------------------------------------------//

					//-----Setting the animation time and triggers if not at max combo-----//
					if (SecondaryCount < SecondaryCombos.Length)
					{
						anim.SetInteger("SecondaryCombo", SecondaryCount);

						if (CurrentState.IsName(SecondaryCombos[SecondaryCount]))
							colliderTime = CurrentState.length + leewayTime;
					}
					else if (CurrentState.IsName(SecondaryCombos[SecondaryCombos.Length - 1]))
						colliderTime = 0;

					//-----Resetting the count to 0-----//
					if (SecondaryCount == SecondaryCombos.Length && CurrentState.IsName(SecondaryCombos[SecondaryCombos.Length - 1]))// && CurrentState.IsName(SecondaryCombos[SecondaryCount]))
						SecondaryCount = 0;
				}
				else
				{
					//Aerial smash
				}
			}
        }
	}
}
