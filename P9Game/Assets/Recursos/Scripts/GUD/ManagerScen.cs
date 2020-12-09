using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ManagerScen : MonoBehaviour
{
    private Button MyButton = null; // assign in the editor

    public void Start()
    {
    
    }
    // Start is called before the first frame update
    public void SiguienteLevel()
    {
          
        switch (StaticClass.actualLevel +1)
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
        }
    }




}
