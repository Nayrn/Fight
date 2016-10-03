using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
	public PlayerValues Player;
   // public CollisionSoul soulAttack;
	public GameObject[] colliders;
    public TrailRenderer trail;
	public string[] PrimaryCombos;
	public string[] SecondaryCombos;
	public int PrimaryCount = 0;
	public int SecondaryCount = 0;

	private AnimatorStateInfo CurrentState;

	public float leewayTime = 4.5f;
    public float blockTime;
    // Use this for initialization
    void Start()
    {
        Player.SecondaryAttack = false;
        Player.PrimaryAttack = false;
        Player.isAttacking = false;
        for (int i = 0; i < colliders.Length; i++ )
			colliders[i].GetComponent<BoxCollider>().enabled = false;
   //     soulAttack.GetComponent<CollisionSoul>();
        trail.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
		ActionUpdate();
        Debug.Log(Player.isAttacking);
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
                CollidersOn();
                //Activating the animation trigger
                Player.PlayerAnimation.SetTrigger("PrimaryTrigger");
            }
            



            //-----HEAVY ATTACK CODE-----//


            if (Input.GetButtonDown(Player.Joystick + "Secondary") && Player.PrimaryAttack == false)// punch
            {
                //Setting Attack bool to true
                Player.isAttacking = true;
                Player.SecondaryAttack = true;
                CollidersOn();
                //Activating the animation trigger
                Player.PlayerAnimation.SetTrigger("SecondaryTrigger");
            }
   


        }
        if (!Player.isAttacking)
            CollidersOff();
        
	}

    public void CollidersOn()
    {
		Debug.Log("Colliders On");
		if (Player.PrimaryAttack)
		{
			colliders[0].GetComponent<BoxCollider>().enabled = true;
			colliders[0].tag = "PrimaryAttack";
            trail.gameObject.SetActive(true);
        }
		else if (Player.SecondaryAttack)
		{
			colliders[0].GetComponent<BoxCollider>().enabled = true;
			colliders[0].tag = "SecondaryAttack";
            trail.gameObject.SetActive(true);
        }
    }

    public void CollidersOff()
    {
		Player.SecondaryAttack = false;
		Player.PrimaryAttack = false;
		Player.isAttacking = false;

		Debug.Log("Colliders Off");

        colliders[0].GetComponent<BoxCollider>().enabled = false;
        trail.gameObject.SetActive(false);
    }
}
