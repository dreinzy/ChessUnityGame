using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour
{
	public Camera camera1;
	public Camera camera2;
	public GameObject up;

	private Touch initialTouch = new Touch ();
	private bool beenSwiped = false;
	private float distance;

	void Start ()
	{
		Debug.Log ("Player awake.");
//		camera1.GetComponent<Camera> ().enabled = true;
//		camera1.GetComponent<AudioListener> ().enabled = true;
//		camera2.GetComponent<Camera> ().enabled = false;
//		camera2.GetComponent<AudioListener> ().enabled = false;
	}

	void Update ()
	{
		foreach (var t in Input.touches)
		{
			if (t.phase == TouchPhase.Began)
			{
				initialTouch = t;
				Debug.Log ("BEGAN");
			}
			else
			if (t.phase == TouchPhase.Moved && !beenSwiped)
				{
					float deltaX = initialTouch.position.x - t.position.x;
					float deltaY = initialTouch.position.y - t.position.y;
					distance = Mathf.Sqrt ((deltaX * deltaY) + (deltaX * deltaY));
					if (distance > 100.0f)
					{				
						//LEFT
						if (deltaX < 0 && Mathf.Abs (deltaX) <= Mathf.Abs (deltaY))
							Debug.Log ("LEFT");
					//RIGHT
					else
						if (deltaX > 0 && Mathf.Abs (deltaX) <= Mathf.Abs (deltaY))
								Debug.Log ("RIGHT");
					//UP
					else
							if (deltaY > 0 && Mathf.Abs (deltaX) > Mathf.Abs (deltaY))
									up.SetActive (true);
					//DOWN
					else
								if (deltaY < 0 && Mathf.Abs (deltaX) > Mathf.Abs (deltaY))
										Debug.Log ("DOWN");
					}
					beenSwiped = true;
				}
				else
				if (t.phase == TouchPhase.Ended)
					{
						//TAPPED
						Debug.Log ("ENDED");
						initialTouch = new Touch ();
						beenSwiped = false;
					}
		}
	}
}