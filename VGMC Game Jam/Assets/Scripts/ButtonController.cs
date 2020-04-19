using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private GameObject mainPage, chirperOptions, start, Chirpr, replies, icon, name, stats;
    [SerializeField] private Button icon1, icon2, icon3, icon4;
    [SerializeField] private TMP_InputField user, display;
    [SerializeField] private Animator Transition1, Transition2;
    [SerializeField] private TextMeshProUGUI followers, cult, influence, theory, mom;


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
        Chirpr.SetActive(false);
        replies.SetActive(true);
        Transition1.SetTrigger("Fade");
        StartCoroutine(WaitAndShowSecondTweet());
    }

    IEnumerator WaitAndShowSecondTweet()
    {
        yield return new WaitForSecondsRealtime(15f);
        Transition2.SetTrigger("Fade");

        if (GameManager.Instance.isSecond)
        {
            Chirpr.SetActive(true);
            replies.SetActive(false);
            GameManager.Instance.setNextDay();
        }
        else
        {
            Chirpr.SetActive(true);
            replies.SetActive(false);
            GameManager.Instance.setChirperSecondTweet();
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


    //GAME END 
    public void LoseGame()
    {
        //show lose text
    }
    public void CultistWin()
    {
        //show cult win text
    }
    public void MomsWin()
    {
        //show mom win text
    }

    public void TheoristWin()
    {
        //show theorist win text
    }

    public void StanWin()
    {
        //show stan win text
    }
}
