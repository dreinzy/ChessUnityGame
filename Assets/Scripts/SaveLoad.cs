using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static void CreateGame(string gameNo)
    {
        if ( !File.Exists("C:\\Users\\drein\\Desktop\\ChessSaves\\" + gameNo + ".txt") )
            File.Create("C:\\Users\\drein\\Desktop\\ChessSaves\\" + gameNo + ".txt");
        else
            Debug.Log("Saved game with this number already exists");
    }

    public static void LoadGame(string gameNo)
    {

    }

    public static void Update(string move)
    {
        File.AppendAllText("C:\\Users\\drein\\Desktop\\ChessSaves\\1.txt", move);
    }
}