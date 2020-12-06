using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float velocidad = 14f;
    public float posicionX =0f;
    public int vida;


    public float timeOfRayo;
    public float timeOfEscudo;
    public float timeAnimedaño;


    public float limitRayo;
    public float limitEscudo;

    public bool escudoActivo;
    public bool disparoActivo;



    public AudioClip sonidoDaño;
    public AudioClip sonidoMotivaccion;
    public bool recibiendodaño;


    float timeRemaining;
    float timeRemainingDaño;
    float inputaxyY; //entrada del jugador. 
    Camera mainCamera;
    Vector3 posicionVentana;
    private Rigidbody2D rigi;
    private bool ActiveRotary;
    AudioSource sound;
    private Animator animacion;

    void Start()
    {
        escudoActivo = false;
        disparoActivo = false;
        mainCamera = Camera.main.GetComponent<Camera>();
        rigi = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        animacion = GetComponent<Animator>();
        animacion.SetInteger("vida", vida);
    }
    // Update is called once per frame
    void Update()
    {
        Movimiento();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Meteorito"){
            if (collision.gameObject.GetComponent<movimiento>().elementoActivado)
            {
            collision.gameObject.GetComponent<movimiento>().elementoActivado = false;
                Recibirdano(collision.gameObject.GetComponent<movimiento>().daño);
                //Destroy(collision.gameObject);
            }
        }



    }
    private void Movimiento()
    {
        inputaxyY = Input.GetAxis("Vertical");

        if(inputaxyY != 0)
            transform.Translate(0, inputaxyY * Time.deltaTime * velocidad, 0);

        if (Mathf.Abs(rigi.rotation) > 0 && !ActiveRotary)
        {
            ActiveRotary = true;
            timeRemaining = 0.8f;
        }
        

        TimerRotary();//Función que ejecuta la logica de temporizadores.

        if (transform.rotation.z != 0)
            transform.Rotate(new Vector3(0, 0, transform.rotation.z *-1) * Time.deltaTime * 500f);
   
/*
        if(animacion.GetBool("recibeDaño"))
            animacion.SetBool("recibeDaño", false);
*/

        posicionVentana = mainCamera.WorldToViewportPoint(transform.position);
        posicionVentana.x = posicionX;
        posicionVentana.y = Mathf.Clamp(posicionVentana.y,0.075f,0.85f);
        transform.position = mainCamera.ViewportToWorldPoint(posicionVentana);
    }
    private void TimerRotary() {

        if(ActiveRotary)
            if (timeRemaining > 0)
            {
               timeRemaining -= Time.deltaTime;
            }else{
                //Resetear rigibody.
                rigi.constraints = RigidbodyConstraints2D.FreezeRotation;
                Vector3 eulerRotation = transform.rotation.eulerAngles;
                rigi.velocity = Vector2.zero;
                ActiveRotary = false;
                rigi.constraints = RigidbodyConstraints2D.None;
            }


        if(recibiendodaño)
            if (timeRemainingDaño > 0)
            {
                timeRemainingDaño -= Time.deltaTime;
            }
            else
            {
                animacion.SetBool("recibeDaño", false);
                recibiendodaño = false;
            }
    }



    private void Recibirdano(int danio)
    {
        animacion.SetBool("recibeDaño", true);
        recibiendodaño = true;
        timeRemainingDaño = timeAnimedaño;



        if (vida > 0)
        {
            vida -= danio;
            animacion.SetInteger("vida", vida);
        }

        sound.clip = sonidoDaño;
        sound.Play();

        if(vida == 1)
        {
            sound.clip = sonidoMotivaccion;
            sound.Play();
        }
        //Activar secuencia de recibir daño.
        recibiendodaño = true;

        
    }


}
