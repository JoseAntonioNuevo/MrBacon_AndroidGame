using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageParalax : MonoBehaviour
{
    public float velocity;
    public float examine;

    public GameObject[] layes;

    public Transform otraBack;
    public float lenght;
    Camera theCamera;
    Vector3 theDimension;

    Vector3 theStartPosition;

    void Start()
    {
        theCamera = Camera.main;

      /*if(otraBack.position.x < transform.position.x)
            transform.position = newPos;*/
    }

    void Update()
    {
        EndlessRepeater();
        MoverLayer();
    }



    void MoverLayer() {

        foreach (GameObject obj in layes) {
            Vector3 newPos = obj.transform.position;
            newPos.x = newPos.x -(velocity/100);
            obj.transform.position = newPos;
           
        }
    }

    void EndlessRepeater()
    {

        for (int i = 0; i < layes.Length; i++)
        {
            Vector3 posRieght = layes[i].transform.position;
            posRieght.x += layes[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;
            examine = theCamera.WorldToScreenPoint(posRieght).x;

            if (theCamera.WorldToScreenPoint(posRieght).x < 0)
            {
                int positionOtherLayer;
                if (i == layes.Length - 1)
                    positionOtherLayer = 0;
                else
                    positionOtherLayer = i + 1;

                float lengt = layes[i].GetComponent<Renderer>().bounds.size.x;

                layes[i].transform.position = layes[positionOtherLayer].transform.position + new Vector3(lengt, 0);
            }
        }



    }
}