using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, lengthY, startX, startY;
    public Camera cam;
    public float parallaxEffectX;
    public float parallaxEffectY;

    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffectX));
        float distX = (cam.transform.position.x * parallaxEffectX);
        float tempY = (cam.transform.position.y * (1 - parallaxEffectY));
        float distY = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startX + distX, startY + distY, transform.position.z);

        if (tempX > startX + lengthX) startX += lengthX;
        else if (tempX < startX - lengthX) startX -= lengthX;

        if (tempY > startY + lengthY) startY += lengthY;
        else if (tempY < startY - lengthY) startY -= lengthY;
    }
}
