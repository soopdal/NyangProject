using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	public 	GameObject [] 	characters;
	public 	float			speed 			= 1.0f;

			GameObject 		character, target;
			int 			current			= 0;
			float           sky             = 0.0f, 
                            ground          = 0.0f;                     // , 
            bool			change			= false;

	void Start () 								// Use this for initialization
	{
		//Vector2 position = Camera.main.WorldToScreenPoint(transform.position);
		//Game.start.x = position.x;

		//character 	= characters[current];
		//target 		= characters[current + 1];

		ground 		= character.transform.position.y;
//		sky 		= target.transform.position.y;
	}	

	void Update () 								// Update is called once per frame
	{		
		Positioning();
		Changing();
	}

	void Positioning()
	{
		float target = Game.start.x;
		Vector2 position = Camera.main.WorldToScreenPoint(character.transform.position);
		float current = position.x;
		
		float difference = target - current;
		float distance = Mathf.Abs(difference);
		if(distance > speed)
		{											//			Debug.Log ("current " + position.x + "\ndifference " + difference + "\n");
			Vector3 direction;
			if(difference > 0.0f)
				direction = Vector3.right;
			else
				direction = Vector3.left;

			Vector3 move = direction * speed * Time.deltaTime;
			character.transform.Translate(move.x, 0, 0);
        }
	}

	void Changing()
	{
		if (!change)
			return;
																	//	Debug.Log ("캐릭터 " + character.transform.position.y + " " + target.transform.position.y + "\n");
		bool isCharacter = false, isTarget = false;

		character.transform.Translate(0, speed * Time.deltaTime, 0);
		if(character.transform.position.y > sky)
		{
			character.transform.position = new Vector3(character.transform.position.x, sky, 
			                                           character.transform.position.z);
			isCharacter = true;
		}
		
		target.transform.Translate(0, -speed * Time.deltaTime, 0);
		if(target.transform.position.y < ground)
		{
			target.transform.position = new Vector3(target.transform.position.x, ground, 
			                                        target.transform.position.z);
			isTarget = true;
		}
		
		if (isCharacter && isTarget) {
			change = false;
			++current;
			if (current >= characters.Length-1)
			{
				current = -1;
				character = characters[characters.Length-1];
				target = characters[0];

				int characterNo = characters.Length-1;
				Debug.Log ("character " + characterNo + " " + 0 + "\n");
			}
			else
			{
				character = characters[current];			
				target = characters[current + 1];
				int next = current + 1;
				Debug.Log ("character " + current + " " + next +"\n");
			}
		} 
		else
			target.transform.position = new Vector3(character.transform.position.x, target.transform.position.y, 
			                                          target.transform.position.z);
	}

	public void ChangeCharacter()
	{
		change = true;								// Debug.Log ("바꾸기 " + previous + " " + change + "\n");
	}

    public void SetCharacter(GameObject character)
    {
        foreach(var charac in characters)
        {
            if (charac.Equals(character))
            {
                this.character = character;
                Game.start.x = this.character.transform.position.x;
                break;
            }
        }
    }
}
