using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorelvl : MonoBehaviour
{
    private int score;
    Text mytext;

    // Start is called before the first frame update
    void Start()
    {
        score = StaticClass.score;
        mytext = GameObject.Find("title (2)").GetComponent<Text>();

        mytext.text = score.ToString();
    }

}
