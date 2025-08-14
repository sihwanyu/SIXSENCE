using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ControllerPlayer : MonoBehaviour
{
    public Sprite Upsprite;
    public Sprite Rightsprite;
    public Sprite Leftsprite;
    public Sprite Downsprite;
    public Sprite Standsprite;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Standsprite;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            spriteRenderer.sprite = Upsprite;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.sprite = Leftsprite;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            spriteRenderer.sprite = Downsprite;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.sprite = Rightsprite;
        }
        else
        {
            spriteRenderer.sprite = Standsprite;
        }
    }
}