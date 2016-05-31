using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{ 
	private bool player1Turn = true;
	private bool pieceSelect = true;
	private bool zoneSelect = false;
	private GameObject whiteKing;
	private GameObject blackKing;

	public bool check;
	public bool checkMate;
	public GameObject camera1;
	public GameObject camera2;
	public GameObject SelectedPiece;
	public ZoneScript TargetZone;
    public ZoneScript SelectedZone;
	public Light greenHalo;
	public Light redHalo;
	public AudioSource nope;
    public GameObject promotionMenu;
	public GameObject gameOverMenu;
	public GameObject inCheck;

	public void NewGame()
	{
		SceneManager.LoadScene (1);
	}

	void Start ()
	{
       // SaveLoad.CreateGame(System.DateTime.Now.Date.ToString());
		Player (1);
		gameOverMenu.SetActive (false);
		inCheck.SetActive (false);
    }

	private bool CheckForCheck()
	{
		ArrayList whitePieces = new ArrayList ();
		ArrayList blackPieces = new ArrayList ();

		PawnBehaviourScript[] p = FindObjectsOfType<PawnBehaviourScript> ();
		foreach (PawnBehaviourScript piece in p)
			if (piece.tag != "King")
				if (piece.colour == "White")
					whitePieces.Add (piece);
				else
					blackPieces.Add(piece);
		p = null;
		foreach (PawnBehaviourScript whitePiece in whitePieces)
			if (whitePiece.CheckMove (FindContainingZone (blackKing.gameObject)))
			{
				whitePieces = null;
				blackPieces = null;
				inCheck.SetActive (true);
				return true;
			}
		foreach (PawnBehaviourScript blackPiece in blackPieces)
			if (blackPiece.CheckMove (FindContainingZone (whiteKing.gameObject)))
			{
				whitePieces = null;
				blackPieces = null;
				inCheck.SetActive (true);
				return true;
			}
		inCheck.SetActive (false);
		return false;
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
		if (blackKing != null && whiteKing != null)
		{
			CheckForCheck ();
		}
		else
		{
			GameObject[] kings = GameObject.FindGameObjectsWithTag("King");
			foreach (var king in kings)
			{
				if (king.GetComponent<PawnBehaviourScript> ().colour == "White")
					whiteKing = king;
				else
					blackKing = king;
			}
			kings = null;
		}
	}

    // Update is called once per frame
    void Update () 
	{
		// TODO Controller input
		if (Mathf.Abs (Input.GetAxis ("Vertical")) == 1.0F || Mathf.Abs (Input.GetAxis ("Horizontal")) == 1.0F)
			Debug.Log ("Sorry, controller input not yet supported.");
		
		if(Input.anyKeyDown)
		{
			// Select default piece (king), if none selected
			if (SelectedPiece == null)
				SetDefaultPiece ();
				// Game is over if a king can't be selected
				if (SelectedPiece == null)
					GameOver ();

			PawnBehaviourScript piece = SelectedPiece.GetComponent<PawnBehaviourScript>();

			try
			{
				if (pieceSelect)
				{
					FindNextPiece();
				}
				else if (zoneSelect)
				{
					if (TargetZone == null)
						TargetZone = SelectZone(piece.currentCol, piece.currentRow);
					FindNextZone(ref TargetZone);

					//A piece and zone have been selected
					if(!pieceSelect && !zoneSelect)
					{
						if(piece.CheckMove(TargetZone))
						{
							piece.Move(TargetZone.column, TargetZone.row);
							TargetZone.AcceptPiece(piece);
//                            string move = piece.name + "," + TargetZone.column + TargetZone.row + "\n";
                            // TODO SaveLoad.Update(move);
                            piece = null;
							TargetZone = null;
                            SelectedZone = null;
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
				Debug.Log("Nope, nothing there. " + ex.HelpLink);
				nope.Play();
			}
			catch(System.Exception ex)
			{
				Debug.Log("Something went wrong between selecting a zone/piece and making a move. " + ex.HelpLink);
			}
			SetHalos ();
		}
	}

	private void SetHalos()
	{
		if ( SelectedZone != null )
			redHalo.transform.position = (SelectedZone.transform.position + new Vector3(0, 3.5f, 0) );
		//redHalo.transform.position = (SelectedPiece.transform.position + new Vector3(0, 3.5f, 0));
		else
			redHalo.transform.position = new Vector3(250, 250, 250);
		if(TargetZone != null)
			greenHalo.transform.position = (TargetZone.transform.position + new Vector3(0, 1.5f, 0));
		else
			greenHalo.transform.position = new Vector3 (250, 250, 250);
	}

	public void GameOver()
	{
		gameOverMenu.SetActive(true);
	}

	void SetDefaultPiece()
	{
		GameObject[] DefaultPiece = GameObject.FindGameObjectsWithTag("King");
		foreach (var king in DefaultPiece) {
			if ( player1Turn && king.GetComponent<PawnBehaviourScript>().colour == "White" )
			{
				SelectedPiece = king;
				SelectedZone = FindContainingZone(king);
			}
			else if ( !player1Turn && king.GetComponent<PawnBehaviourScript>().colour == "Black" )
			{
				SelectedPiece = king;
				SelectedZone = FindContainingZone(king);
			}
		}
	}
    
    public void FindNextPiece()
    {
        if ( SelectedZone != null )
        {
            if ( player1Turn )
            {
                if ( Input.GetButtonDown("Up") && SelectedZone.row <= 8 )
                {
                    SelectedZone = SelectZone(SelectedZone.column, SelectedZone.row + 1);
                }
                else if ( Input.GetButtonDown("Down") && SelectedZone.row > 0 )
                {
                    SelectedZone = SelectZone(SelectedZone.column, SelectedZone.row - 1);
                }
                else if ( Input.GetButtonDown("Right") && SelectedZone.column != 'H' )
                {
                    SelectedZone = SelectZone((char) ( SelectedZone.column + (char) 1 ), SelectedZone.row);
                }
                else if ( Input.GetButtonDown("Left") && SelectedZone.column != 'A' )
                {
                    SelectedZone = SelectZone((char) ( SelectedZone.column - (char) 1 ), SelectedZone.row);
                }
                if ( Input.GetButtonDown("Select") && SelectedZone.occupado && SelectedZone.containedPiece.colour == "White" )
                {
                    SelectedPiece = SelectedZone.containedPiece.gameObject;
                    pieceSelect = false;
                    zoneSelect = true;
                }
            }
            else
            {
                if ( Input.GetButtonDown("Down") )
                {
                    SelectedZone = SelectZone(SelectedZone.column, SelectedZone.row + 1);
                }
                else if ( Input.GetButtonDown("Up") )
                {
                    SelectedZone = SelectZone(SelectedZone.column, SelectedZone.row - 1);
                }
                else if ( Input.GetButtonDown("Left") )
                {
                    SelectedZone = SelectZone((char) ( SelectedZone.column + (char) 1 ), SelectedZone.row);
                }
                else if ( Input.GetButtonDown("Right") )
                {
                    SelectedZone = SelectZone((char) ( SelectedZone.column - (char) 1 ), SelectedZone.row);
                }
                if ( Input.GetButtonDown("Select") && SelectedZone.occupado && SelectedZone.containedPiece.colour == "Black" )
                {
                    SelectedPiece = SelectedZone.containedPiece.gameObject;
                    pieceSelect = false;
                    zoneSelect = true;
                }
            }
        }
        else
            if ( SelectedPiece != null )
               SelectedZone = FindContainingZone(SelectedPiece);
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
			else if (Input.GetButtonDown("Right")) {
			TargetZone = SelectZone((char)(TargetZone.column + (char)1), TargetZone.row);
			}
			else if (Input.GetButtonDown("Left")) {
			TargetZone = SelectZone((char)(TargetZone.column - (char)1), TargetZone.row);
			}
		}
		else
		{
			if (Input.GetButtonDown("Up")) {
			TargetZone = SelectZone(TargetZone.column, TargetZone.row - 1);
			}
			else if (Input.GetButtonDown("Down")) {
			TargetZone = SelectZone(TargetZone.column, TargetZone.row + 1);
			}
			else if (Input.GetButtonDown("Right")) {
			TargetZone = SelectZone((char)(TargetZone.column - (char)1), TargetZone.row);
			}
			else if (Input.GetButtonDown("Left")) {
			TargetZone = SelectZone((char)(TargetZone.column + (char)1), TargetZone.row);
			}
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

    public void PromotionMenu(bool on)
    {
        promotionMenu.SetActive(on);
    }

    public void PromotePiece(string u)
    {
        ChessBoard pb = GameObject.FindObjectOfType<ChessBoard>();
        PawnBehaviourScript[] pieces = GameObject.FindObjectsOfType<PawnBehaviourScript>();
        foreach ( var piece in pieces )
            if ( piece.tag == "Pawn" )
                if ( piece.currentRow == 1 || piece.currentRow == 8)
                {
                    pb.PromotePiece(piece, u);
                    break;
                }
        GameObject.FindGameObjectWithTag("Promotion").SetActive(false);
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