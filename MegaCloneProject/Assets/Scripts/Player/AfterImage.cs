using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f; //how long has the game object been active
    private float timeActivated; //keeps track of how long it was activated
    private float alpha; //alpha channel
    [SerializeField]
    private float alphaSet = 0.8f; //sets the alpha to 0.8
    private float alphaMultiplier = 0.85f; //the smaller this # is , the faster the afterimages will fade.

    private Transform player; //reference to player gameobject, so that you can obtain the rotation and position.

    private SpriteRenderer SR; //refrence to spriteRenderer on this GameObject
    private SpriteRenderer PlayerSR; //reference to player sprite, so that you can obtain the current sprite.

    private Color color; //since the transparancy of the image will change over time, you will need to change the color

    private void OnEnable()   //gets called everytime you enable the gameObject.
    {
        SR = GetComponent<SpriteRenderer>(); 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = PlayerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if(Time.time>= (timeActivated + activeTime))
        {
            //add back to pool
            AfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
