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

				//Get the current state and set the time
				CurrentState = Player.PlayerAnimation.GetCurrentAnimatorStateInfo(0);
				colliderTime = CurrentState.normalizedTime % 1;

                if(!Player.isGrounded)
				{

				}
			}


			//-----HEAVY ATTACK CODE-----//


			if (Input.GetButtonDown(Player.Joystick + "Secondary") && Player.PrimaryAttack == false)// punch
			{
                Player.isAttacking = true;
                Player.SecondaryAttack = true;

				Player.PlayerAnimation.SetTrigger("SecondaryTrigger");

				CurrentState = Player.PlayerAnimation.GetCurrentAnimatorStateInfo(0);

				colliderTime = CurrentState.normalizedTime % 1;
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

            if(this.Player.gameObject.tag == "Player2")
            {
                colliders[0].tag = "PrimaryP2";
            }
        }
        else if (Player.SecondaryAttack)
        {
            colliders[0].GetComponent<BoxCollider>().enabled = true;
            colliders[0].tag = "SecondaryAttack";


            if (this.Player.gameObject.tag == "Player2")
            {
                colliders[0].tag = "SecondaryP2";
            }
        }
        // switch colliders on for this amount of time
    }

    public void CollidersOff()
    {
		Debug.Log("Colliders Off");
		if (Player.PrimaryAttack)
            colliders[0].GetComponent<BoxCollider>().enabled = false;
        else if (Player.SecondaryAttack)
            colliders[0].GetComponent<BoxCollider>().enabled = false;
        // switch colliders on for this amount of time
    }
}
