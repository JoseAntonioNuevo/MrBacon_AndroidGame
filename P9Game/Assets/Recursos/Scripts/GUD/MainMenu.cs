using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EscenaJuego(string pNombreNivel)
    {

        SaveLoad.LoadPlayer();

        //cargar siguiente nivel
        switch (StaticClass.actualLevel )
        {
            case 0:
                SceneManager.LoadScene("Nivel 0");
                break;
            case 1:
                SceneManager.LoadScene("Nivel 1");
                break;
            case 2:
                SceneManager.LoadScene("Nivel 2");
                break;
            case 3:
                SceneManager.LoadScene("Nivel 3");
                break;
            case 4:
                SceneManager.LoadScene("Creditos");
                break;
        }
    }



    public void Salir()
    {
        Application.Quit();
    }

}
