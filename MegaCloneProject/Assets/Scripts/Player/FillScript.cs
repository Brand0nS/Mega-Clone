using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillScript : MonoBehaviour
{
    public Image Fill;
    private PlayerController2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fill.fillAmount = player.healthBar / player.maxHealth; 
    }
}
