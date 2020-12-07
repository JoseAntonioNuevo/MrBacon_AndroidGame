using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorGame : MonoBehaviour
{
    public GameObject jugador;
    public GameObject[] vidas;

    public string nombreplayer = "MrBeacon";
    public int maxscore;
    public int level;
    public int score;
    private DateTime timestartlevel;

    private Player scriptjugador;
    private int vidaMostrada;
    

    void Start()
    {
        scriptjugador = jugador.GetComponent<Player>();
        vidaMostrada = scriptjugador.vida;
        MostrarVida(vidaMostrada);
    }

    // Update is called once per frame
    void Update()
    {
        if(scriptjugador.vida != vidaMostrada)
        {
            MostrarVida(scriptjugador.vida);
            vidaMostrada = scriptjugador.vida;
        }
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
}
