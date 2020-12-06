﻿using UnityEngine;
using UnityEngine.Assertions;


public class Starfield : MonoBehaviour
{
    public int MaxStars = 100;
    public float StarSize = 0.1f;
    public float StarSizeRange = 0.5f;
    public float FieldWidth = 20f;
    public float FieldHeight = 25f;
    public float velocity = 0.25f;
    public bool Colorize = false;
    public float ParallaxFactor;

    public Camera cam;

    float xOffset;
    float yOffset;

    ParticleSystem Particles;
    ParticleSystem.Particle[] Stars;


    void Start()
    {
        cam = Camera.main;
    }
    void Awake()
    {
        Stars = new ParticleSystem.Particle[MaxStars];
        Particles = GetComponent<ParticleSystem>();

        Assert.IsNotNull(Particles, "Particle system missing from object!");

        xOffset = FieldWidth * 0.5f;                                                                                                        // Offset the coordinates to distribute the spread
        yOffset = FieldHeight * 0.5f;                                                                                                       // around the object's center

        for (int i = 0; i < MaxStars; i++)
        {
            float randSize = Random.Range(StarSizeRange, StarSizeRange + 1f);                       // Randomize star size within parameters
            float scaledColor = (true == Colorize) ? randSize - StarSizeRange : 1f;         // If coloration is desired, color based on size

            Stars[i].position = GetRandomInRectangle(FieldWidth, FieldHeight) + transform.position;
            Stars[i].startSize = StarSize * randSize;
            Stars[i].startColor = new Color(1f, scaledColor, scaledColor, 1f);
        }
        Particles.SetParticles(Stars, Stars.Length);                                                                // Write data to the particle system
    }


    // GetRandomInRectangle
    //----------------------------------------------------------
    // Get a random value within a certain rectangle area
    //
    Vector3 GetRandomInRectangle(float width, float height)
    {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset, 0);
    }


    void Update()
    {

        Vector3 screenPoint = cam.WorldToViewportPoint(this.transform.position);

        Vector3 newPos = cam.transform.position ;                   // Calculate the position of the object
        newPos.z = 0;                       // Force Z-axis to zero, since we're in 2D
        transform.position = newPos;


        for (int i = 0; i < MaxStars; i++)
        {
            Vector3 pos = Stars[i].position + transform.position;

            if (pos.x < screenPoint.x - xOffset)
            {
                pos.x += FieldWidth;
            }
            else 
            {
                pos.x -= velocity * ParallaxFactor * Time.deltaTime;
   
            }


            Stars[i].position = pos - transform.position ;
        }


        Particles.SetParticles(Stars, Stars.Length);

    }



}