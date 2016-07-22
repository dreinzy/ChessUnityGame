using UnityEngine;
using System.Collections;

public class ZoneScript : MonoBehaviour
{
    private bool attackedByWhite;
    private bool attackedByBlack;
    public char column;
    public int row;
    public bool occupado = false;
    public bool setupStep = true;
    public PawnBehaviourScript containedPiece;

    public bool AttackedByWhite { get; set; }

    public bool AttackedByBlack { get; set; }

    void OnTriggerEnter(Collider piece)
    {
        containedPiece = piece.GetComponent<PawnBehaviourScript>();
        containedPiece.SetCurrentPosition(column, row);
        occupado = true;
        if (setupStep && (row == 7 || row == 8))
            containedPiece.colour = "Black";
        setupStep = false;
    }

    public void AcceptPiece(PawnBehaviourScript piece)
    {
        //containedPiece.enabled = false;
        containedPiece = piece;
        containedPiece.SetCurrentPosition(column, row);
        occupado = true;
        if (setupStep && (row == 7 || row == 8))
        {
            containedPiece.colour = "Black";
            setupStep = false;
        }
    }

    void OnTriggerExit(Collider piece)
    {
        occupado = false;
        //Debug.Log(((char)column).ToString() + row.ToString() + " empty");
    }

    void OnMouseDown()
    {
        Debug.Log("CLICK");
    }
}