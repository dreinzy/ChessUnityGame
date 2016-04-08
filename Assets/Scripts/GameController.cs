using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	private double _boardWidth;
	private double _boardHeight;

	public GameObject camera1;
	public GameObject camera2;
	private bool validMove = false;
	private bool particlesAnimating = false;
	private bool firstTime = true;
	private bool player1Turn = true;
	private bool pieceSelect = true;
	private bool zoneSelect = false;
//	private string playerColor;
	PawnBehaviourScript initialPiece = null;

	public GameObject SelectedPiece;
	public GameObject[] TargetZones;
	public GameObject TargetZone;

	void Start ()
	{
		Player (1);
	}

//	private void AddLights()
//	{
//		BoxCollider[] bc = FindObjectsOfType<BoxCollider> ();
//		foreach (BoxCollider b in bc) 
//		{			
//			Light light = b.gameObject.AddComponent<Light>();
//			light.color = Color.blue;
//			light.intensity = 8.0F;
//			light.areaSize = new Vector2(2.0F, 2.0F);
//			light.enabled = false;
//		}
//	}

	private void Player(int playerNo)
	{
		if (playerNo == 1)
		{
			camera1.GetComponent<Camera> ().enabled = true;
			camera1.GetComponent<AudioListener> ().enabled = true;
			camera2.GetComponent<Camera> ().enabled = false;
			camera2.GetComponent<AudioListener> ().enabled = false;
			player1Turn = true;
//			playerColor = "White";
		}
		else
		{			
			camera1.GetComponent<Camera> ().enabled = false;
			camera1.GetComponent<AudioListener> ().enabled = false;
			camera2.GetComponent<Camera> ().enabled = true;
			camera2.GetComponent<AudioListener> ().enabled = true;
			player1Turn = false;
//			playerColor = "Black";
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.anyKeyDown)
		{
			// Select default piece, if none selected
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
				// White player choosing piece
				if (pieceSelect)
				{
					FindNextPiece(ref piece);
				}
				else if (zoneSelect)
				{
//					ZoneScript zone = FindContainingZone(SelectedPiece.gameObject);
//					FindNextZone(ref zone);
					if (TargetZone == null)
						TargetZone = SelectZone(piece.currentCol, piece.currentRow).gameObject;
					ZoneScript zs = TargetZone.GetComponent<ZoneScript>();
					FindNextZone(ref zs);
					//TargetZone.GetComponent<Light>().enabled = true;
//
//					if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
//					{
//						TargetZone = SelectZone((char)(zs.column + (char)1), zs.row).gameObject;
//					}
//					else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
//						TargetZone = SelectZone(zs.column, zs.row - 1).gameObject;
//					else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
//						TargetZone = SelectZone((char)(zs.column - (char)1), zs.row).gameObject;
//					else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
//						TargetZone = SelectZone(zs.column, zs.row + 1).gameObject;						
//					else if(Input.GetKeyDown(KeyCode.Escape))
//					{
//						pieceSelect = true;
//						zoneSelect = false;
//						TargetZone = null;
//					}
//					else if(Input.GetKeyDown(KeyCode.Return))
//					{
//						particlesAnimating = false;
//						ZoneScript z = FindContainingZone (SelectedPiece);
////						ParticleSystem pS = FindContainingZone (SelectedPiece).gameObject.GetComponent<ParticleSystem> ();
//
//						SelectedPiece.GetComponent<PawnBehaviourScript>().CheckMove(TargetZone.GetComponent<ZoneScript>());
//						MovePiece (SelectedPiece, TargetZone);
//						if (validMove) 
//						{
//							z.occupado = false;
//							SelectedPiece = null;
//							validMove = false;
////							pS.Stop ();
//							zoneSelect = false;
//							pieceSelect = true;
//							SelectedPiece = null;
//							Player(2);
//						}
//						TargetZone = null;
//					}
//					Debug.Log("TargetZone: " + TargetZone.name);
				}
			}
			catch(System.NullReferenceException ex)
			{
				Debug.Log("Nope, nothing there: " + ex.Message);
			}
		}

		// A piece has been selected
		if (SelectedPiece != null && TargetZone == null && (!particlesAnimating || CheckIfSelectedPieceHasChanged (SelectedPiece.GetComponent<PawnBehaviourScript> ()))) 
		{
//			ParticleSystem pS = FindContainingZone(SelectedPiece).gameObject.GetComponent<ParticleSystem> ();
//			pS.Play ();
			particlesAnimating = true;
		}
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
		}

		return SelectedPiece.GetComponent<PawnBehaviourScript>();
	}

	public ZoneScript FindNextZone(ref ZoneScript zs)
	{
		if (player1Turn) 
		{				
			if (Input.GetButtonDown("Up")) {
				TargetZone = SelectZone(zs.column, zs.row + 1).gameObject;
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
			pieceSelect = true;
			zoneSelect = false;
		}

		return TargetZone.GetComponent<ZoneScript>();
	}

	private bool CheckIfSelectedPieceHasChanged (PawnBehaviourScript p)
	{
		if(firstTime)
		{
			initialPiece = p;
			firstTime = false;
			return false;
		}
		else
		{
			if (initialPiece.Equals(p))
				return false;
			else
			{
//				ParticleSystem partSys = new ParticleSystem();
				if(initialPiece != null)
				{
//					partSys = FindContainingZone(initialPiece.gameObject).GetComponent<ParticleSystem>();
//					partSys.Stop();
				}
				initialPiece = p;
				return true;
			}
		}
	}

	public void MovePiece(GameObject piece, GameObject newLocation)
	{
		ZoneScript zone = newLocation.GetComponent<ZoneScript>();
//		BoxCollider bc = newLocation.GetComponent<BoxCollider>();
		PawnBehaviourScript pieceScript = piece.GetComponent<PawnBehaviourScript> ();
		bool pawnFirstMove = pieceScript.firstMove;
		char pieceCol = pieceScript.currentCol;
		int pieceRow = pieceScript.currentRow;
		char zoneCol = newLocation.GetComponent<ZoneScript>().column;
		int zoneRow = newLocation.GetComponent<ZoneScript>().row;

		pieceScript.Move (zoneCol, zoneRow);

		switch (piece.tag)
		{
		case "Pawn":
			if((zoneCol == (pieceCol+1) || zoneCol == (pieceCol-1))&&zoneRow == pieceRow)
			{
				validMove = true;
			}
			else if ((zoneCol == (pieceCol+2) || zoneCol == (pieceCol-2))&&zoneRow == pieceRow && pawnFirstMove)
			{
				validMove = true;
				piece.GetComponent<PawnBehaviourScript>().firstMove = false;
			}
			break;
		case "Rook":
			if(zoneCol == pieceCol || zoneRow == pieceRow)
				validMove = true;
			break;
		case "Bishop":
			for (int i = 1; i < 8; i++) 
			{
				if(zoneCol == pieceCol + i && zoneRow == pieceRow + i || zoneCol == pieceCol + i && zoneRow == pieceRow - i ||
				   zoneCol == pieceCol - i && zoneRow == pieceRow + i || zoneCol == pieceCol - i && zoneRow == pieceRow - i)
				{
					validMove = true;
					break;
				}
			}
			break;
		case "Knight":
			if(zoneCol == pieceCol + 1 && zoneRow == pieceRow + 2 || zoneCol == pieceCol + 1 && zoneRow == pieceRow - 2 ||
			   zoneCol == pieceCol - 1 && zoneRow == pieceRow + 2 || zoneCol == pieceCol - 1 && zoneRow == pieceRow - 2 ||
			   zoneCol == pieceCol + 2 && zoneRow == pieceRow + 1 || zoneCol == pieceCol + 2 && zoneRow == pieceRow - 1 ||
			   zoneCol == pieceCol - 2 && zoneRow == pieceRow + 1 || zoneCol == pieceCol - 2 && zoneRow == pieceRow - 1)
				validMove = true;
			break;
		case "Queen":
			if(zoneCol == pieceCol || zoneRow == pieceRow)
				validMove = true;
			for (int i = 1; i < 8; i++) 
			{
				if( zoneCol == pieceCol + i && zoneRow == pieceRow + i || zoneCol == pieceCol + i && zoneRow == pieceRow - i ||
			   zoneCol == pieceCol - i && zoneRow == pieceRow + i || zoneCol == pieceCol - i && zoneRow == pieceRow - i)
					validMove = true;
			}
			break;
		case "King":
			if(zoneCol == pieceCol && zoneRow == pieceRow + 1 || zoneCol == pieceCol -1 ||
			   zoneCol == pieceCol + 1 && zoneRow == pieceRow || zoneCol == pieceCol + 1 && zoneRow == pieceRow + 1 || zoneCol == pieceCol + 1 && zoneRow == pieceRow - 1 ||
			   zoneCol == pieceCol - 1 && zoneRow == pieceRow || zoneCol == pieceCol - 1 && zoneRow == pieceRow + 1 || zoneCol == pieceCol - 1 && zoneRow == pieceRow - 1)
				validMove = true;
			break;
		default:
			break;
		}
		if(validMove)
		{
			if(zone.occupado)
			{
				char col = zone.column;
				int row = zone.row;
				
				PawnBehaviourScript[] piecesContainingDestructor = FindObjectsOfType<PawnBehaviourScript>();
				foreach (PawnBehaviourScript p in piecesContainingDestructor) 
				{
					if (p.currentCol == col && p.currentRow == row)
						Destroy(p.gameObject);
				}
			}
			Destroy (piece);
//			piece = (GameObject)Instantiate (piece, bc.bounds.center - offset, Quaternion.identity);
			zone.occupado = false;
		}
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

	private ZoneScript SelectZone(char col, int row)
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