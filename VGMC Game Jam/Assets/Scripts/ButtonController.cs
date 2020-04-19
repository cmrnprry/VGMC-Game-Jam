using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private GameObject mainPage, chirperOptions, start, Chirpr, replies;
    
    //Goes to the home page of Chirper
    public void GoToChirper()
    {
        Chirpr.SetActive(true);
        mainPage.SetActive(false);
    }
    public void GoToMain()
    {
        start.SetActive(false);
        mainPage.SetActive(true);
    }

    //Allows player to choose what to post on Chirper
    public void PostOnChirper()
    {
        GameManager.Instance.populateChirperOptions();
        chirperOptions.SetActive(true);
    }

    public void OpenReplies()
    {
        Chirpr.SetActive(false);
        replies.SetActive(true);
    }
}
