using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeTerrains : MonoBehaviour
{
    private static TuileTerrain[] listeTerrains;
    public static TuileTerrain[] TousTerrains
    {
        get
        {
            TuileTerrain[] liste = FindObjectsOfType<TuileTerrain>();

            if (listeTerrains == null || listeTerrains.Length != liste.Length)
            {
                listeTerrains = liste; 
            }
            return listeTerrains;
        }
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
