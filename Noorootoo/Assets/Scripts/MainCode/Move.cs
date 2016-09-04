using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public string m_Name;

	public int m_Damage;
	private ElemTrait m_Element;

	public Collider strikeBox;

	public int attackNumber = 1;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter()
	{

	}
}
