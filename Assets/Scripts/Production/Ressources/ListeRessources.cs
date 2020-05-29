using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeRessources : MonoBehaviour
{

    public Ressource[] listeDesRessources;

    private static ListeRessources listeRessources;

    public static ListeRessources Defaut
    {
        get
        {
            if (listeRessources == null)
            {
                listeRessources = FindObjectOfType<ListeRessources>();
            }
            return listeRessources;
        }
    }

   

    // Start is called before the first frame update
    void Start()
    {
        listeRessources = this;
        foreach (Ressource ressource in listeDesRessources)
        {
            ressource.icone = ListeIcones.Defaut.TrouverIconeRessource(ressource.nom);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int TrouverIndexRessource(string nom)
    {
        for (int i = 0; i < listeDesRessources.Length; i++)
        {
            if(listeDesRessources[i].nom == nom)
            {
                return i;
            }
        }
        return -1;
    }

    public int TrouverIndexRessource(Sprite Icone)
    {
        for (int i = 0; i < listeDesRessources.Length; i++)
        {
            if(Icone.name.EndsWith(listeDesRessources[i].nom))
            {
                return i;
            }
        }
        return -1;
    }
}
