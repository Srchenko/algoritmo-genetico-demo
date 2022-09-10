using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flor : MonoBehaviour
{
    public GameObject flor;
    public GameObject tallo;
    private SpriteRenderer render;

    void Awake()
    {
        render = tallo.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        flor.transform.position = new Vector2(tallo.transform.position.x, tallo.transform.position.y + (render.bounds.size.y/2));
    }
}
