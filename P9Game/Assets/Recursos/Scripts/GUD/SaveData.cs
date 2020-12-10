using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public int score;
    public int level;

    public SaveData() { }

    public SaveData(int level, int score)
    {
        this.score = score;
        this.level = level;
    }
}
