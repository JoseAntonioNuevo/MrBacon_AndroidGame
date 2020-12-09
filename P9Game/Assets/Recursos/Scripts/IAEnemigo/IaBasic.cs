using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaBasic : MonoBehaviour
{
    Camera mainCamera;
    public float posicionX = 0f;
    public float vida = 200;

    public bool recibeDaño;
    public float timeRemaninRecibeDaño;
    public GameObject PrefabExplosion;

    Vector3 posicionVentana;
    Rigidbody2D rg;

void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
        rg = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        posicionVentana = mainCamera.WorldToViewportPoint(transform.position);


        if (posicionVentana.y < 0.060f)
            rg.AddForce(new Vector2(0, 20));

        if (posicionVentana.y > 0.70f)
            rg.AddForce(new Vector2(0, -20));

        if (recibeDaño)
        {
            timeRemaninRecibeDaño -= Time.deltaTime;
            if (timeRemaninRecibeDaño < 0)
                recibeDaño = false;
        }
           
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
