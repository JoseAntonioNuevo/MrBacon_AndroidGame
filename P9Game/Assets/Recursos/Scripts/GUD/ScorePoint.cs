using System;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{

    private DateTime timefinishlevel;
    public int scorelvl = 1500;
    public int multiploresta = 13;
    public int scorelvlincrement = 300;
    public int scorereturn;

    public int Score(DateTime timestart, int nivel)
    {
        timefinishlevel = DateTime.Now;

        TimeSpan duracion = timefinishlevel - timestart;
        int segundos = duracion.Seconds;

        switch (nivel)
        {
            case 1:
                scorereturn = scorelvl + (segundos * multiploresta);
                
                break;

            case 2:
                scorereturn = scorelvl + scorelvlincrement - (segundos * multiploresta);
                break;

            case 3:
                scorereturn = scorelvl + (scorelvlincrement * 2) - (segundos * multiploresta);
                break;
        }

        return scorelvl;
    }

}
