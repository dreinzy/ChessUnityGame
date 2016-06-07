using UnityEngine;
using System.Collections;

public class ChessBoard : MonoBehaviour {
    
	private Vector3 offset = new Vector3 (0.0F, 1.0F); //Used when instantiating pieces, this is distance above the board

	public GameObject pawn;
	public GameObject rook;
	public GameObject bishop;
	public GameObject knight;
	public GameObject queen;
	public GameObject king;
	public GameObject blackPawn;
	public GameObject blackRook;
	public GameObject blackBishop;
	public GameObject blackKnight;
	public GameObject blackQueen;
	public GameObject blackKing;

	// Use this for initialization
	void Start () {
		//MakeParticles ();
		PopulateBoard ();
	}

	public void AnimateMenu()
	{
		PopulateBoard ();
		offset -= new Vector3 (0.0F, 1.0F);
	}

	private void PopulateBoard()
	{
		int pawnCount = 1;
		int rookCount = 1;
		int bishopCount = 1;
		int knightCount = 1;
		int queenCount = 1;
		int kingCount = 1;
		string pieceName;

		BoxCollider[] bc = FindObjectsOfType<BoxCollider>();
		foreach (BoxCollider boxCollider in bc)
		{
			if (boxCollider.name == "A2" || boxCollider.name == "B2" || boxCollider.name == "C2" || boxCollider.name == "D2" || boxCollider.name == "E2" || boxCollider.name == "F2" || boxCollider.name == "G2" || boxCollider.name == "H2")
			{
				pieceName = "Pawn " + pawnCount.ToString();
				GameObject Pawn = (GameObject)Instantiate(pawn, boxCollider.bounds.center - offset, Quaternion.identity);
				Pawn.tag = "Pawn";
				Pawn.name = pieceName;
				Pawn.AddComponent<PawnBehaviourScript>();
				Pawn.AddComponent<Rigidbody>();
				Pawn.AddComponent<BoxCollider>();
				pawnCount++;
			}
			else if (boxCollider.name == "A7" || boxCollider.name == "B7" || boxCollider.name == "C7" || boxCollider.name == "D7" || boxCollider.name == "E7" || boxCollider.name == "F7" || boxCollider.name == "G7" || boxCollider.name == "H7")
			{
				pieceName = "Pawn " + pawnCount.ToString();
				GameObject Pawn = (GameObject)Instantiate(blackPawn, boxCollider.bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
				Pawn.tag = "Pawn";
				Pawn.name = pieceName;
				Pawn.AddComponent<PawnBehaviourScript>();
				Pawn.AddComponent<Rigidbody>();
				Pawn.AddComponent<BoxCollider>();
				pawnCount++;
			}
			else if (boxCollider.name == "A1" || boxCollider.name == "H1" )
			{
				pieceName = "Rook " + rookCount.ToString();
				GameObject Rook = (GameObject)Instantiate(rook, boxCollider.bounds.center - offset, Quaternion.identity);
				Rook.tag = "Rook";
				Rook.name = pieceName;
				Rook.AddComponent<PawnBehaviourScript>();
				Rook.AddComponent<Rigidbody>();
				Rook.AddComponent<BoxCollider>();
				rookCount++;
			}
			else if (boxCollider.name == "A8" || boxCollider.name == "H8") {
				pieceName = "Rook " + rookCount.ToString();
				GameObject Rook = (GameObject)Instantiate(blackRook, boxCollider.bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
				Rook.tag = "Rook";
				Rook.name = pieceName;
				Rook.AddComponent<PawnBehaviourScript>();
				Rook.AddComponent<Rigidbody>();
				Rook.AddComponent<BoxCollider>();
				rookCount++;
			}
			else if (boxCollider.name == "B1" || boxCollider.name == "G1")
			{
				pieceName = "Knight " + knightCount.ToString();
				GameObject Knight = (GameObject)Instantiate(knight, boxCollider.bounds.center - offset, Quaternion.identity);
				Knight.tag = "Knight";
				Knight.name = pieceName;
				Knight.AddComponent<PawnBehaviourScript>().currentCol = 'A';
				Knight.AddComponent<Rigidbody>();
				Knight.AddComponent<BoxCollider>();
				knightCount++;
			}
			else if (boxCollider.name == "B8" || boxCollider.name == "G8") {
				pieceName = "Knight " + knightCount.ToString();
				GameObject Knight = (GameObject)Instantiate(blackKnight, boxCollider.bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
				Knight.tag = "Knight";
				Knight.name = pieceName;
				Knight.AddComponent<PawnBehaviourScript>().currentCol = 'A';
				Knight.AddComponent<Rigidbody>();
				Knight.AddComponent<BoxCollider>();
				knightCount++;
			}
			else if (boxCollider.name == "C1" || boxCollider.name == "F1")
			{
				pieceName = "Bishop " + bishopCount.ToString();
				GameObject Bishop = (GameObject)Instantiate(bishop, boxCollider.bounds.center - offset, Quaternion.identity);
				Bishop.tag = "Bishop";
				Bishop.name = pieceName;
				Bishop.AddComponent<PawnBehaviourScript>();
				Bishop.AddComponent<Rigidbody>();
				Bishop.AddComponent<BoxCollider>();
				bishopCount++;
			}
			else if (boxCollider.name == "C8" || boxCollider.name == "F8")
			{
				pieceName = "Bishop " + bishopCount.ToString();
				GameObject Bishop = (GameObject)Instantiate(blackBishop, boxCollider.bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
				Bishop.tag = "Bishop";
				Bishop.name = pieceName;
				Bishop.AddComponent<PawnBehaviourScript>();
				Bishop.AddComponent<Rigidbody>();
				Bishop.AddComponent<BoxCollider>();
				bishopCount++;
			}
			else if (boxCollider.name == "D1")
			{
				pieceName = "Queen " + queenCount.ToString();
				GameObject Queen = (GameObject)Instantiate(queen, boxCollider.bounds.center - offset, Quaternion.AngleAxis(270, Vector3.up));
				Queen.tag = "Queen";
				Queen.name = pieceName;
				Queen.AddComponent<PawnBehaviourScript>();
				Queen.AddComponent<Rigidbody>();
				Queen.AddComponent<BoxCollider>();
				queenCount++;
			}
			else if (boxCollider.name == "D8") {
				pieceName = "Queen " + queenCount.ToString();
				GameObject Queen = (GameObject)Instantiate(blackQueen, boxCollider.bounds.center - offset, Quaternion.AngleAxis(90, Vector3.up));
				Queen.tag = "Queen";
				Queen.name = pieceName;
				Queen.AddComponent<PawnBehaviourScript>();
				Queen.AddComponent<Rigidbody>();
				Queen.AddComponent<BoxCollider>();
				queenCount++;
			}
			else if (boxCollider.name == "E1")
			{
				pieceName = "King " + kingCount.ToString();
				GameObject King = (GameObject)Instantiate(king, boxCollider.bounds.center - offset, Quaternion.AngleAxis(270, Vector3.up));
				King.tag = "King";
				King.name = pieceName;
				King.AddComponent<PawnBehaviourScript>();
				King.AddComponent<Rigidbody>();
				King.AddComponent<BoxCollider>();
				kingCount++;
			}
			else if (boxCollider.name == "E8") {
				pieceName = "King " + kingCount.ToString();
				GameObject King = (GameObject)Instantiate(blackKing, boxCollider.bounds.center - offset, Quaternion.AngleAxis(90, Vector3.up));
				King.tag = "King";
				King.name = pieceName;
				King.AddComponent<PawnBehaviourScript>();
				King.AddComponent<Rigidbody>();
				King.AddComponent<BoxCollider>();
				kingCount++;
			}
		}
		PawnBehaviourScript[] pieces = FindObjectsOfType<PawnBehaviourScript>();
		for (int i = 0; i < pieces.Length; i++) {
			if (pieces[i].currentRow == 7 || pieces[i].currentRow == 8)
				pieces[i].colour = "Black";
		}
	}

	private void MakeParticles()
	{ 
		ZoneScript[] zone = FindObjectsOfType<ZoneScript> ();
		foreach (ZoneScript z in zone) 
		{
			ParticleSystem particleSystem = z.gameObject.AddComponent<ParticleSystem>();
			particleSystem.startColor = Color.red;
			//particleSystem.emissionRate = 50.0F;	DEPRECATED and emission.rate uneditable
			particleSystem.startLifetime = 1.0F;
			particleSystem.startSpeed = 30.0F;
			particleSystem.startSize = 2.0F;
			particleSystem.startRotation = 0.0F;
			particleSystem.Stop();
		}
	}

    public void PromotePiece(PawnBehaviourScript piece, string newType)
    {
        GameObject Replacement = new GameObject();
        if ( piece.colour == "White" )
        {
            switch ( newType )
            {
                case "Queen":
                    Replacement = (GameObject) Instantiate(queen, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Queen";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                case "Knight":
                    Replacement = (GameObject) Instantiate(knight, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Knight";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                case "Rook":
                    Replacement = (GameObject) Instantiate(rook, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Rook";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                case "Bishop":
                    Replacement = (GameObject) Instantiate(bishop, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Bishop";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                default:
                    break;
            }            
        }
        else if ( piece.colour == "Black" )
        {
            switch ( newType )
            {
                case "Queen":
                    Replacement = (GameObject) Instantiate(blackQueen, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.identity);
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Queen";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                case "Knight":
                    Replacement = (GameObject) Instantiate(blackKnight, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Knight";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                case "Rook":
                    Replacement = (GameObject) Instantiate(blackRook, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Rook";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                case "Bishop":
                    Replacement = (GameObject) Instantiate(blackBishop, GameController.FindContainingZone(piece.gameObject).GetComponent<BoxCollider>().bounds.center - offset, Quaternion.AngleAxis(180, Vector3.up));
                    Destroy(GameController.FindContainingZone(piece.gameObject).containedPiece.gameObject);
                    Replacement.tag = "Bishop";
                    Replacement.AddComponent<PawnBehaviourScript>();
                    Replacement.AddComponent<Rigidbody>();
                    Replacement.AddComponent<BoxCollider>();
                    break;
                default:
                    break;
            }
        }
    }
}