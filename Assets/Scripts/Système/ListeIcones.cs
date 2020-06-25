using System.Collections;
using System.Collections.Generic;
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
    public Sprite iconeSlot;

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

    public Sprite TrouverIconeRessource(Ressource ressource)
    {
        foreach (Sprite icone in listeIconeRessource)
        {
            if (icone.name.EndsWith(ressource.nom))
            {
                return icone;
            }
        }

        Debug.LogError("Il faut un icone pour " + ressource.nom + " ou le nom de la ressource est pas la bonne");
        return null;
    }


}
