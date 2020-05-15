using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DamierGen : MonoBehaviour
{
    private static DamierGen actuel;
    public static DamierGen Actuel
    {
        get
        {
            if(actuel == null)
            {
                actuel = FindObjectOfType<DamierGen>();
            }
            return actuel;
        }
    }

    //[SerializeField] public GameObject tuileCarree;
    [SerializeField] public GameObject tuileHexa;
    [SerializeField] DamierFleuveGen damierFleuve;

    [HideInInspector] public bool genererEnTuileCarree = false;
    [HideInInspector] public bool genererEnTuileHexa = true;
    public TuileManager[,] damier;

    public int colonnes = 1;
    public int lignes = 1;

    /* OBSOLET
    [Header("Génération Procédurale")]
    [SerializeField] bool genererProceduralement;
    [SerializeField] public int colonnes = 1;
    [SerializeField] public int lignes = 1;
    [SerializeField] public float zoom;
    [Range(0.00001f,1)]
    [SerializeField] public float persistance;
    [SerializeField] public float lacunarite;
    [SerializeField] public int octaves;
    [SerializeField] public int seed;
    [SerializeField] public Vector2 decalage;
    */

    /*OBSOLET
   private void OnValidate()
   {
       
       if(colonnes < 1)
       {
           colonnes = 1;
       }
       if(lignes < 1)
       {
           lignes = 1;
       }
       if(zoom < 0)
       {
           zoom = 0;
       }
       if(lacunarite < 0)
       {
           lacunarite = 0;
       }
       if(octaves<1)
       {
           octaves = 1;
       }
       
}
*/

      
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    /* OBSOLET
    public void GenDamierCarre(int x, int y)
    { 
        int col = 0;
        int ligne = -1;


        float tailleTuile = tuileCarree.GetComponent<SpriteRenderer>().bounds.size.x;

        Vector3 positionTuile = new Vector3();


        for(int i = 0; i < x*y; i++)
        {
            if (i % x != 0)
            {

                col++;
            }
            else if (ligne <= y)
            {

                col = 0;
                ligne++;
            }

            positionTuile.x = col * tailleTuile;
            positionTuile.y = ligne * tailleTuile;

            GameObject nvlTuile = Instantiate(tuileCarree, transform);
            nvlTuile.transform.position += positionTuile;
            nvlTuile.name = "Tuile" + i;


        }
    }
    */

    #region GENERATEUR
    public void GenDamier(MappeSysteme.Mappe mappe)
    {
        colonnes = mappe.colonnes;
        lignes = mappe.lignes;

        ClearDamier(true);

        TuileManager[,] damierTuiles = new TuileManager[mappe.colonnes,mappe.lignes];

        int col = 0;
        int ligne = -1;


        float tailleTuileX = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;
        float tailleTuileY = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 positionTuile = new Vector3();

        for (int i = 0; i < mappe.colonnes * mappe.lignes; i++)
        {

            if (i % mappe.colonnes != 0)
            {

                col++;
            }
            else if (ligne <= mappe.lignes)
            {

                col = 0;
                ligne++;
            }

            positionTuile.x = col * tailleTuileX + ((tailleTuileX / 2) * (ligne % 2));
            positionTuile.y = ligne * (tailleTuileY / 4 * 3);


            GameObject nvlTuile = Instantiate(tuileHexa, transform);

            nvlTuile.transform.position += positionTuile;

            damierTuiles[col, ligne] = nvlTuile.GetComponent<TuileManager>();
            damierTuiles[col, ligne].SetTerrain(mappe.mappeTerrains[col, ligne]);
            

        }
       foreach(TuileManager tuile in damierTuiles)
        {
            tuile.Init();
        }

        RenommerTuilesDamier();

        damierFleuve.GenererDamierFleuve(mappe);
    }


    public void GenDamier(int x, int y)
    {
        ClearDamier(true);

        colonnes = x;
        lignes = y;

        TuileManager[,] damierTuiles = new TuileManager[x,y];

        int col = 0;
        int ligne = -1;


        float tailleTuileX = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;
        float tailleTuileY = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;


        Vector3 positionTuile = new Vector3();
        

        for (int i = 0; i < x * y; i++)
        {

            if (i % x != 0)
            {

                col++;
            }
            else if (ligne <= y)
            {

                col = 0;
                ligne++;
            }

            positionTuile.x = col * tailleTuileX + ((tailleTuileX / 2) * (ligne % 2));
            positionTuile.y = ligne * (tailleTuileY / 4 * 3);

            GameObject nvlTuile = Instantiate(tuileHexa, transform);



            nvlTuile.transform.position += positionTuile;

            damierTuiles[col, ligne] = nvlTuile.GetComponent<TuileManager>();
            
            
        }

        foreach (TuileManager tuile in damierTuiles)
        {
            tuile.Init();
        }

        RenommerTuilesDamier();

        damierFleuve.GenererDamierFleuve(x, y);
        /* OBSOLET
        if(genererProceduralement)
        {
            gP.AttribuerHauteur(damier, gP.CreerMappeHauteur(x, y, zoom, octaves, lacunarite, persistance, seed, decalage));//On attribue une hauteur à chaque tuile
            terrain.AttribuerTerrain(damier); //En fonction de cette hauteur, on attribu à un terrain. 
        }
        */
    }
    #endregion

    #region MODIFICATEUR
    public void AjouterTuiles(int nbrColonne, int nbrLigne)
    {
        float tailleTuileX = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;
        float tailleTuileY = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;


        Vector3 positionTuile = new Vector3();

        

        for (int y = 0; y < lignes + nbrLigne; y++)
        {
            for (int x = 0; x < nbrColonne; x++)
            {
                positionTuile.x = (colonnes + x ) * tailleTuileX + ((tailleTuileX / 2) * (y % 2));
                positionTuile.y =  y * (tailleTuileY / 4 * 3);

                GameObject nvlTuile = Instantiate(tuileHexa, transform);

                nvlTuile.transform.position += positionTuile;
                
                //Changer la place dans la hierarchie
                nvlTuile.transform.SetSiblingIndex(colonnes+x + ((nbrColonne + colonnes)* y));

                nvlTuile.GetComponent<TuileManager>().Init();

            }
        }

        positionTuile = Vector3.zero;

        if(nbrLigne > 0)
        {
            for (int y = 0; y < nbrLigne; y++)
            {
                for (int x = 0; x < colonnes; x++)
                {
                    positionTuile.x = x * tailleTuileX + ((tailleTuileX / 2) * ((lignes + y) % 2));
                    positionTuile.y = (lignes + y) * (tailleTuileY / 4 * 3);

                    GameObject nvlTuile = Instantiate(tuileHexa, transform);

                    nvlTuile.transform.position += positionTuile;

                    //Changer la place dans la hierarchie
                    nvlTuile.transform.SetSiblingIndex(x + ((nbrColonne + colonnes) * (y + lignes)));

                    nvlTuile.GetComponent<TuileManager>().Init();
                }
            }
        }

        //On met à jour les dimensions du damier
        colonnes += nbrColonne;
        lignes += nbrLigne;

        RenommerTuilesDamier();

        damierFleuve.AjouterNoeuds(nbrColonne, nbrLigne);
    }

    public void RetirerTuiles(int nbrColonne, int nbrLigne)
    {
        damier = RecupDamier();
        for (int y = 0; y < damier.GetLength(1); y++)
        {
            for (int x = 0; x < damier.GetLength(0); x++)
            {
                char[] nomTuile = damier[x, y].gameObject.name.ToCharArray();
                int col = 0;
                int lig = 0;

                //Extrait les coordonnées de la tuile en fonction de son nom
                string actu = "" ;
                for (int z = 0; z < nomTuile.Length; z++)
                {
                    actu += nomTuile[z];

                    if(actu == "Tuile")
                    {
                        actu = "";
                    }
                    else if(actu.Contains(":"))
                    {
                        actu = actu.Remove(actu.IndexOf(':'));
                        col = int.Parse(actu);
                        actu = "";
                    }
                    else if(z == nomTuile.Length - 1)
                    {
                        lig = int.Parse(actu);
                    }
                }

                //Compare les coordonnées au nombre de ligne et de colonne qui doit être retiré
                if (col >= colonnes - nbrColonne)
                {
                    DestroyImmediate(damier[x, y].gameObject);
                }
                else if(lig >= lignes - nbrLigne)
                {
                    DestroyImmediate(damier[x, y].gameObject);
                }
            }
        }

        colonnes -= nbrColonne;
        lignes -= nbrLigne;

        RenommerTuilesDamier();

        damierFleuve.RetirerNoeuds(nbrColonne, nbrLigne);
    }

    private void RenommerTuilesDamier()
    {
        damier = RecupDamier();

        //Renommer les tuiles 
        int col = 0;
        int lig = 0;
        for (int i = 0; i < damier.Length; i++)
        {
            col = i % colonnes;

            if (i != 0 && col == 0)
            {
                lig++;
            }

            damier[col, lig].gameObject.name = "Tuile" + col + ":" + lig;
            //print("intération " + i + " : " + damier[col, lig].gameObject.name + " : "+ damier[col, lig].transform.GetSiblingIndex());
        }
    }

    #endregion

    public void ClearDamier(bool utiliseDestroyImmediate)
    {
        GameObject[] damier = GameObject.FindGameObjectsWithTag("Tuile");

        if(utiliseDestroyImmediate)
        {
            foreach (GameObject go in damier)
            {
                DestroyImmediate(go);
            }
        }
        else
        {
            foreach (GameObject go in damier)
            {
                Destroy(go);
            }
        }
        
    }

    public TuileManager[,] RecupDamier() 
    {
        TuileManager[,] damier = new TuileManager[colonnes, lignes];
        TuileManager[] damierRef = GetComponentsInChildren<TuileManager>();

        int index = 0;
        for (int y = 0; y < lignes; y++)
        {
            for (int x = 0; x < colonnes; x++)
            {
                damier[x, y] = damierRef[index];
                damierRef[index].transform.SetSiblingIndex(index);
                
                index++;
            }
        }
    
        return damier;
    }

    /* OBSOLET
    //Fonction de débugg
    private void ColorerModuloMappehauteur(float[,] mappeHauteur, TuileManager[,] damier)
    {
        for (int y = 0; y < mappeHauteur.GetLength(1); y++)
        {
            for (int x = 0; x < mappeHauteur.GetLength(0); x++)
            {
                damier[x, y].ColorerTuile(new Color(mappeHauteur[x, y], 0, 0));
            }
        }
    }
    */

    
}
