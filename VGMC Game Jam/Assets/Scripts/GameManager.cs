using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Class getter and static instance
    private static GameManager managerInstance;
    public static GameManager Instance { get { return managerInstance; } }

    //Approval rating
    private int day, totalCultist, totalMoms, totalTheorists, totalStans, totalFollowers;

    //Data from the TSV
    private List<ChirperOptionsStruct> chirperOptionsData = new List<ChirperOptionsStruct>();
    private List<ChirpStruct> chirperChirpData = new List<ChirpStruct>();
    private List<ChirperTrendStruct> chirperTrendsData = new List<ChirperTrendStruct>();

    //Types of followers
    public enum FollowerType { CULTISTS = 0, MOMS = 1, THEROISTS = 2, STANS = 3 };

    //List of profile Pictures
    [Header("Profile Photos")]
    [SerializeField] private List<Sprite> profiles = new List<Sprite>();

    //Objects To be Populated
    [Header("Chirper Options")]
    [SerializeField] private Button Option1;
    [SerializeField] private Button Option2;
    [SerializeField] private Button Option3;
    [SerializeField] private Button Option4;
    [SerializeField] private TextMeshProUGUI Option1Text;
    [SerializeField] private TextMeshProUGUI Option2Text;
    [SerializeField] private TextMeshProUGUI Option3Text;
    [SerializeField] private TextMeshProUGUI Option4Text;

    [Header("Chirper Chirps")]
    [SerializeField] private TextMeshProUGUI[] ChirpNameText;
    [SerializeField] private TextMeshProUGUI[] ChirpUserText;
    [SerializeField] private TextMeshProUGUI[] ChirpContentText; 
    [SerializeField] private Image[] ChirpImage;

    [Header("Chirper Trends")]
    [SerializeField] private TextMeshProUGUI[] ChirpTitleText;
    [SerializeField] private TextMeshProUGUI[] ChirpChirpText;
    [SerializeField] private TextMeshProUGUI[] ChirpNumberText;


    // Start is called before the first frame update
    void Awake()
    {
        if (managerInstance != null && managerInstance != this)
        {
            Debug.Log("Multiple stats managers!");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Initializing Stats Manager!");
            managerInstance = this;
            Debug.Log("Stat Manager Loaded!");

        }

        day = 1;
    }

    //  Methods for adjusting stats from outside
    public void addFollower(FollowerType type, int delta)
    {
        //updates teh hidden stats
        switch ((int)type)
        {
            case 0:
                totalCultist += delta;
                break;
            case 1:
                totalMoms += delta;
                break;
            case 2:
                totalStans += delta;
                break;
            case 3:
                totalTheorists += delta;
                break;
            default:
                Debug.Log("Error in method 'addFollowers'");
                break;
        }
        
        //keeps the total number of follower up to date
        totalFollowers += delta;
    }

    public void setChirperOptionsStruct(List<ChirperOptionsStruct> data)
    {
        chirperOptionsData = data;
    }

    public void setChirperChirpData(List<ChirpStruct> data)
    {
        chirperChirpData = data;
    }

    public void setChirperTrendsData(List<ChirperTrendStruct> data)
    {
        chirperTrendsData = data;
    }

    public void populateChirperOptions()
    {
        ChirperOptionsStruct data = chirperOptionsData[day];

        Option1Text.text = data.option_1;
        Option2Text.text = data.option_2;
        Option3Text.text = data.option_3;
        Option4Text.text = data.option_4;
    }

    public void populateChirperChirps()
    {
        ChirpStruct data = chirperChirpData[day];
        Sprite image = null;

        foreach(Sprite img in profiles)
        {
            if (data.profile_pic == img.name)
            {
                image = img;
                break;
            }
        }

        ChirpImage[day - 1].sprite = image;
        ChirpNameText[day - 1].text = data.chirper_name;
        ChirpUserText[day - 1].text = data.user_name;
        ChirpContentText[day - 1].text = data.chirp_content;
    }
}
