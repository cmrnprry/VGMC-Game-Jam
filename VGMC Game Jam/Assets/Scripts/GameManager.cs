using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Class getter and static instance
    private static GameManager managerInstance;
    public static GameManager Instance { get { return managerInstance; } }

    //Approval rating
    [SerializeField] private int totalCultist, totalMoms, totalTheorists, totalStans, totalFollowers;

    public enum FollowerType { CULTISTS = 0, MOMS = 1, THEROISTS = 2, STANS = 3 };

    // Start is called before the first frame update
    void Start()
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
}
