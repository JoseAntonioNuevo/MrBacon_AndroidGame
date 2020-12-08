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
    public float timeRemainingRayo;
    public float timeMaxOfRayo;
    public float timeRecargaRayoPorSegundo;


    public float timeMaxOfEscudo;   //El tiempo maximo de uso del escudo.
    public float limiteInicioEscudo;//El limite de inicio que permitirá activar el escudo.
    public float timeRecargaEscudoPorSegundo;
    public bool escudoActivado;


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
    public float timeRemainingEscudo;
    float inputaxyY; //entrada del jugador. 
    Camera mainCamera;
    Vector3 posicionVentana;
    private Rigidbody2D rigi;
    private bool ActiveRotary;
    AudioSource sound;
    private Animator animacion;

    void Start()
    {
        timeRemainingEscudo = timeMaxOfEscudo;
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
                //Destroy(collision.gameObject);
            }
        }



    }

    public void MoveUp() {
      
        transform.Translate(0, 0.5f* Time.deltaTime * velocidad, 0);
    }
    public void MoveDown()
    {
        transform.Translate(0, -0.5f* Time.deltaTime * velocidad, 0);
    }

    public void ActivarEscudo()
    {
        if (timeRemainingEscudo > limiteInicioEscudo)
        {
            escudoActivado = true;

        }
        else
        {

            if (escudoActivado && (timeRemainingEscudo <= 0))//Si el escudo se encuentra activado y no tiene tiempo de uso disponible
            {
                escudoActivado = false;
            }
        }

        if (escudoActivado != animacion.GetBool("escudoActivado"))
            animacion.SetBool("escudoActivado", escudoActivado);

        //Logica de calculo de tiempo,
        if (escudoActivado)
        {//Resta tiempo
            timeRemainingEscudo -= Time.deltaTime;
        }
        else
        {
            //Si no se encuentra a tope, le sumamos
            if (timeRemainingEscudo < timeMaxOfEscudo)
                timeRemainingEscudo += timeRecargaEscudoPorSegundo * Time.deltaTime;

        }

    }

    public void ActivarDisparo() {
        if (timeOfRayo > limitRayo)
        {
            disparoActivo = true;

        }
        else
        {

            if (disparoActivo && (limitRayo <= 0))//Si el escudo se encuentra activado y no tiene tiempo de uso disponible
            {
                disparoActivo = false;
            }
        }

        if (disparoActivo != animacion.GetBool("DisparoActivado"))
            animacion.SetBool("DisparoActivado", disparoActivo);

        //Logica de calculo de tiempo,
        if (disparoActivo)
        {//Resta tiempo
            timeRemainingRayo -= Time.deltaTime;
        }
        else
        {
            //Si no se encuentra a tope, le sumamos
            if (timeRemainingRayo < timeMaxOfRayo)
                timeRemainingRayo += timeRecargaRayoPorSegundo * Time.deltaTime;

        }
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
            
            Scene scenes = SceneManager.GetActiveScene();
            string scene = scenes.name;

            switch (scene)
            {
                case "Nivel 0":
                    SceneManager.LoadScene("GameOver");
                    break;
                case "Nivel 1":
                    SceneManager.LoadScene("GameOver2");
                    break;
                case "Nivel 2":
                    SceneManager.LoadScene("GameOver3");
                    break;
                case "Nivel 3":
                    SceneManager.LoadScene("GameOver4");
                    break;
                default:
                    SceneManager.LoadScene("Menu");
                    break;
            }

            
        }

    }




    public void DisparoEscudos() {

        if (timeRemainingEscudo > limiteInicioEscudo) {
            escudoActivado = Input.GetKey(KeyCode.Space);

        } else {

            if (escudoActivado &&( timeRemainingEscudo <= 0  || !Input.GetKey(KeyCode.Space)))//Si el escudo se encuentra activado y no tiene tiempo de uso disponible
            {
                escudoActivado = false;
            }
        }

        if (escudoActivado != animacion.GetBool("escudoActivado"))
            animacion.SetBool("escudoActivado", escudoActivado);

        //Logica de calculo de tiempo,
        if (escudoActivado)
        {//Resta tiempo
            timeRemainingEscudo -= Time.deltaTime;
        } else{
            //Si no se encuentra a tope, le sumamos
            if (timeRemainingEscudo < timeMaxOfEscudo)
                timeRemainingEscudo += timeRecargaEscudoPorSegundo * Time.deltaTime;
                
        }

        // if(es)


    }
}
