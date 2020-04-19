using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public struct ChirperTrendStruct
{
    public ChirperTrendStruct(int d, string title, string chirp, string number)
    {
        day = d;
        trending_title = title;
        trending_chirp = chirp;
        trending_number = number;
    }

    public int day { get; }
    public string trending_title { get; }
    public string trending_chirp { get; }
    public string trending_number { get; }
}

public class ChirperTrendsReader : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReadCSVFile()
    {
        List<ChirperTrendStruct> list = new List<ChirperTrendStruct>();
        List<string[]> tempList = new List<string[]>();
        StreamReader reader = new StreamReader("./Assets/TSV/Trends.tsv");

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

        GameManager.Instance.setChirperTrendsData(list);
    }

    ChirperTrendStruct ToStruct(string[] data)
    {
        int d = Int32.Parse(data[0]);
        string title = data[1];
        string chirp = data[2];
        string number = data[3];

        return new ChirperTrendStruct(d, title, chirp, number);
    }

}
