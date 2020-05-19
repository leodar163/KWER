using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeIcones : MonoBehaviour
{
    private static ListeIcones listeIcones;

    public static ListeIcones ParDefaut
    {
        get
        {
            if (listeIcones == null)
            {
                listeIcones = FindObjectOfType<ListeIcones>();
            }
            return listeIcones;
        }
    }

    [Header("Icones de ressource")]
    public Sprite IconeNourriture;
    public Sprite IconePeau;
    public Sprite IconePierre;
    public Sprite IconePigment;
    public Sprite IconeOutil;

    private void Awake()
    {
        listeIcones = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
