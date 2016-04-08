using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PawnBehaviourScript : MonoBehaviour, IPointerClickHandler {
	public char currentCol = 'A';
	public int currentRow = 1;
	public bool firstMove = true;

	public string colour = "White";
	//public string Colour { get; set; }

	public void SetCurrentPosition(char col, int row)
	{
		currentCol = col;
		currentRow = row;
	}

	public void ChangePieceToBlack()
	{
		colour = "Black";
	}

	public bool CheckMove(ZoneScript target)
	{
		switch (this.gameObject.tag) {
		case "Pawn":
			/*** Pawn taking another piece ***/
			//Either side of the pawn and occupied
			if ((target.row == currentRow + 1 || target.row == currentRow - 1) && target.occupado)
			{
				// Pawn is white and target is diagonal in correct direction
				if (colour == "White" && target.column == currentCol + (char)1) 
				{
					// Target is black
					if (target.containedPiece.colour == "Black")
						return true;
					else
						return false;					
				}
				// Pawn is black and target is diagonal in correct direction
				else if (colour == "Black" && target.column == currentCol - (char)1) 
				{	
					// Target is white
					if (target.containedPiece.colour == "White")
						return true;
					else
						return false;					
				}
				else
					return false;
			}
			/*** Pawn moving straight***/
			else if (target.row == currentRow) 
			{
				// Pawn is white and moving one place
				if (colour == "White" && target.column == currentCol + (char)1) 
				{
					if (!target.occupado)
						return true;
					else
						return false;					
				}
				// Pawn is white, on its first turn and moving two places
				else if (colour == "White" && target.column == currentCol + (char)2 && firstMove)
				{
					// Check two places are clear
					if (IsOccupied((char)(currentCol + (char)1), currentRow) || IsOccupied(target.column, target.row))
							return false;
						else
							return true;
				}
				// Pawn is black and moving one place
				else if (colour == "Black" && target.column == currentCol - (char)1) 
				{
					return false;
				}
				// Pawn is black, on its first turn and moving two places
				else if (colour == "Black" && target.column == currentCol - (char)2 && firstMove) 
				{
					// Check two places are clear
					if (IsOccupied((char)(currentCol - (char)2), currentRow) || IsOccupied(target.column, target.row))
						return false;
					else
						return true;
				}
				return false;
			}
			break;
		case "Rook":
			/*** Rook movement ***/
			// Rook isn't trying to take a piece of the same colour
			if (target.occupado) 
				if (target.containedPiece.colour == colour)
					return false;
			// Target is in same row
			if (target.row.Equals(currentRow)) 
			{
				for (int i = 0; i < Mathf.Abs(target.row - currentRow); i++) {
					if(IsOccupied(currentCol, currentRow + i))
						return false;
					return true;
				}
			}
			// Target is in same column
			else if (target.column.Equals(currentCol)) 
			{
				for (int i = 0; i < Mathf.Abs((int)target.column - (int)currentCol); i++) {
					if(IsOccupied((char)((char)currentCol + (char)i), currentRow))
						return false;
					return true;
				}
			}
			break;
		case "Bishop":
			/*** Bishop movement ***/
			// Bishop isn't trying to take a piece of the same colour
			if (target.occupado) 
				if (target.containedPiece.colour == colour)
					return false;
			break;
		case "Knight":
			/*** Knight movement ***/
			// Knight isn't trying to take a piece of the same colour
			if (target.occupado) 
				if (target.containedPiece.colour == colour)
					return false;
			break;
		case "Queen":
			/*** Queen movement ***/
			// Queen isn't trying to take a piece of the same colour
			if (target.occupado) 
				if (target.containedPiece.colour == colour)
					return false;
			break;
		case "King":
			/*** King movement ***/
			// King isn't trying to take a piece of the same colour
			if (target.occupado) 
				if (target.containedPiece.colour == colour)
					return false;
			break;
		default:
			break;
		}
		return false;
	}

	public void OnPointerDown()
	{

	}

	private bool IsOccupied(char col, int row)
	{
		ZoneScript[] zones = FindObjectsOfType<ZoneScript> ();
		for (int i = 0; i < zones.Length; i++) {
			if ((zones[i].column == col && zones[i].row == row))
				return zones[i].occupado;
		}
		return false;
	}

	public void Move (char col, int row)
	{
		GameController.FindContainingZone (this.gameObject);
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log ("You clicked me: " + this.name);
	}

	#endregion
}