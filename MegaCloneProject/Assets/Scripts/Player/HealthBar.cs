using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
   public float healthBar;
    [SerializeField]
    public float maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
