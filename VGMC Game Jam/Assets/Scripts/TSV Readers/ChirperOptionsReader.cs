using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;



public struct ChirperOptionsStruct
{
    public ChirperOptionsStruct(int d,
        string op1, int delta1, GameManager.FollowerType type1,
        string op2, int delta2, GameManager.FollowerType type2,
        string op3, int delta3, GameManager.FollowerType type3,
        string op4, int delta4, GameManager.FollowerType type4)
    {
        day = d;

        delta_1 = delta1;
        delta_2 = delta2;
        delta_3 = delta3;
        delta_4 = delta4;

        option_1 = op1;
        option_2 = op2;
        option_3 = op3;
        option_4 = op4;

        type_1 = type1;
        type_2 = type2;
        type_3 = type3;
        type_4 = type4;
    }

    public int day { get; }

    public int delta_1 { get; }
    public int delta_2 { get; }
    public int delta_3 { get; }
    public int delta_4 { get; }

    public string option_1 { get; }
    public string option_2 { get; }
    public string option_3 { get; }
    public string option_4 { get; }

    public GameManager.FollowerType type_1 { get; }
    public GameManager.FollowerType type_2 { get; }
    public GameManager.FollowerType type_3 { get; }
    public GameManager.FollowerType type_4 { get; }


}

public class ChirperOptionsReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadCSVFile();
    }

    void ReadCSVFile()
    {
        List<ChirperOptionsStruct> list = new List<ChirperOptionsStruct>();
        List<string[]> tempList = new List<string[]>();
        StreamReader reader = new StreamReader("C:/Users/Cam/Documents/GitHub/VGMC-Game-Jam/VGMC Game Jam/Assets/TSV/ChirperOptions.tsv");

        string line;


        // Read and display lines from the file until the end of 
        // the file is reached.
        while ((line = reader.ReadLine()) != null)
        {
            var data = line.Split('\t');

            tempList.Add(data);

        }


        for (int i = 1; i < tempList.Count; i++)
        {

            list.Add(ToStruct(tempList[i]));
        }

        GameManager.Instance.setChirperOptionsStruct(list);
    }

    ChirperOptionsStruct ToStruct(string[] data)
    {
        int d = Int32.Parse(data[0]);
        string op1 = data[1];
        int delta1 = Int32.Parse(data[2]);
        GameManager.FollowerType type1 = determineType(data[3]);

        string op2 = data[4];
        int delta2 = Int32.Parse(data[5]);
        GameManager.FollowerType type2 = determineType(data[6]);

        string op3 = data[7];
        int delta3 = Int32.Parse(data[8]);
        GameManager.FollowerType type3 = determineType(data[9]);

        string op4 = data[10];
        int delta4 = Int32.Parse(data[11]);
        GameManager.FollowerType type4 = determineType(data[12]);


        return new ChirperOptionsStruct(d,
        op1, delta1, type1,
        op2, delta2, type2,
        op3, delta3, type3,
        op4, delta4, type4);
    }

    GameManager.FollowerType determineType(string val)
    {
        GameManager.FollowerType type = 0;
        switch (val)
        {
            case "STANS":
                type = GameManager.FollowerType.STANS;
                break;
            case "CULTISTS":
                type = GameManager.FollowerType.CULTISTS;
                break;
            case "THEROISTS":
                type = GameManager.FollowerType.THEROISTS;
                break;
            case "MOMS":
                type = GameManager.FollowerType.MOMS;
                break;
            default:
                Debug.Log("Error un ChirperReader");
                break;
        }

        return type;
    }

}
