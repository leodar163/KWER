using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeRecettes : MonoBehaviour
{
    public List<Recette> listeDesRecettes;

    private static ListeRecettes listeRecettes;

    public static ListeRecettes Defaut
    {
        get
        {
            if(listeRecettes == null)
            {
                listeRecettes = FindObjectOfType<ListeRecettes>();
            }
            return listeRecettes;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        listeRecettes = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
