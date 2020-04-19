﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private GameObject chirperOptions, instantOptions;

    //Goes to the home page of Chirper
    public void GoToChirper()
    {

    }

    //Allows player to choose what to post on Chirper
    public void PostOnChirper()
    {
        GameManager.Instance.populateChirperOptions();
        chirperOptions.SetActive(true);
    }
}
