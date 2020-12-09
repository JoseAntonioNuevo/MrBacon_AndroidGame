using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaBasic : MonoBehaviour
{
    Camera mainCamera;
    public float posicionX = 0f;
    public float vida;

    public bool recibeDaño;
    public float timeRemaninRecibeDaño;
    public GameObject PrefabExplosion;

    public Vector3 posicionVentana;
    Rigidbody2D rg;



    public GameObject disparo;
    Vector2 dispos;
    public float firerate = 0.2f;
    public float nextfire = 0.0f;
    public float timeRemanin;

    public float tiempoDisparoEntre;
    public float TiempoDisparoHasta;


    void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
        rg = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        posicionVentana = mainCamera.WorldToViewportPoint(transform.position);


        if (posicionVentana.y < 0.1f)
            rg.AddForce(new Vector2(0, 30));

        if (posicionVentana.y > 0.70f)
            rg.AddForce(new Vector2(0, -20));

        if (recibeDaño)
        {
            timeRemaninRecibeDaño -= Time.deltaTime;
            if (timeRemaninRecibeDaño < 0)
                recibeDaño = false;
        }


        //


        if (timeRemanin < 0)
        {
            timeRemanin = Random.Range(tiempoDisparoEntre, TiempoDisparoHasta);
            fire();
        }
        else {
            timeRemanin -= Time.deltaTime;
        }


    }

    void fire()
    {
        dispos = transform.position;
        dispos += new Vector2(-2f, 0f);
        Instantiate(disparo, dispos, Quaternion.identity);
    }

    void Fire(int idx, int wpn)
    {
        //ProjectileData projectileInstance = Instantiate(projectiles[idx], weapon[wpn].transform.position, Quaternion.identity);

        //projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectiles[idx].Speed, 0);

    }

    void FireController()
    {


        //time += Time.deltaTime;

        //if (time > fireDelay)
        //{
        //    random = Random.Range(1, 10);
        //    Debug.Log("entra en +2 y el random es " + random);
        //    for (int i = 0; i < projectiles.Length; i++)
        //    {
        //        if (random >= projectiles[i].MinProbability && random < projectiles[i].MaxProbability)
        //        {
        //            Debug.Log("disparo " + i + " porque el random es " + random);
        //            Fire(i, i);

        //        }
        //    }

        //    time = 0;
        //    fireDelay = Random.Range(minDelay, maxDelay);
        //}




    }

    public void QuitarVida(float daño) {
        vida -= daño;
        recibeDaño = true;
        timeRemaninRecibeDaño = 1;
        //Animación de daño..
        GameObject var = Instantiate(PrefabExplosion);
        var.transform.position = this.transform.position + new Vector3(0,0,-1);

        //Sonido de daño...
    }
}
