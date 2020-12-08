using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DirectorGame : MonoBehaviour
{
    public GameObject jugador;
    public GameObject[] vidas;

    public GameObject SliderEscudo;
    private Slider escudoView;
   // public GameObject SliderLaser;

    private Player scriptjugador;
    private int vidaMostrada;

    public string nombreplayer = "MrBeacon";
    public int maxscore;
    public int level;
    public int score;
    private DateTime timestartlevel;


    private bool MouseUpActive;
    private bool MouseDownActive;
    private bool ButtonShieldActive;
    private bool ButtonShootActive;

    void Start()
    {
        scriptjugador = jugador.GetComponent<Player>();
        vidaMostrada = scriptjugador.vida;
        MostrarVida(vidaMostrada);
        escudoView = SliderEscudo.GetComponent<Slider>();


        escudoView.minValue = 0;
        escudoView.maxValue = scriptjugador.timeMaxOfEscudo;

    }

    // Update is called once per frame
    void Update()
    {
        if(scriptjugador.vida != vidaMostrada)
        {
            MostrarVida(scriptjugador.vida);
            vidaMostrada = scriptjugador.vida;
        }
        RefescarBarras();

        if (MouseUpActive)
            scriptjugador.MoveUp();

        if (MouseDownActive)
            scriptjugador.MoveDown();

        if (ButtonShieldActive)
            scriptjugador.ActivarEscudo();

        if (ButtonShootActive)
            scriptjugador.ActivarDisparo();
    }


    void MostrarVida(int vida) {

        if(vida < vidaMostrada) 
            for(int i = vida ; i < vidaMostrada; i++)
            {
                vidas[i].SetActive(false);
            }
        else
            for (int i = vida; i < vidaMostrada; i++)
            {
                vidas[i].SetActive(false);
            }
    }


    void RefescarBarras() {
        escudoView.value = scriptjugador.timeRemainingEscudo;
    }


    public void MoveUpON() { MouseUpActive = true; }

    public void MoveUpOFF() { MouseUpActive = false; }

    public void MoveDownON() { MouseDownActive = true; }
    public void MoveDownOFF() { MouseDownActive = false; }

    public void MoveDown() { scriptjugador.MoveDown(); }
    public void ActivarDisparoON() { ButtonShootActive = true;  }

    public void ActivarDisparoOFF() { ButtonShootActive = false; }

    public void ActivarEscudoON() { ButtonShieldActive = true; }

    public void ActivarEscudoOFF() { ButtonShieldActive = false; }
}
