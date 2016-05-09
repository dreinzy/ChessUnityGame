using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Promotion : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Pawn" )
        {
            GameObject.FindObjectOfType<GameController>().PromotionMenu(true);
        }
    }
}
