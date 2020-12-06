using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorGame : MonoBehaviour
{
    public GameObject jugador;
    private Player scriptjugador;
    private int vidaMostrada;
    

    void Start()
    {
        scriptjugador = jugador.GetComponent<Player>();
        vidaMostrada = scriptjugador.vida;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void MostrarVida(int vida) {




    }
}
