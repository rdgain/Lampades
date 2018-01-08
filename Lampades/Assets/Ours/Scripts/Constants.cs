using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour{

    public static string AVATAR_NAME = "avatar";
    public static string SHADOW_NAME = "shadow";

    public static string STUFF_SIZE_TEXTBOX = "StuffSize";

    public static string PLAYER_TAG = "Player";
    public static string COLLECT_TAG = "Collectibles";
    public static string NETHER_TAG = "Nether";
    public static string REAL_TAG = "Real";


    public static string[] stuffsList = { "Stick", "Box" };
    public GameObject[] realModel = new GameObject[stuffsList.Length];
    public GameObject[] netherModel = new GameObject[stuffsList.Length];
    static Dictionary<string, GameObject> mapper;

    public static string STICK_LAYER = "Sticking";

    public static int platform_num = 2;

    void Start()
    {
        mapper = new Dictionary<string, GameObject>();

        for(int i=0;i<stuffsList.Length;i++)
        {
            mapper.Add(AVATAR_NAME+"_"+stuffsList[i], realModel[i]);
            mapper.Add(SHADOW_NAME + "_" + stuffsList[i], netherModel[i]);
        }
    }

    public static GameObject getModel(string avatar, string key)
    {
        return mapper[avatar+"_"+key];
    }


}
