using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ListeIcones : MonoBehaviour
{
    private static ListeIcones listeIcones;

    public static ListeIcones Defaut
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
    [Header("nom icone = \"Icone\" + \"NomRessource\"")]
    public Sprite[] listeIconeRessource;


    [Header("Icones démograpie")]
    public Sprite iconePopulation;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite TrouverIconeRessource(string nom)
    {
        foreach (Sprite icone in listeIconeRessource)
        {
            if (icone.name.EndsWith(nom))
            {
                return icone;
            }
        }

        Debug.LogError("Il faut un icone pour " + nom + " ou le nom de la ressource est pas la bonne");
        return null;
    }


}
