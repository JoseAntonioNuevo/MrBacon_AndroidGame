using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public GameObject meteor;
    public float speed = 1f;
    public int daño;
    public bool elementoActivado;//Determina si el elemento puede hacer daño.

    Transform destructor;
    Vector3 localScale;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        elementoActivado = true;
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        destructor = GameObject.Find("destructorLeft").GetComponent<Transform>();
    
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > destructor.position.x)
        {
            localScale.x = -0.5f;
            transform.localScale = localScale;
            rb.velocity = new Vector2(localScale.x * speed, rb.velocity.y);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}
