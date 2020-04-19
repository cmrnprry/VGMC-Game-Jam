using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public struct ChirperFollowerStruct
{
    public ChirperFollowerStruct(int d, string pic, string name, string user)
    {
        day = d;
        profile_pic = pic;
        chirper_name = name;
        user_name = user;
    }

    public int day { get; }

    public string profile_pic { get; }
    public string chirper_name { get; }
    public string user_name { get; }

}
public class ChirperFollowerReader : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReadCSVFile()
    {
        List<ChirperFollowerStruct> list = new List<ChirperFollowerStruct>();
        List<string[]> tempList = new List<string[]>();
        StreamReader reader = new StreamReader("./Assets/TSV/Followers.tsv");

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

        GameManager.Instance.setChirperFollowersData(list);
    }

    ChirperFollowerStruct ToStruct(string[] data)
    {
        int d = Int32.Parse(data[0]);
        string pic = data[1];
        string name = data[2];
        string user = data[3];

        return new ChirperFollowerStruct(d, pic, name, user);
    }
}
