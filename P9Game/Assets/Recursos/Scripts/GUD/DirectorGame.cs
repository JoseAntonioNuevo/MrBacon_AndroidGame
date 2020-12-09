using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DirectorGame : MonoBehaviour
{
    public GameObject jugador;
    public GameObject[] vidas;

    public GameObject SliderEscudo;
    private Slider escudoView;
    public GameObject SliderLaser;
    private Slider laserView;

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
        laserView = SliderLaser.GetComponent<Slider>();

        escudoView.minValue = scriptjugador.limiteInicioEscudo;
        escudoView.maxValue = scriptjugador.timeMaxOfEscudo;

        laserView.minValue = scriptjugador.limiteInicioRayo;
        laserView.maxValue = scriptjugador.timeMaxOfRayo;

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
        laserView.value = scriptjugador.timeRemainingRayo;
    }


    public void nextLevel()
    {

        Scene scenes = SceneManager.GetActiveScene();
        string scene = scenes.name;

        int Level = 0;
        switch (scene)
        {
            case "Nivel 0": Level = 0;break;
            case "Nivel 1": Level = 1; break;
            case "Nivel 2": Level = 2; break;
            case "Nivel 3": Level = 3; break;
        }

        StaticClass.actualLevel = Level;

        SceneManager.LoadScene("LevelPassed");
    }


    public void MoveUpON() { MouseUpActive = true; }

    public void MoveUpOFF() { MouseUpActive = false; }

    public void MoveDownON() { MouseDownActive = true; }
    public void MoveDownOFF() { MouseDownActive = false; }

    public void MoveDown() { scriptjugador.MoveDown(); }
    public void ActivarDisparoON() { scriptjugador.ActivarDisparo(true); }

    public void ActivarDisparoOFF() { scriptjugador.ActivarDisparo(false); }

    public void ActivarEscudoON() { scriptjugador.ActivarEscudo(true); }

    public void ActivarEscudoOFF() { scriptjugador.ActivarEscudo(false); }




}

