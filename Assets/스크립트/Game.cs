using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public static Vector2 start, end;
	public float difference	= 200.0f;
    public GameObject controlledCharacter;

	PlayerManager playerManager;    

	void Start () 										// Use this for initialization
	{
		playerManager = GetComponent<PlayerManager>();
        playerManager.SetCharacter(controlledCharacter);
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.GetMouseButtonDown (0)) 
		{
			start = Input.mousePosition;				// Debug.Log ("시작 " + start + "\n");
		} 

		if (Input.GetMouseButtonUp (0)) 
		{
			end = Input.mousePosition;					// Debug.Log ("끝 " + end + "\n");
			float distance = Vector2.Distance(start, end);
			if(distance > difference)
				playerManager.ChangeCharacter();		// Debug.Log ("거리 " + distance + "\n");
		}
	}													//	[System.Diagnostics.Conditional("UNITY_EIDTOR")]
}
