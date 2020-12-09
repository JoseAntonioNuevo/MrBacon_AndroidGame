using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryMenu : MonoBehaviour
{
    public float velocity;
    RectTransform transform;
    bool goRight;
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<RectTransform>();
        goRight = true;
    }

    // Update is called once per frame
    void Update()
    {



        if (this.transform.rotation.z > 0.20f && goRight == true)
            goRight = false;

        if (this.transform.rotation.z < -0.05f && goRight == false)
            goRight = true;



        if (goRight)
            transform.Rotate(new Vector3(0, 0, velocity) * Time.deltaTime);
        else
            transform.Rotate(new Vector3(0,0,-velocity) * Time.deltaTime);
    }
}
