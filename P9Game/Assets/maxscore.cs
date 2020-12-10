using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maxscore : MonoBehaviour
{
    private int score;
    Text mytext;

    void Start()
    {
        score = StaticClass.Totalscore;
        mytext = GameObject.Find("title (9)").GetComponent<Text>();

        mytext.text = score.ToString();
    }
}
