using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    SpriteRenderer spR;

    // Start is called before the first frame update
    void Start()
    {
        spR = GetComponent<SpriteRenderer>();
        spR.sprite = ListeIcones.Defaut.iconePopulation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
