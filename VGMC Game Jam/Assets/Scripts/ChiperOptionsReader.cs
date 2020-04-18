using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class ChiperOptionsReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadCSVFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadCSVFile()
    {
        string[,] chirperCSV = new string[29,13];
        StreamReader reader = new StreamReader("C:/Users/Cam/Documents/GitHub/VGMC-Game-Jam/VGMC Game Jam/Assets/CSV/ChirperOptions.csv");
        
        string line;
        int row = 0;

        // Read and display lines from the file until the end of 
        // the file is reached.
        while ((line = reader.ReadLine()) != null)
        {
            var col = 0;
            var data = line.Split(',');

            foreach (string s in data)
            {
                chirperCSV[row, col] = s;
                col++;
            }

            row++;
        }

        for (int i = 0; i < 30; i++)
        {
            for (int k = 0; k < 14; k++)
            {
                Debug.Log(chirperCSV[i, k]);
            }
        }

    }

}
