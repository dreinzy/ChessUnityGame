using UnityEngine;
using System.Collections;

public class ChessBoard : MonoBehaviour{
	
	public bool IsZoneOccupied(char c, int r)
	{
		ZoneScript[] zones = MonoBehaviour.FindObjectsOfType<ZoneScript> ();
		for (int i = 0; i < zones.Length; i++) {
			if (zones[i].column == c && zones[i].row == r) {
				return zones [i].occupado;
			}
		}
		return false;
	}
	public void OnLevelWasLoaded()
	{
		ColourHalfThePiecesBlack();
	}

	public void ColourHalfThePiecesBlack()
	{
		PawnBehaviourScript[] pieces = MonoBehaviour.FindObjectsOfType<PawnBehaviourScript>();
		for (int i = 0; i < pieces.Length; i++) {
			if (pieces[i].currentRow == 7 || pieces[i].currentRow == 8)
				pieces[i].colour = "Black";
		}
	}
}
