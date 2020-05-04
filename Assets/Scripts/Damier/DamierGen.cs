using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DamierGen : MonoBehaviour
{
    [SerializeField] public GameObject tuileCarree;
    [SerializeField] public GameObject tuileHexa;


    [HideInInspector] public bool genererEnTuileCarree = false;
    [HideInInspector] public bool genererEnTuileHexa = true;
    public TuileManager[,] damier;


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

    // Start is called before the first frame update
    void Start()
    {
        //print(damier[0, 0].gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }


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

    public void GenDamierHexa(MappeSysteme.Mappe mappe)
    {

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
            nvlTuile.name = "Tuile" + i;

            damierTuiles[col, ligne] = nvlTuile.GetComponent<TuileManager>();
            damierTuiles[col, ligne].SetTerrain(mappe.mappeTerrains[col, ligne]);
            

        }
       foreach(TuileManager tuile in damierTuiles)
        {
            tuile.Init();
        }
    }


    public void GenDamierHexa(int x, int y)
    {

        TuileManager[,] damierTuiles = new TuileManager[x,y];

        int col = 0;
        int ligne = -1;


        float tailleTuileX = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;
        float tailleTuileY = tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;


        Vector3 positionTuile = new Vector3();

        Terrains terrain = FindObjectOfType<Terrains>();
        GenerationProcedurale gP = GameObject.FindObjectOfType<GenerationProcedurale>();

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
            nvlTuile.name = "Tuile" + i;

            damierTuiles[col, ligne] = nvlTuile.GetComponent<TuileManager>();
            
            
        }

        foreach (TuileManager tuile in damierTuiles)
        {
            tuile.Init();
        }

        /* OBSOLET
        if(genererProceduralement)
        {
            gP.AttribuerHauteur(damier, gP.CreerMappeHauteur(x, y, zoom, octaves, lacunarite, persistance, seed, decalage));//On attribue une hauteur à chaque tuile
            terrain.AttribuerTerrain(damier); //En fonction de cette hauteur, on attribu à un terrain. 
        }
        */
    }

    public TuileManager[,] RecupDamier()//Je suis obligé de recréer la variable parce qu'à chaque fois que j'appuye sur play, damier se reset ! C'est méga chiant ! 
    {
        TuileManager[,] damier = new TuileManager[colonnes,lignes];
        TuileManager[] damierRef = FindObjectsOfType<TuileManager>();

            for (int y = 0; y < lignes; y++)
            {
                for (int x = 0; x < colonnes; x++)
                {
                    damier[x, y] = damierRef[x + y];
                }
            }

        return damier;
    }

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


    
}
