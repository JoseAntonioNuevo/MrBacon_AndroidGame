using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaBasic : MonoBehaviour
{
    Camera mainCamera;
    public float posicionX = 0f;

    [SerializeField]
    float health = 100f;

    [SerializeField]
    float speed = 1f;

    [SerializeField]
    bool isFloating = false;

    [SerializeField]
    float floatingsSpeed = 0.5f;

    [SerializeField]
    float minDelay = 1f;

    [SerializeField]
    float maxDelay = 3f;

    [SerializeField]
    GameObject[] weapon = null;

    [SerializeField]
   // ProjectileData[] projectiles = null;

    float fireDelay = 0;

    public float time = 0;
    public int random = 0;

    Vector3 posicionVentana;
    Rigidbody2D rg;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Floating();
       // FireController();

        posicionVentana = mainCamera.WorldToViewportPoint(transform.position);


        //posicionVentana.y = Mathf.Clamp(posicionVentana.y, 0.075f, 0.85f);
        if (posicionVentana.y < 0.060f)
            rg.AddForce(new Vector2(0, 20));

        if (posicionVentana.y > 0.70f)
            rg.AddForce(new Vector2(0, -20));
        //transform.position = mainCamera.ViewportToWorldPoint(posicionVentana);
    }

    void Floating()
    {

        if (isFloating)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * floatingsSpeed);
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
}
