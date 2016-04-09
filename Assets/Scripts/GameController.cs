using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	private double _boardWidth;
	private double _boardHeight;
	private bool player1Turn = true;
	private bool pieceSelect = true;
	private bool zoneSelect = false;

	public GameObject camera1;
	public GameObject camera2;
	public GameObject SelectedPiece;
	public ZoneScript TargetZone;
	public Light greenHalo;
	public Light redHalo;
	public AudioSource nope;

	void Start ()
	{
		Player (1);
	}

	private void Player(int playerNo)
	{
		if (playerNo == 1)
		{
			camera1.GetComponent<Camera> ().enabled = true;
			camera1.GetComponent<AudioListener> ().enabled = true;
			camera2.GetComponent<Camera> ().enabled = false;
			camera2.GetComponent<AudioListener> ().enabled = false;
			player1Turn = true;
		}
		else
		{			
			camera1.GetComponent<Camera> ().enabled = false;
			camera1.GetComponent<AudioListener> ().enabled = false;
			camera2.GetComponent<Camera> ().enabled = true;
			camera2.GetComponent<AudioListener> ().enabled = true;
			player1Turn = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.anyKeyDown)
		{
			// Select default piece (king), if none selected
			if (SelectedPiece == null)
			{
				GameObject[] DefaultPiece = GameObject.FindGameObjectsWithTag("King");
				foreach (var king in DefaultPiece) {
					if (player1Turn && king.GetComponent<PawnBehaviourScript> ().colour == "White")
						SelectedPiece = king;
					else if (!player1Turn && king.GetComponent<PawnBehaviourScript> ().colour == "Black") {
						SelectedPiece = king;
					}
				}
			}
			PawnBehaviourScript piece = SelectedPiece.GetComponent<PawnBehaviourScript>();

			try
			{
				if (pieceSelect)
				{
					FindNextPiece(ref piece);
				}
				else if (zoneSelect)
				{
					if (TargetZone == null)
						TargetZone = SelectZone(piece.currentCol, piece.currentRow);
					FindNextZone(ref TargetZone);
					if(!pieceSelect && !zoneSelect)
					{	
						if(piece.CheckMove(TargetZone))
						{
							piece.Move(TargetZone.column, TargetZone.row);
							piece= null;
							TargetZone = null;
							SelectedPiece = null;
							pieceSelect = true;
							if (player1Turn)
								Player(2);
							else
								Player(1);
						}
						else 
						{
							Debug.Log("Not a valid move");
							TargetZone = SelectZone(piece.currentCol, piece.currentRow);
							zoneSelect = true;
						}
					}
				}
			}
			catch(System.NullReferenceException ex)
			{
				Debug.Log("Nope, nothing there: " + ex.Message);
				nope.Play();
			}

			if(SelectedPiece != null)
				redHalo.transform.position = (SelectedPiece.transform.position + new Vector3(0, 3.5f, 0));
			else
				redHalo.transform.position = new Vector3(250, 250, 250);
			if(TargetZone != null)
				greenHalo.transform.position = (TargetZone.transform.position + new Vector3(0, 1.5f, 0));
			else
				greenHalo.transform.position = new Vector3 (250, 250, 250);
		}

		// A piece has been selected
//		if (SelectedPiece != null && TargetZone == null && (!particlesAnimating || CheckIfSelectedPieceHasChanged (SelectedPiece.GetComponent<PawnBehaviourScript> ()))) 
//		{
////			ParticleSystem pS = FindContainingZone(SelectedPiece).gameObject.GetComponent<ParticleSystem> ();
////			pS.Play ();
//			particlesAnimating = true;
//		}
	}

	public PawnBehaviourScript FindNextPiece(ref PawnBehaviourScript piece)
	{
		if (player1Turn) 
		{				
			if (Input.GetButtonDown("Up")) {
				SelectedPiece = SelectPiece(piece.currentCol, piece.currentRow + 1).gameObject;
			}
			else if (Input.GetButtonDown("Down")) {
				SelectedPiece = SelectPiece(piece.currentCol, piece.currentRow - 1).gameObject;
			}
			else if (Input.GetButtonDown("Left")) {

				SelectedPiece = SelectPiece((char)(piece.currentCol + (char)1), piece.currentRow).gameObject;
				}
			else if (Input.GetButtonDown("Right")) {
				SelectedPiece = SelectPiece((char)(piece.currentCol - (char)1), piece.currentRow).gameObject;			
			}
		}
		else 
		{
			if (Input.GetButtonDown("Up")) {
				SelectedPiece = SelectPiece(piece.currentCol, piece.currentRow - 1).gameObject;
			}
			else if (Input.GetButtonDown("Down")) {
				SelectedPiece = SelectPiece(piece.currentCol, piece.currentRow + 1).gameObject;
			}
			else if (Input.GetButtonDown("Left")) {
				SelectedPiece = SelectPiece((char)(piece.currentCol - (char)1), piece.currentRow).gameObject;
			}
			else if (Input.GetButtonDown("Right")) {
				SelectedPiece = SelectPiece((char)(piece.currentCol + (char)1), piece.currentRow).gameObject;
			}
			else if (Input.GetButtonDown("Select")) {
				Debug.Log("Select");
			}
		}
		if (Input.GetButtonDown("Select")) {
			pieceSelect = false;
			zoneSelect = true;
			TargetZone = SelectZone(piece.currentCol, piece.currentRow);
		}

		return SelectedPiece.GetComponent<PawnBehaviourScript>();
	}

	public ZoneScript FindNextZone(ref ZoneScript TargetZone)
	{
		if (player1Turn) 
		{				
			if (Input.GetButtonDown("Up")) {
				TargetZone = SelectZone(TargetZone.column, TargetZone.row + 1);
			}
			else if (Input.GetButtonDown("Down")) {
			TargetZone = SelectZone(TargetZone.column, TargetZone.row - 1);
			}
			else if (Input.GetButtonDown("Left")) {
			TargetZone = SelectZone((char)(TargetZone.column + (char)1), TargetZone.row);
			}
			else if (Input.GetButtonDown("Right")) {
			TargetZone = SelectZone((char)(TargetZone.column - (char)1), TargetZone.row);
			}
//			else if (Input.GetButtonDown("Down")) {
//				SelectedPiece = SelectPiece(piece.currentCol, piece.currentRow - 1).gameObject;
//			}
//			else if (Input.GetButtonDown("Left")) {
//
//				SelectedPiece = SelectPiece((char)(piece.currentCol + (char)1), piece.currentRow).gameObject;
//			}
//			else if (Input.GetButtonDown("Right")) {
//				SelectedPiece = SelectPiece((char)(piece.currentCol - (char)1), piece.currentRow).gameObject;			
//			}
		}
		else
		{
			if (Input.GetButtonDown("Up")) {
			TargetZone = SelectZone(TargetZone.column, TargetZone.row - 1);
			}
			else if (Input.GetButtonDown("Down")) {
			TargetZone = SelectZone(TargetZone.column, TargetZone.row + 1);
			}
			else if (Input.GetButtonDown("Left")) {
			TargetZone = SelectZone((char)(TargetZone.column - (char)1), TargetZone.row);
			}
			else if (Input.GetButtonDown("Right")) {
			TargetZone = SelectZone((char)(TargetZone.column + (char)1), TargetZone.row);
			}
//			if (Input.GetButtonDown("Up")) {
//				SelectedPiece = SelectPiece(piece.currentCol, piece.currentRow - 1).gameObject;
//			}
//			else if (Input.GetButtonDown("Down")) {
//				SelectedPiece = SelectPiece(piece.currentCol, piece.currentRow + 1).gameObject;
//			}
//			else if (Input.GetButtonDown("Left")) {
//				SelectedPiece = SelectPiece((char)(piece.currentCol - (char)1), piece.currentRow).gameObject;
//			}
//			else if (Input.GetButtonDown("Right")) {
//				SelectedPiece = SelectPiece((char)(piece.currentCol + (char)1), piece.currentRow).gameObject;
//			}
//			else if (Input.GetButtonDown("Select")) {
//				Debug.Log("Select");
//			}
		}
		if (Input.GetButtonDown("Select")) {
			zoneSelect = false;
		}
		else if (Input.GetButtonDown("Cancel"))
		{
			pieceSelect = true;
			TargetZone = null;
			zoneSelect = false;
		}

		return TargetZone.GetComponent<ZoneScript>();
	}
		
	public static ZoneScript FindContainingZone(GameObject piece)
	{
		PawnBehaviourScript p = piece.GetComponent<PawnBehaviourScript> ();
		ZoneScript[] z = FindObjectsOfType<ZoneScript> ();
		foreach (ZoneScript zone in z) 
		{
			if (zone.column == p.currentCol && zone.row == p.currentRow)
				return zone;
		}
		return null;
	}

	public static PawnBehaviourScript FindContainedPiece(GameObject zone)
	{
		ZoneScript z = zone.GetComponent<ZoneScript> ();
		PawnBehaviourScript[] p = FindObjectsOfType<PawnBehaviourScript> ();
		foreach (PawnBehaviourScript piece in p) 
		{
			if (piece.currentCol == z.column && piece.currentRow == z.row)
				return piece;
		}
		return null;
	}

	public static PawnBehaviourScript SelectPiece(char col, int row)
	{
		PawnBehaviourScript[] p = FindObjectsOfType<PawnBehaviourScript> ();
		foreach (PawnBehaviourScript piece in p) 
		{
			if (piece.currentCol == col && piece.currentRow == row)
				return piece;
		}
		return null;
	}

	public static ZoneScript SelectZone(char col, int row)
	{
		ZoneScript[] z = FindObjectsOfType<ZoneScript> ();
		foreach (ZoneScript zone in z) 
		{
			if (zone.column == col && zone.row == row)
				return zone;
		}
		return null;
	}
}