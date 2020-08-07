using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

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
    private TuileManager[,] damier;

    public TuileManager[,] Damier
    {
        get
        {
            if (damier == null) damier = RecupDamier();
            return damier;
        }
    }

    public int colonnes = 1;
    public int lignes = 1;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
            positionTuile.z = ligne * 0.01f;

            GameObject nvlTuile = Instantiate(tuileHexa, transform);

            nvlTuile.transform.position += positionTuile;

            damierTuiles[col, ligne] = nvlTuile.GetComponent<TuileManager>();
            damierTuiles[col, ligne].SetTerrain(mappe.mappeTerrains[col, ligne]);
            

        }
       foreach(TuileManager tuile in damierTuiles)
        {
            tuile.Init();
        }

        damier = RecupDamier();

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
            positionTuile.z = ligne * 0.01f;

            GameObject nvlTuile = Instantiate(tuileHexa, transform);



            nvlTuile.transform.position += positionTuile;

            damierTuiles[col, ligne] = nvlTuile.GetComponent<TuileManager>();
            
            
        }

        foreach (TuileManager tuile in damierTuiles)
        {
            tuile.Init();
        }

        damier = RecupDamier();

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
                positionTuile.z = y * 0.01f;

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
                    positionTuile.z = y * 0.01f;

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

        

        damierFleuve.AjouterNoeuds(nbrColonne, nbrLigne);
        damier = RecupDamier();

        RenommerTuilesDamier();
    }

    public void RetirerTuiles(int nbrColonne, int nbrLigne)
    {
        for (int y = 0; y < Damier.GetLength(1); y++)
        {
            for (int x = 0; x < Damier.GetLength(0); x++)
            {
                char[] nomTuile = Damier[x, y].gameObject.name.ToCharArray();
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
                    DestroyImmediate(Damier[x, y].gameObject);
                }
                else if(lig >= lignes - nbrLigne)
                {
                    DestroyImmediate(Damier[x, y].gameObject);
                }
            }
        }

        colonnes -= nbrColonne;
        lignes -= nbrLigne;

        

        damierFleuve.RetirerNoeuds(nbrColonne, nbrLigne);

        damier = RecupDamier();

        RenommerTuilesDamier();
    }

    private void RenommerTuilesDamier()
    {
        //Renommer les tuiles 
        int col;
        int lig = 0;
        for (int i = 0; i < Damier.Length; i++)
        {
            col = i % colonnes;

            if (i != 0 && col == 0)
            {
                lig++;
            }

            Damier[col, lig].gameObject.name = "Tuile" + col + ":" + lig;
        }
    }

    #endregion

    #region RECUPERATION
    private TuileManager[,] RecupDamier() 
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

    public Vector2Int RecupererCoordonneesTuile(TuileManager tuile)
    {
        string nomTuile = tuile.gameObject.name;
        int x;
        int y;

        nomTuile = nomTuile.Remove(0, "Tuile".Length);
        string[] coordonnees = nomTuile.Split(':');
        x = int.Parse(coordonnees[0]);
        y = int.Parse(coordonnees[1]);

        return new Vector2Int(x, y);
    }

    public TuileManager[] RecupTuilesFrontalieres()
    {
        TuileManager[] retour = new TuileManager[2*lignes + 2*colonnes];

        int index = 0;

        TuileManager[] premierLigne = TrouverTuilesParLigne(0);
        TuileManager[] derniereLigne = TrouverTuilesParLigne(lignes - 1);

        TuileManager[] premiereColonne = TrouverTuilesParColonne(0);
        TuileManager[] derniereColonne = TrouverTuilesParColonne(colonnes - 1);

        while (index < retour.Length)
        {
            if(index < premierLigne.Length)
            {
                retour[index] = premierLigne[index];
            }
            else if(index < derniereLigne.Length + premierLigne.Length)
            {
                retour[index] = derniereLigne[index - premierLigne.Length];
            }
            else if(index < premiereColonne.Length + derniereLigne.Length + premierLigne.Length)
            {
                retour[index] = premiereColonne[index - (derniereLigne.Length + premierLigne.Length)];
            }
            else
            {
                retour[index] = derniereColonne[index - (premiereColonne.Length + derniereLigne.Length + premierLigne.Length)];
            }

            index++;
        }

        return retour;
    }

    /// <summary>
    /// Trouve la tuile qui correspond aux coordonnées
    /// </summary>
    /// <param name="axeX">Coordonnée sur l'axe x</param>
    /// <param name="axeY">Coordonnée sur l'axe y</param>
    /// <returns></returns>
    public TuileManager TrouverTuileParCoordonnee(int axeX, int axeY)
    {
        return Damier[axeX, axeY];
    }

    public TuileManager TrouverTuileParCoordonnee(Vector2Int coordonnees)
    {
        return Damier[coordonnees.x, coordonnees.y];
    }

    public TuileManager[] TrouverTuilesParEtendue(int minX, int minY, int maxX, int maxY)
    {
        TuileManager[,] damier = Damier;
        TuileManager[] retour = new TuileManager[(maxX - minX) * (maxY -minY)];

        int index = 0;

        for (int i = minY; i < maxY; i++)
        {
            for (int j = minX; j < maxX; j++)
            {
                retour[index] = damier[j, i];
                index++;
            }
        }
        return retour;
    }

    /// <summary>
    /// Marche pas, faut pas l'utiliser
    /// </summary>
    /// <param name="coordonneesDeb"></param>
    /// <param name="coordonneesFin"></param>
    /// <returns></returns>
    public TuileManager[] TrouverTuilesParEtendue(Vector2Int coordonneesDeb, Vector2Int coordonneesFin)
    {
        TuileManager[,] damier = Damier;
        TuileManager[] retour = new TuileManager[(coordonneesFin.x - coordonneesDeb.x) * (coordonneesFin.y - coordonneesDeb.y)];

        int index = 0;

        for (int i = coordonneesDeb.y; i < coordonneesFin.y; i++)
        {
            for (int j = coordonneesDeb.x; j < coordonneesFin.x; j++)
            {
                retour[index] = damier[j, i];
                index++;
            }
        }
        return retour;
    }

    public TuileManager[] TrouverTuilesParColonne(int indexColonne)
    {
        TuileManager[,] damier = Damier;
        TuileManager[] retour = new TuileManager[damier.GetLength(1)];

        for (int i = 0; i < damier.GetLength(1); i++)
        {
            retour[i] = damier[indexColonne, i];
        }

        return retour;
    }

    public TuileManager[] TrouverTuilesParLigne(int indexLigne)
    {
        TuileManager[,] damier = Damier;
        TuileManager[] retour = new TuileManager[damier.GetLength(0)];

        for (int i = 0; i < damier.GetLength(0); i++)
        {
            retour[i] = damier[i, indexLigne];
        }

        return retour;
    }
    #endregion

    public void ClearDamier(bool utiliseDestroyImmediate)
    {
        TuileManager[,] damier = Damier;

        if(utiliseDestroyImmediate)
        {
            foreach (TuileManager tuile in damier)
            {
                DestroyImmediate(tuile.gameObject);
            }
        }
        else
        {
            foreach (TuileManager tuile in damier)
            {
                Destroy(tuile.gameObject);
            }
        }
        
    }

}
