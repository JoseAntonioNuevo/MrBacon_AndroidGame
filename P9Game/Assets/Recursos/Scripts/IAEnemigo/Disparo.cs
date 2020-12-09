using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public float velX = 9f;
    public float velY = 0f;
    Rigidbody2D rb;
    Transform destructor;
    // Start is called before the first frame update
    void Start()
    {
        destructor = GameObject.Find("destructorLeft").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > destructor.position.x)
        {
            rb.velocity = new Vector2(-velX, velY);

        }
        else
        {
            Destroy(this.gameObject);
        }
        

        //Destroy(gameObject, 3f);

    }

}
