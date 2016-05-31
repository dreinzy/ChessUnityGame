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



	public bool CheckMove(ZoneScript target)
	{
		int distanceCol = Mathf.Abs((int)currentCol - (int)target.column);
		int distanceRow = Mathf.Abs(currentRow - target.row);
		switch (this.tag) {
		case "Pawn":
			/*** Pawn taking another piece ***/
			//Either side of the pawn and occupied
			if ((target.column == currentCol + (char)1 || target.column == currentCol - (char)1) && target.occupado)
			{
				// Pawn is white and target is diagonal in correct direction
				if (colour == "White" && target.row == currentRow + 1) 
				{
					// Target is black
					if (target.containedPiece.colour == "Black")
					{
						firstMove = false;
						return true;
					}
					else
						return false;					
				}
				// Pawn is black and target is diagonal in correct direction
				else if (colour == "Black" && target.row == currentRow - 1) 
				{	
					// Target is white
					if (target.containedPiece.colour == "White")
					{
						firstMove = false;
						return true;
					}
					else
						return false;					
				}
				else
					return false;
			}
			/*** Pawn moving straight***/
			else if (target.column == currentCol) 
			{
				// Pawn is white and moving one place
				if (colour == "White" && target.row == currentRow + 1) 
				{
					if (!target.occupado)
					{
						firstMove = false;
						return true;
					}
					else
						return false;					
				}
				// Pawn is white, on its first turn and moving two places
				else if (colour == "White" && target.row == currentRow + 2 && firstMove)
				{
					// Check two places are clear
						if (IsOccupied(target.column, target.row - 1) || IsOccupied(target.column, target.row))
							return false;
						else
						{
							firstMove = false;
							return true;
						}
				}
				// Pawn is black and moving one place
				else if (colour == "Black" && target.row == currentRow - 1) 
				{
					if (!target.occupado)
					{
						firstMove = false;
						return true;
					}
					else
						return false;	
				}
				// Pawn is black, on its first turn and moving two places
				else if (colour == "Black" && target.row == currentRow - 2 && firstMove) 
				{
					// Check two places are clear
					if (IsOccupied(target.column, target.row + 1) || IsOccupied(target.column, target.row))
						return false;
					else
					{
						 firstMove = false;
							return true;
					}
				}
				return false;
			}
			break;
		case "Rook":
			/*** Rook movement ***/
			// Rook isn't trying to take a piece of the same colour
			if (target.occupado && target.containedPiece.colour == colour) 
				return false;
			// Target is in same row
			if (target.row == currentRow) 
			{
                    // Check places between piece and target are vacant
                    int distance = ( (int) target.column - (int) currentCol );
                    for ( int i = 1; i < Mathf.Abs(distance); i++ )
                    {
                        if ( distance > 0 )
                            if ( IsOccupied((char) ( currentCol + (char) i ), currentRow) )
                                return false;
                            else
                            if ( IsOccupied((char) ( currentCol - (char) i ), currentRow) )
                                return false;
                    }
                    return true;
                }
			// Target is in same column
			else if (target.column.Equals(currentCol)) 
			{
					// Check places between piece and target are vacant
					int distance = target.row - currentRow;
					for (int i = 1; i < Mathf.Abs(distance); i++) {
                        if ( distance > 0 )
                            if ( IsOccupied(currentCol, currentRow + i) )
                                return false;
                        else
                            if ( IsOccupied(currentCol, currentRow - i) )
                                return false;
                    }
					return true;
			}
			break;
		case "Bishop":
			/*** Bishop movement ***/
			// Bishop isn't trying to take a piece of the same colour
			if (target.occupado && target.containedPiece.colour == colour) 
				return false;
			// Target is diagonal to bishop
			if (distanceCol == distanceRow)
			{
				// Determine direction of target
				if ((int)currentCol < target.column && currentRow < target.row)
				{ // up-left
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol + (char)i), currentRow + i))
						return false;
					}
					return true;
				}
				else if((int)currentCol > target.column && currentRow < target.row)
				{ // up-right
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol - (char)i), currentRow + i))
						return false;
					}
					return true;
				}
				else if((int)currentCol < target.column && currentRow > target.row)
				{ // down-left
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol + (char)i), currentRow - i))
						return false;
					}
					return true;
				}
				else
				{ // down-right
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol - (char)i), currentRow - i))
						return false;
					}
					return true;
				}
			}			
			break;
		case "Knight":
			/*** Knight movement ***/
			// Knight isn't trying to take a piece of the same colour
			if (target.occupado && target.containedPiece.colour == colour) 
				return false;
				int vertDistance = Mathf.Abs((int)currentCol - (int)target.column);
				int horDistance = Mathf.Abs(currentRow - target.row);
				if ((vertDistance == 1 && horDistance == 2) || (vertDistance == 2 && horDistance == 1))
					return true;
			break;
		case "Queen":
			/*** Queen movement ***/
			// Queen isn't trying to take a piece of the same colour
			if (target.occupado && target.containedPiece.colour == colour) 
				return false;
			// Target is in same row
			if (target.row == currentRow) 
			{
				// Check places between piece and target are vacant
				int distance = ((int)target.column - (int)currentCol);
				for (int i = 1; i < Mathf.Abs(distance); i++)
                {
                        if ( distance > 0 )                        
                            if ( IsOccupied((char) ( currentCol + (char) i ), currentRow) )
                                return false;                        
                        else                        
                            if ( IsOccupied((char) ( currentCol - (char) i ), currentRow) )
                                return false;                        
				}
				return true;
			}
			// Target is in same column
			else if (target.column.Equals(currentCol)) 
			{
                    // Check places between piece and target are vacant
                    int distance = ( (int) target.column - (int) currentCol );
                    for ( int i = 1; i < Mathf.Abs(distance); i++ )
                    {
                        if ( distance > 0 )
                            if ( IsOccupied((char) ( currentCol + (char) i ), currentRow) )
                                return false;
                            else
                            if ( IsOccupied((char) ( currentCol - (char) i ), currentRow) )
                                return false;
                    }
                    return true;
                }
			 // Target is diagonal to queen
			else if (distanceCol == distanceRow)
			{
				// Determine direction of target
				if ((int)currentCol < target.column && currentRow < target.row)
				{ // up-left
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol + (char)i), currentRow + i))
							return false;
					}
					return true;
				}
				else if((int)currentCol > target.column && currentRow < target.row)
				{ // up-right
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol - (char)i), currentRow + i))
							return false;
					}
					return true;
				}
				else if((int)currentCol < target.column && currentRow > target.row)
				{ // down-left
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol + (char)i), currentRow - i))
							return false;
					}
					return true;
				}
				else
				{ // down-right
					for (int i = 1; i < distanceCol; i++)
					{
						if(IsOccupied((char)(currentCol - (char)i), currentRow - i))
							return false;
					}
					return true;
				}
			}			
			break;
		case "King":
			/*** King movement ***/
			// King isn't trying to take a piece of the same colour
			if (target.occupado && target.containedPiece.colour == colour) 
				return false;
				if ((distanceCol == 0 && distanceRow == 1) || (distanceCol == 1 && distanceRow == 0) || (distanceCol == 1 && distanceRow == 1))
				return true;
			break;
		default:
			break;
		}
		return false;
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
		GameController.FindContainingZone (this.gameObject).occupado = false;
		GameController.FindContainingZone (this.gameObject).containedPiece = null;
		ZoneScript TargetZone = GameController.SelectZone(col, row);
		if (TargetZone.occupado)
		{
			PawnBehaviourScript targetPiece = GameController.SelectPiece(col, row);
			Destroy(targetPiece.gameObject);
		}
		this.transform.position = TargetZone.gameObject.transform.position;
		this.currentCol = col;
		this.currentRow = row;
		//Destroy (this.gameObject);
	}


	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log ("You clicked me: " + this.name);
	}

	#endregion
}