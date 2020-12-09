using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    
    public float velocidad = 8;
    float randY;
    Vector2 whereToSpawn;
    Vector2 spawnestrella;

    public GameObject meteor;
    public float spawnRatemeteor = 2f;
    float nextSpawnmeteor = 0.0f;

    public GameObject TrufaElectrica;
    public float spawnRatestrella = 2f;
    public float nextSpawnestrella = 1f;

    public GameObject corazon;
    public float spawnRatecorazon = 2f;
    public float nextSpawncorazon = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
        if (nextSpawnmeteor < 0)
        {
            nextSpawnmeteor = spawnRatemeteor;
            randY = Random.Range(-4.5f,4.5f);
            whereToSpawn = new Vector2(transform.position.x, randY);
            meteor.GetComponent<movimiento>().speed = velocidad;
            Instantiate(meteor, whereToSpawn, Quaternion.identity);
            
        }else
        {
            nextSpawnmeteor -= Time.deltaTime;
        }

        if (nextSpawnestrella < 0)
        {
            nextSpawnestrella = spawnRatestrella;
            randY = Random.Range(-4.5f, 4.5f);
            spawnestrella = new Vector2(transform.position.x, randY);
            TrufaElectrica.GetComponent<movpowerup>().speed = velocidad;
            Instantiate(TrufaElectrica, spawnestrella, Quaternion.identity);
        }else
        {
            nextSpawnestrella -= Time.deltaTime;
        }

        if (nextSpawncorazon < 0)
        {
            nextSpawncorazon = spawnRatecorazon;
            randY = Random.Range(-4.5f, 4.5f);
            spawnestrella = new Vector2(transform.position.x, randY);
            corazon.GetComponent<movpowerup>().speed = velocidad;
            Instantiate(corazon, spawnestrella, Quaternion.identity);
        }
        else
        {
            nextSpawncorazon -= Time.deltaTime;
        }

    }
}
