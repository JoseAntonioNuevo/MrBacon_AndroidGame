using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;

public class ManagerScen : MonoBehaviour
{
    public DateTime startlvl;
    private Button MyButton = null; // assign in the editor

    public void Start()
    {
    
    }
    // Start is called before the first frame update
    public void SiguienteLevel()
    {
        //cargar siguiente nivel
        switch (StaticClass.actualLevel +1)
        {
            case 0:
                SceneManager.LoadScene("Nivel 0");
                break;
            case 1:
                SceneManager.LoadScene("Nivel 1");
                startlvl = DateTime.Now;
                break;
            case 2:
                SceneManager.LoadScene("Nivel 2");
                startlvl = DateTime.Now;
                break;
            case 3:
                SceneManager.LoadScene("Nivel 3");
                startlvl = DateTime.Now;
                break;
            case 4:
                SceneManager.LoadScene("Creditos");
                break;
        }
    }

    public void Reintentar()
    {

        switch (StaticClass.actualLevel )
        {
            case 0:
                SceneManager.LoadScene("Nivel 0");
                break;
            case 1:
                SceneManager.LoadScene("Nivel 1");
                startlvl = DateTime.Now;
                break;
            case 2:
                SceneManager.LoadScene("Nivel 2");
                startlvl = DateTime.Now;
                break;
            case 3:
                SceneManager.LoadScene("Nivel 3");
                startlvl = DateTime.Now;
                break;
        }
    }

    public void RestarContinue() {
        StaticClass.actualLevel = 1;
        SceneManager.LoadScene("Nivel 1");
    }

    public DateTime startlv()
    {
        return startlvl;
    }

    public void Salir()
    {
        Application.Quit();
    }
}
