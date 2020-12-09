using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float velocidad = 14f;
    public float posicionX =0f;
    public int vida;



    public float timeOfRayo;


    public float timeMaxOfRayo;
    public float limiteInicioRayo;
    public float timeRecargaRayoPorSegundo;
    public float timeRemainingRayo;


    public float timeMaxOfEscudo;   //El tiempo maximo de uso del escudo.
    public float limiteInicioEscudo;//El limite de inicio que permitirá activar el escudo.
    public float timeRecargaEscudoPorSegundo;
    public bool escudoActivado;


    public float timeAnimedaño;

    public float limitRayo;
    public float limitEscudo;
    public bool escudoActivo;
    public bool disparoActivo;

    private bool BtnEscudoActivo;
    private bool BtnDisparoActivo;


    public AudioClip sonidoDaño;
    public AudioClip sonidoMotivaccion;
    public AudioClip sonidoActiveScudo;

    public bool recibiendodaño;

    //public GameObject laserPrefab;

    public float timeRemainingEscudo;
    float timeRemaining;
    float timeRemainingDaño;


    float inputaxyY; //entrada del jugador. 
    Camera mainCamera;
    Vector3 posicionVentana;
    Rigidbody2D rigi;
    bool ActiveRotary;
    AudioSource sound;
    Animator animacion;

    public GameObject PrefabDisparo;
    public float timeEntreDisparoYDisparo;
    float timeRemaninUltimoDisparo;


    void Start()
    {
        timeRemainingEscudo = timeMaxOfEscudo;
        timeRemainingRayo = timeMaxOfRayo;

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
        if(vida != 0)
            Movimiento();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Meteorito"){
            if (collision.gameObject.GetComponent<movimiento>().elementoActivado)
            {
                collision.gameObject.GetComponent<movimiento>().elementoActivado = false;
                Recibirdano(collision.gameObject.GetComponent<movimiento>().daño);
                collision.gameObject.GetComponent<Animator>().SetBool("destroy", true);

                collision.gameObject.GetComponent<AudioSource>().Play();
                //Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.tag == "powerup")
        {
                timeRemainingEscudo = timeMaxOfEscudo;
                Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Corazon")
        {
            if(vida <6)
            vida += 1;
            Destroy(collision.gameObject);
        }


    }

    public void MoveUp() {
        transform.Translate(0, 0.5f* Time.deltaTime * velocidad, 0);
    }
    public void MoveDown()
    {
        transform.Translate(0, -0.5f* Time.deltaTime * velocidad, 0);
    }

    public void ActivarEscudo(bool active)
    {
        BtnEscudoActivo = active;
    }

    public void ActivarDisparo(bool active) {
        BtnDisparoActivo = active;
    }

    private void Movimiento()
    {
        inputaxyY = Input.GetAxis("Vertical");


        //si el boton up esta activo inputxy+1


        if(inputaxyY != 0)
            transform.Translate(0, inputaxyY * Time.deltaTime * velocidad, 0);

        if (Mathf.Abs(rigi.rotation) > 0 && !ActiveRotary)
        {
            ActiveRotary = true;
            timeRemaining = 0.8f;
        }



        DisparoEscudos();

        TimerRotary();//Función que ejecuta la logica de temporizadores.

        if (transform.rotation.z != 0)
            transform.Rotate(new Vector3(0, 0, transform.rotation.z *-1) * Time.deltaTime * 500f);

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
        if (escudoActivado)
            return;

        animacion.SetBool("recibeDaño", true);
        recibiendodaño = true;
        timeRemainingDaño = timeAnimedaño;



        if (vida > 0)
        {
            vida -= danio;
        }else{
            rigi.simulated = false;
        }

        animacion.SetInteger("vida", vida);
        sound.clip = sonidoDaño;
        sound.Play();

        if(vida == 1)
        {
            sound.clip = sonidoMotivaccion;
            sound.Play();
        }
        //Activar secuencia de recibir daño.
        recibiendodaño = true;


        //Pasar a escena gamer over al perder todas las vidas
        if (vida < 1)
        {
            
        }

    }

    public void DisparoEscudos() {

        if (timeRemainingEscudo > limiteInicioEscudo) {

            
            escudoActivado = Input.GetKey(KeyCode.Space) || BtnEscudoActivo  ;

        } else {

            if (escudoActivado &&( timeRemainingEscudo <= limiteInicioEscudo || !(Input.GetKey(KeyCode.Space) || BtnEscudoActivo)))//Si el escudo se encuentra activado y no tiene tiempo de uso disponible
            {
                escudoActivado = false;
                timeRemainingEscudo = 0;
            }
        }


        if (escudoActivado != animacion.GetBool("escudoActivado"))
        {
            animacion.SetBool("escudoActivado", escudoActivado);
            sound.clip = sonidoActiveScudo;
            sound.Play();
        }
            

        //Logica de calculo de tiempo,
        if (escudoActivado)
        {//Resta tiempo
            timeRemainingEscudo -= Time.deltaTime;
        } else{
            //Si no se encuentra a tope, le sumamos
            if (timeRemainingEscudo < timeMaxOfEscudo)
                timeRemainingEscudo += timeRecargaEscudoPorSegundo * Time.deltaTime;
                
        }


        if (timeRemainingRayo > limiteInicioRayo)
        {
            disparoActivo = Input.GetKey(KeyCode.LeftShift) || BtnDisparoActivo;

        }
        else
        {

            if (disparoActivo && (timeRemainingRayo <= limiteInicioRayo || !(Input.GetKey(KeyCode.LeftShift) || BtnDisparoActivo )))//Si el escudo se encuentra activado y no tiene tiempo de uso disponible
            {
                disparoActivo = false;
                timeRemainingRayo = 0;
            }
        }

        if (disparoActivo != animacion.GetBool("rayoActivo"))
            animacion.SetBool("rayoActivo", disparoActivo);


        //Logica de calculo de tiempo,
        if (disparoActivo)
        {//Resta tiempo
            timeRemainingRayo -= Time.deltaTime;

            timeRemaninUltimoDisparo -= Time.deltaTime;
            if (timeRemaninUltimoDisparo < 0)
            {
                timeRemaninUltimoDisparo = timeEntreDisparoYDisparo;
               GameObject laser= Instantiate(PrefabDisparo);

                laser.transform.position = this.transform.position + new Vector3(2, 0);

            }

        }
        else
        {
            //Si no se encuentra a tope, le sumamos
            if (timeRemainingRayo < timeMaxOfRayo)
                timeRemainingRayo += timeRecargaRayoPorSegundo * Time.deltaTime;

        }

    }



}
