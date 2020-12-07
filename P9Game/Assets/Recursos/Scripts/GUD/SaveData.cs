using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public string name;
    public int score;
    public int level;

    public SaveData(DirectorGame player)
    {
        name = player.nombreplayer;
        score = player.score;
        level = player.level;
    }
}
