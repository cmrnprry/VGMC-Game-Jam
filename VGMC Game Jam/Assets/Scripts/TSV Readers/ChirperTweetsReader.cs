using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;


public struct ChirpStruct
{
    public ChirpStruct(int d, string pic, string name, string user, string content)
    {
        day = d;
        profile_pic = pic;
        chirper_name = name;
        user_name = user;
        chirp_content = content;
    }

    public int day { get; }

    public string profile_pic { get; }
    public string chirper_name { get; }
    public string user_name { get; }
    public string chirp_content { get; }

}

public class ChirperTweetsReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadCSVFile();
    }

    void ReadCSVFile()
    {
        List<ChirpStruct> list = new List<ChirpStruct>();
        List<string[]> tempList = new List<string[]>();
        StreamReader reader = new StreamReader("C:/Users/Cam/Documents/GitHub/VGMC-Game-Jam/VGMC Game Jam/Assets/TSV/Chirps.tsv");

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

        GameManager.Instance.setChirperChirpData(list);
    }

    ChirpStruct ToStruct(string[] data)
    {
        int d = Int32.Parse(data[0]);
        string pic = data[1];
        string name = data[2];
        string user = data[3];
        string content = data[4];
       
        return new ChirpStruct(d, pic, name, user, content);
    }

}
