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


    public GameObject Enemigo;

    public GameObject SliderEscudo;
    private Slider escudoView;
    public GameObject SliderLaser;
    private Slider laserView;

    private Player scriptjugador;
    private IaBasic scriptEnemigo;

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

    private bool ActiveFinGame;
    private float timeRemaninActive;
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        scriptjugador = jugador.GetComponent<Player>();

        scriptEnemigo = Enemigo.GetComponent<IaBasic>();

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
            if (scriptjugador.vida < 1)
            {
                ActiveFinGame = true;
                GetComponent<AudioSource>().Stop();
            }
                

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (scriptEnemigo.vida < 1)
        {
            nextLevel();
        }

            RefescarBarras();

        if (MouseUpActive)
            scriptjugador.MoveUp();

        if (MouseDownActive)
            scriptjugador.MoveDown();

        if (ActiveFinGame)
        {
            timeRemaninActive += Time.deltaTime;
            if (timeRemaninActive > 2)
                GameOver();
        }
           


    }


    void MostrarVida(int vida) {

        if(vida < vidaMostrada) 
            for(int i = vida ; i < vidaMostrada; i++)
            {
                vidas[i].SetActive(false);
            }
        else
            for (int i = vidaMostrada ; i < vida; i++)
            {
                vidas[i].SetActive(true);
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

    public void GameOver() {

        Scene scenes = SceneManager.GetActiveScene();
        string scene = scenes.name;

        int Level = 0;
        switch (scene)
        {
            case "Nivel 0": Level = 0; break;
            case "Nivel 1": Level = 1; break;
            case "Nivel 2": Level = 2; break;
            case "Nivel 3": Level = 3; break;
        }

        StaticClass.actualLevel = Level;


        SceneManager.LoadScene("GameOver");
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




    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

