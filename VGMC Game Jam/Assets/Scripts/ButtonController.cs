using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    [Header("Pages to Show and Hide")]
    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject chirperOptions;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject Chirpr;
    [SerializeField] private GameObject replies;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject name;
    [SerializeField] private GameObject stats;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject StartScreen;

    [Header("User Icons")]
    [SerializeField] private Button icon1;
    [SerializeField] private Button icon2;
    [SerializeField] private Button icon3;
    [SerializeField] private Button icon4;

    [Header("User Input")]
    [SerializeField] private TMP_InputField user;
    [SerializeField] private TMP_InputField display;

    [Header("Transitions")]
    [SerializeField] private Animator Transition1;
    [SerializeField] private Animator Transition2;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI followers;
    [SerializeField] private TextMeshProUGUI cult;
    [SerializeField] private TextMeshProUGUI influence;
    [SerializeField] private TextMeshProUGUI theory;
    [SerializeField] private TextMeshProUGUI mom;

    [Header("End Text")]
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject winCult;
    [SerializeField] private GameObject winInfluence;
    [SerializeField] private GameObject WinTheory;
    [SerializeField] private GameObject winMom;

    [Header("TSVs")]
    [SerializeField] private ChirperOptionsReader optionsReader;
    [SerializeField] private ChirperTweetsReader chirpsReader;
    [SerializeField] private ChirprRepliesReader repliesReader;
    [SerializeField] private ChirperFollowerReader followersReader;
    [SerializeField] private ChirperTrendsReader trendsReader;



    void Start()
    {
        icon1.onClick.AddListener(delegate { ChooseIcon("darkgod-cult"); });
        icon2.onClick.AddListener(delegate { ChooseIcon("darkgod-theorists"); });
        icon3.onClick.AddListener(delegate { ChooseIcon("darkgod-influencer"); });
        icon4.onClick.AddListener(delegate { ChooseIcon("darkgod-moms"); });
    }

    //Goes to the home page of Chirper
    public void GoToChirper()
    {
        Chirpr.SetActive(true);
        mainPage.SetActive(false);
    }
    public void GoToMain()
    {
        //Parse Through the TSV
        Debug.Log("1");

        optionsReader.ReadCSVFile();
        Debug.Log("2");

        chirpsReader.ReadCSVFile();
        Debug.Log("3");

        repliesReader.ReadCSVFile();
        Debug.Log("4");

        followersReader.ReadCSVFile();
        Debug.Log("5");

        trendsReader.ReadCSVFile();

        //Debug.Log("6");

        name.SetActive(false);
        mainPage.SetActive(true);
    }

    public void SetNamer()
    {
        GameManager.Instance.userName = user.text;
        GameManager.Instance.displayName = display.text;
    }

    public void GoToChooseName()
    {
        icon.SetActive(false);
        name.SetActive(true);
    }

    public void GoToChooseIcon()
    {
        start.SetActive(false);
        icon.SetActive(true);
    }

    //Allows player to choose what to post on Chirper
    public void PostOnChirper()
    {
        GameManager.Instance.populateChirperOptions();
        chirperOptions.SetActive(true);
    }

    public void OpenReplies()
    {
        Transition2.gameObject.SetActive(false);
        Chirpr.SetActive(false);
        chirperOptions.SetActive(false);
        replies.SetActive(true);
        Transition1.SetTrigger("Fade");
        StartCoroutine(WaitAndShowSecondTweet());
    }

    IEnumerator WaitAndShowSecondTweet()
    {
        yield return new WaitForSecondsRealtime(15f);
        Transition2.gameObject.SetActive(true);
        Transition2.SetTrigger("Fade");

        if (GameManager.Instance.isSecond)
        {
            Chirpr.SetActive(true);
            replies.SetActive(false);
            GameManager.Instance.setChirperSecondTweet();
        }
        else
        {
            Chirpr.SetActive(true);
            replies.SetActive(false);
            GameManager.Instance.setNextDay();
            
        }

    }

    public void ChooseIcon(string name)
    {
        GameManager.Instance.profileName = name;
    }

    public void ShowStats()
    {
        followers.text = GameManager.Instance.getTotalFollowers().ToString();
        cult.text = GameManager.Instance.getTotalCult().ToString();
        influence.text = GameManager.Instance.getTotalStans().ToString();
        theory.text = GameManager.Instance.getTotalTheory().ToString();
        mom.text = GameManager.Instance.getTotalMoms().ToString();

        stats.SetActive(true);
        Chirpr.SetActive(false);
    }

    public void ReturntoChirpr()
    {
        stats.SetActive(false);
        Chirpr.SetActive(true);
    }

    public void ReturntoStartScreen()
    {
        StartScreen.SetActive(true);
        Chirpr.SetActive(false);
        stats.SetActive(false);
        replies.SetActive(false);
        start.SetActive(false);
        credits.SetActive(false);
        endScreen.SetActive(false);
        icon.SetActive(false);
    }


    //GAME END 

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
    }
    public void LoseGame()
    {
        //show lose text
        lose.SetActive(true);
    }
    public void CultistWin()
    {
        //show cult win text
        winCult.SetActive(true);
    }
    public void MomsWin()
    {
        //show mom win text
        winMom.SetActive(true);

    }

    public void TheoristWin()
    {
        //show theorist win text
        WinTheory.SetActive(true);

    }

    public void StanWin()
    {
        //show stan win text
        winInfluence.SetActive(true);

    }
}
