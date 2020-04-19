using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;


public struct ChirpStruct
{
    public ChirpStruct(int d, int isAgain, string pic, string name, string user, string content)
    {
        day = d;
        isChirpTwo = isAgain;
        profile_pic = pic;
        chirper_name = name;
        user_name = user;
        chirp_content = content;
    }

    public int day { get; }
    public int isChirpTwo { get; }

    public string profile_pic { get; }
    public string chirper_name { get; }
    public string user_name { get; }
    public string chirp_content { get; }

}

public class ChirperTweetsReader : MonoBehaviour
{
    // Start is called before the first frame update
   public void ReadCSVFile()
    {
        List<ChirpStruct> list = new List<ChirpStruct>();
        List<string[]> tempList = new List<string[]>();
        StreamReader reader = new StreamReader("./Assets/TSV/Chirps.tsv");

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
        int two = Int32.Parse(data[1]);
        string pic = data[2];
        string name = data[3];
        string user = data[4];
        string content = data[5];
       
        return new ChirpStruct(d, two, pic, name, user, content);
    }

}
