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

    //checks tweets and repopulates
    public bool isSecond, nextDay;
    public int winningFollowers;

    //Players Profile, user and display name
    public string profileName, userName, displayName, userChirp;
    private Sprite userProfile;

    //Data from the TSV
    private List<ChirperOptionsStruct> chirperOptionsData = new List<ChirperOptionsStruct>();
    private List<ChirpStruct> chirperChirpData = new List<ChirpStruct>();
    private List<ChirperTrendStruct> chirperTrendsData = new List<ChirperTrendStruct>();
    private List<ChirperFollowerStruct> chirperFollowerData = new List<ChirperFollowerStruct>();
    private List<RepliesStruct> chirperRepliesData = new List<RepliesStruct>();

    //Button Controller Reference
    [SerializeField] private ButtonController bc;

    //Types of followers
    public enum FollowerType { CULTISTS = 0, MOMS = 1, THEROISTS = 2, STANS = 3 };

    //List of profile Pictures
    [Header("Profile Photos")]
    [SerializeField] private List<Sprite> profiles = new List<Sprite>();
    [SerializeField] private Image p1, p2;


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

    [Header("Chirper Who to Follow")]
    [SerializeField] private TextMeshProUGUI[] FollowNameText;
    [SerializeField] private TextMeshProUGUI[] FollowUserText;
    [SerializeField] private Image[] FollowImage;

    [Header("Replies")]
    [SerializeField] private TextMeshProUGUI[] ReplyTitleText;
    [SerializeField] private TextMeshProUGUI[] ReplyChirpText;
    [SerializeField] private TextMeshProUGUI[] ReplyNumberText;
    [SerializeField] private TextMeshProUGUI[] ReplyNameText;
    [SerializeField] private TextMeshProUGUI[] ReplyUserText;
    [SerializeField] private TextMeshProUGUI[] ReplyContentText;
    [SerializeField] private Image[] ReplyImage;


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

    //EndGame
    private void endGame()
    {
        bc.ShowEndScreen();

        if (totalFollowers > winningFollowers)
        {
            if (totalCultist > Mathf.Max(totalMoms, Mathf.Max(totalStans, totalTheorists)) )
            {
                bc.CultistWin();
            }
            else if (totalMoms > Mathf.Max(totalCultist, Mathf.Max(totalStans, totalTheorists)))
            {
                bc.MomsWin();
            }
            else if (totalTheorists > Mathf.Max(totalMoms, Mathf.Max(totalStans, totalCultist)))
            {
                bc.TheoristWin();
            }
            else if (totalStans > Mathf.Max(totalMoms, Mathf.Max(totalCultist, totalTheorists)))
            {
                bc.StanWin();
            }
        }
        else
        {
            bc.LoseGame();
        }
    }

    //Setters from their respective Reader
    public void setChirperOptionsStruct(List<ChirperOptionsStruct> data)
    {
        chirperOptionsData = data;
    }

    public void setChirperChirpData(List<ChirpStruct> data)
    {
        chirperChirpData = data;
        populateChirperChirps();
    }

    public void setChirperTrendsData(List<ChirperTrendStruct> data)
    {
        chirperTrendsData = data;
        populateTrendsChirps();
    }
    public void setChirperFollowersData(List<ChirperFollowerStruct> data)
    {
        chirperFollowerData = data;
        populateFollowersChirps();
    }    
    
    public void setChirperRepliesData(List<RepliesStruct> data)
    {
        chirperRepliesData = data;
        populateRepliesChirps();
    }

    public void setChirperSecondTweet()
    {
        isSecond = true;
        rePopulate();
    }

    public void setNextDay()
    {
        isSecond = false;
        bc.ShowStats();

        if(day >= 14)
        {
            endGame();
        }

        day++;
        rePopulate();
    }

    public void SetButton(GameManager.FollowerType t, int d, string chirp)
    {
        userChirp = chirp;
        bc.OpenReplies();
        addFollower(t, d);
    }

    //Populaters
    //This is gonna be some bad code, I'm sorry
    public void populateChirperOptions()
    {
        ChirperOptionsStruct data = chirperOptionsData[day - 1];
        
        if (isSecond)
        {
            data = chirperOptionsData[day];
        }

       

        Option1Text.text = data.option_1;
        Option2Text.text = data.option_2;
        Option3Text.text = data.option_3;
        Option4Text.text = data.option_4;

        //Set the Buttons
        Option1.onClick.AddListener(delegate { SetButton(data.type_1, data.delta_1, data.option_1); });
        Option2.onClick.AddListener(delegate { SetButton(data.type_2, data.delta_2, data.option_2); });
        Option3.onClick.AddListener(delegate { SetButton(data.type_3, data.delta_3, data.option_3); });
        Option4.onClick.AddListener(delegate { SetButton(data.type_4, data.delta_4, data.option_4); });
    }

    public void populateChirperChirps()
    {
        ChirpStruct data_one = chirperChirpData[0], data_two = chirperChirpData[0];
        Sprite image = null, image2 = null;

        for (int i = 0; i < chirperChirpData.Count; i++)
        {
            if (isSecond)
            {
                data_one = chirperChirpData[i + 2];
                data_two = chirperChirpData[i + 3];

                break;
            }

            if (chirperChirpData[i].day == day)
            {
                data_one = chirperChirpData[i];
                data_two = chirperChirpData[i + 1];

                break;
            }
        }


        foreach (Sprite img in profiles)
        {
            if (data_one.profile_pic == img.name)
            {
                image = img;
            }

            if (data_two.profile_pic == img.name)
            {
                image2 = img;
            }
        }


        ChirpImage[0].sprite = image;
        ChirpNameText[0].text = data_one.chirper_name;
        ChirpUserText[0].text = data_one.user_name;
        ChirpContentText[0].text = data_one.chirp_content;

        ChirpImage[1].sprite = image2;
        ChirpNameText[1].text = data_two.chirper_name;
        ChirpUserText[1].text = data_two.user_name;
        ChirpContentText[1].text = data_two.chirp_content;

    }

    public void populateTrendsChirps()
    {
        ChirperTrendStruct data_one = chirperTrendsData[0], data_two = chirperTrendsData[0], data_three = chirperTrendsData[0], data_four = chirperTrendsData[0], data_five = chirperTrendsData[0];

        for (int i = 0; i < chirperTrendsData.Count; i++)
        {
            if (chirperTrendsData[i].day == day)
            {
                data_one = chirperTrendsData[i + 0];
                data_two = chirperTrendsData[i + 1];
                data_three = chirperTrendsData[i + 2];
                data_four = chirperTrendsData[i + 3];
                data_five = chirperTrendsData[i + 4];
                break;
            }
        }

        //Normal Trends
        ChirpChirpText[0].text = data_one.trending_chirp;
        ChirpNumberText[0].text = data_one.trending_number;
        ChirpTitleText[0].text = data_one.trending_title;

        ChirpChirpText[1].text = data_two.trending_chirp;
        ChirpNumberText[1].text = data_two.trending_number;
        ChirpTitleText[1].text = data_two.trending_title;

        ChirpChirpText[2].text = data_three.trending_chirp;
        ChirpNumberText[2].text = data_three.trending_number;
        ChirpTitleText[2].text = data_three.trending_title;

        ChirpChirpText[3].text = data_four.trending_chirp;
        ChirpNumberText[3].text = data_four.trending_number;
        ChirpTitleText[3].text = data_four.trending_title;

        ChirpChirpText[4].text = data_five.trending_chirp;
        ChirpNumberText[4].text = data_five.trending_number;
        ChirpTitleText[4].text = data_five.trending_title;

        //Replies Trends
        ReplyChirpText[0].text = data_one.trending_chirp;
        ReplyNumberText[0].text = data_one.trending_number;
        ReplyTitleText[0].text = data_one.trending_title;

        ReplyChirpText[1].text = data_two.trending_chirp;
        ReplyNumberText[1].text = data_two.trending_number;
        ReplyTitleText[1].text = data_two.trending_title;

        ReplyChirpText[2].text = data_three.trending_chirp;
        ReplyNumberText[2].text = data_three.trending_number;
        ReplyTitleText[2].text = data_three.trending_title;

        ReplyChirpText[3].text = data_four.trending_chirp;
        ReplyNumberText[3].text = data_four.trending_number;
        ReplyTitleText[3].text = data_four.trending_title;

        ReplyChirpText[4].text = data_five.trending_chirp;
        ReplyNumberText[4].text = data_five.trending_number;
        ReplyTitleText[4].text = data_five.trending_title;

}

    public void populateFollowersChirps()
    {
        ChirperFollowerStruct data_one = chirperFollowerData[0], data_two = chirperFollowerData[0];
        Sprite image = null, image2 = null;

        for (int i = 0; i < chirperFollowerData.Count; i++)
        {

            if (isSecond)
            {
                data_one = chirperFollowerData[i + 2];
                data_two = chirperFollowerData[i + 3];
                break;
            }

            if (chirperFollowerData[i].day == day)
            {
                data_one = chirperFollowerData[i];
                data_two = chirperFollowerData[i + 1];
                break;
            }
        }

        foreach (Sprite img in profiles)
        {
            if (data_one.profile_pic == img.name)
            {
                image = img;
            }

            if (data_two.profile_pic == img.name)
            {
                image2 = img;
            }
        }

        FollowImage[0].sprite = image;
        FollowNameText[0].text = data_one.chirper_name;
        ChirpUserText[0].text = data_one.user_name;

        FollowImage[1].sprite = image2;
        FollowNameText[1].text = data_two.chirper_name;
        FollowUserText[1].text = data_two.user_name;
    }

    public void populateRepliesChirps()
    {
        RepliesStruct data_one = chirperRepliesData[0], data_two = chirperRepliesData[0], data_three = chirperRepliesData[0];
        Sprite image = null, image2 = null, image3 = null;
        ;

        for (int i = 0; i < chirperFollowerData.Count; i++)
        {
            if (isSecond)
            {
                data_one = chirperRepliesData[i + 3];
                data_two = chirperRepliesData[i + 4];
                data_three = chirperRepliesData[i + 5];
                break;
            }

            if (chirperFollowerData[i].day == day)
            {
                data_one = chirperRepliesData[i + 0];
                data_two = chirperRepliesData[i + 1];
                data_three = chirperRepliesData[i + 2];
                break;
            }
        }

        foreach (Sprite img in profiles)
        {
            if (data_one.profile_pic == img.name)
            {
                image = img;
            }

            if (data_two.profile_pic == img.name)
            {
                image2 = img;
            }

            if (data_three.profile_pic == img.name)
            {
                image3 = img;
            }

            if (profileName == img.name)
            {
                userProfile = img;
            }
        }

        p1.sprite = userProfile;
        p2.sprite = userProfile;
        ReplyImage[0].sprite = userProfile;
        ReplyNameText[0].text = displayName;
        ReplyUserText[0].text = userName;
        ReplyContentText[0].text = userChirp;

        ReplyImage[1].sprite = image;
        ReplyNameText[1].text = data_two.chirper_name;
        ReplyUserText[1].text = data_two.user_name;
        ReplyContentText[1].text = data_two.chirp_content;

        ReplyImage[2].sprite = image2;
        ReplyNameText[2].text = data_two.chirper_name;
        ReplyUserText[2].text = data_two.user_name;
        ReplyContentText[2].text = data_two.chirp_content;

        ReplyImage[3].sprite = image3;
        ReplyNameText[3].text = data_three.chirper_name;
        ReplyUserText[3].text = data_three.user_name;
        ReplyContentText[3].text = data_three.chirp_content;
    }

    public void rePopulate()
    {
        populateChirperChirps();
        populateTrendsChirps();
        populateFollowersChirps();
        populateRepliesChirps();
    }

    //Getters
    public int getTotalFollowers()
    {
        return totalFollowers;
    }
    
    public int getTotalCult()
    {
        return totalCultist;
    }

    public int getTotalMoms()
    {
        return totalMoms;
    }

    public int getTotalTheory()
    {
        return totalTheorists;
    }

    public int getTotalStans()
    {
        return totalStans;
    }
}
