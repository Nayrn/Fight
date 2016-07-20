using UnityEngine;
using System.Collections;

public class PlayerValues : MonoBehaviour {

	private const float MAX_HEALTH = 100;

	public float m_Health;
	public float m_Damage;

	public bool isGrounded;
	public float m_Speed;
	public float m_JumpSpeed;

	public float TurnSpeed;

	[HideInInspector]
	public float fallSpeed = -4;

	// Use this for initialization
	void Start () {
		m_Health = MAX_HEALTH;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "LeftHand" || col.gameObject.tag == "RightHand")
		{
			m_Health = m_Health - 20;
		}

		if (col.gameObject.tag == "LeftFoot" || col.gameObject.tag == "RightFoot")
		{
			m_Health = m_Health - 10;
		}

	}
}
