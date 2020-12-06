using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject meteor;
    public float velocidadMeteoritos;
    float randY;
    Vector2 whereToSpawn;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randY = Random.Range(-4.5f,4.5f);
            whereToSpawn = new Vector2(transform.position.x, randY);
            meteor.GetComponent<movimiento>().speed = velocidadMeteoritos;
            Instantiate(meteor, whereToSpawn, Quaternion.identity);
            
        }
    }
}
