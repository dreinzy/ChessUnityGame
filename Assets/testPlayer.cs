using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class testPlayer : MonoBehaviour
{

	public Camera camera1;
	public Camera camera2;
	public GameObject up;
	public Text upText;

	private Touch initialTouch = new Touch();
	private bool beenSwiped = false;
	private float distance;

	// Use this for initialization
	void Start()
	{
	
	}

	void Update()
	{
//		foreach (var t in Input.touches)
//		{
//			if (t.phase == TouchPhase.Began)
//			{
//				initialTouch = t;
//				upText.text += "BEGAN";
//				Debug.Log("BEGAN");
//			}
//			if (t.phase == TouchPhase.Moved && !beenSwiped)
//			{
//				float deltaX = initialTouch.position.x - t.position.x;
//				float deltaY = initialTouch.position.y - t.position.y;
//				distance = Mathf.Sqrt((deltaX * deltaY) + (deltaX * deltaY));
//				if (distance > 100.0f)
//				{				
//					//LEFT
//					if (deltaX < 0 && Mathf.Abs(deltaX) <= Mathf.Abs(deltaY))
//						upText.text += "LEFT";
//					//RIGHT
//					else if (deltaX > 0 && Mathf.Abs(deltaX) <= Mathf.Abs(deltaY))
//						upText.text += "RIGHT";
//					//UP
//						else if (deltaY > 0 && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
//						upText.text += "UP";
//					//DOWN
//							else if (deltaY < 0 && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
//						upText.text += "DOWN";
//				}
//				beenSwiped = true;
//			}
//			else if (t.phase == TouchPhase.Ended)
//			{
//				//TAPPED
//				Debug.Log("ENDED");
//				initialTouch = new Touch();
//				beenSwiped = false;
//			}
//		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			switch (Input.GetTouch(0).phase)
			{
				case TouchPhase.Began:
					upText.text += "BEGAN";
					break;
				case TouchPhase.Moved:
					if (!beenSwiped)
					{
						upText.text += "SWIPED" + touch.deltaPosition;
						beenSwiped = true;
					}
					break;
				case TouchPhase.Ended:
					beenSwiped = false;
					upText.text += "ENDED";
					break;
				default:
					break;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			upText.text += "SPACE";
		}
	}
}
