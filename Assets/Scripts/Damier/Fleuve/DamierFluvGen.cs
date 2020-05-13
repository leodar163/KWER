using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamierFluvGen : MonoBehaviour
{
    [SerializeField] DamierGen damierGen;
    [SerializeField] GameObject noeudFleuve;

    NoeudFleuve[,] damierFleuve;

    public int colonnes;
    public int lignes;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region GENERATEUR

    public void GenererDamierFleuve(int nbrColonne, int nbrLigne)
    {
        ClearDamierFleuve();

        colonnes = nbrColonne;
        lignes = nbrLigne * 2;

        //print("Lignes : " + lignes);
        //print("colonnes : " + colonnes);

        damierFleuve = new NoeudFleuve[colonnes, lignes];

        //print(damierFleuve.GetLength(1));

        float tailleTuileX = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;
        float tailleTuileY = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 position = new Vector3();

        for (int y = 0; y < damierFleuve.GetLength(1); y++)
        {
            position.x = 0;

            if(y !=0)
                position.y += tailleTuileY / 2;

            for (int x = 0; x < damierFleuve.GetLength(0); x++)
            {
                //print(y % 4);
                if (y != 0 && y != 1)
                {
                    if(x == 0)
                    {
                        if(y % 4 == 0   )
                        {
                            position.y -= tailleTuileY/4;
                        }
                        else if (y % 4 == 2 || y % 4 == 3)
                        {
                            position.x -= tailleTuileX / 2;

                            if(y % 4 == 2)
                                position.y -= tailleTuileY / 4;
                        }
                    }
                }

                position.x += tailleTuileX;
                
                GameObject nvNoeud = Instantiate(noeudFleuve, transform);

                nvNoeud.transform.position += position;
                //print(nvNoeud.transform.position);   
            }
        }
        RenommerNoeudsFleuve();
    }

    #endregion


    #region MODIFICATEUR

    //Ajoute des noeud de fleuve au damier -- ZERO ELEGANCE DANS LE CODE
    public void AjouterNoeuds(int nbrColonne, int nbrLigne)
    {
        int lignesParcourues = 0;

        float tailleTuileX = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;
        float tailleTuileY = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 position = new Vector3();

        //Génère l'ajout des lignes 
        for (int i = 0; i < nbrLigne; i++)
        {
            for (int y = 0; y < 2; y++)
            {
                //print(lignesParcourues);
                position.x = 0;

                position.y = Recupererhauteur(nbrLigne - i) + (tailleTuileY / 2 * y);


                for (int x = 0; x < colonnes; x++)
                {
                    //Si la ligne à partir de laquelle on ajoute des ligne était impaire
                    if ((damierGen.lignes - (nbrLigne - i)) % 2 != 0 && x == 0)
                    {
                        //On ajoute un décalage vers la gauche
                        position.x -= tailleTuileX / 2;
                    }

                    position.x += tailleTuileX;
                    //print(position);

                    GameObject nvNoeud = Instantiate(noeudFleuve, transform);

                    nvNoeud.transform.position += position;

                    //Changer la place dans la hierarchie
                    int index = x + (lignesParcourues * damierGen.colonnes) + (lignes * damierGen.colonnes);
                    nvNoeud.transform.SetSiblingIndex(index);

                    //Color cDef = nvNoeud.GetComponent<SpriteRenderer>().color;
                    //nvNoeud.GetComponent<SpriteRenderer>().color = new Color(cDef.r , index, cDef.b);

                }
                lignesParcourues++;
            }
           
        }

        position = Vector3.zero;

        lignesParcourues = 0;

        //Génère l'ajout des colonnes
        for (int i = 0; i < damierGen.lignes; i++)
        {
            position.x = RecupererLargeur(nbrColonne);

            for (int y = 0; y < 2; y++)
            {
                
                position.x = RecupererLargeur(nbrColonne);

                for (int x = 0; x < nbrColonne; x++)
                {

                    //Si la ligne est paire 
                    if (i % 2 != 0  && x == 0)
                    {
                        //On décale sur la droite et vers la bas
                        if(y == 0)
                        {
                            position.y -= tailleTuileY / 4;
                        }
                        position.x -= tailleTuileX / 2;
                    }
                    else if(i % 2 == 0 && i != 0 && y == 0 && x == 0)
                    {
                        position.y -= tailleTuileY / 4;
                    }

                    position.x += tailleTuileX;

                    GameObject nvNoeud = Instantiate(noeudFleuve, transform);

                    nvNoeud.transform.position += position;

                    //Changer la place dans la hierarchie
                    nvNoeud.transform.SetSiblingIndex(colonnes + x + lignesParcourues * damierGen.colonnes);
                }

                position.y += tailleTuileY / 2;
                lignesParcourues++;
            }
        }
        

        
        

    
        //On met à jour les dimensions du damier
        colonnes += nbrColonne;
        lignes += nbrLigne * 2;


        RenommerNoeudsFleuve();
    }

    public void RetirerNoeuds(int nbrColonne, int nbrLigne)
    {
        damierFleuve = RecupDamierFleuve();
        for (int y = 0; y < damierFleuve.GetLength(1); y++)
        {
            for (int x = 0; x < damierFleuve.GetLength(0); x++)
            {
                char[] nomTuile = damierFleuve[x, y].gameObject.name.ToCharArray();
                int col = 0;
                int lig = 0;

                //Extrait les coordonnées du noeud en fonction de son nom
                string actu = "";
                for (int z = 0; z < nomTuile.Length; z++)
                {
                    actu += nomTuile[z];

                    if (actu == "NoeudFleuve")
                    {
                        actu = "";
                    }
                    else if (actu.Contains(":"))
                    {
                        actu = actu.Remove(actu.IndexOf(':'));
                        col = int.Parse(actu);
                        actu = "";
                    }
                    else if (z == nomTuile.Length - 1)
                    {
                        lig = int.Parse(actu);
                    }
                }

                //Compare les coordonnées au nombre de ligne et de colonne qui doit être retiré
                if (col >= colonnes - nbrColonne)
                {
                    DestroyImmediate(damierFleuve[x, y].gameObject);
                }
                else if (lig >= lignes - (nbrLigne * 2))
                {
                    DestroyImmediate(damierFleuve[x, y].gameObject);
                }
            }
        }

        colonnes -= nbrColonne;
        lignes -= nbrLigne * 2;

        RenommerNoeudsFleuve();
    }

    private void RenommerNoeudsFleuve()
    {
        damierFleuve = RecupDamierFleuve();

        for (int y = 0; y < damierFleuve.GetLength(1); y++)
        {
            for (int x = 0; x < damierFleuve.GetLength(0); x++)
            {
                damierFleuve[x, y].gameObject.name = "NoeudFleuve" + x + ":" + y;
            }
        }
    }

    #endregion

    public void ClearDamierFleuve()
    {
        NoeudFleuve[] damier = FindObjectsOfType<NoeudFleuve>();

        foreach(NoeudFleuve noeud in damier)
        {
            DestroyImmediate(noeud.gameObject);
        }
    }

    private NoeudFleuve[,] RecupDamierFleuve()
    {
        NoeudFleuve[,] damier = new NoeudFleuve[colonnes, lignes];
        NoeudFleuve[] damierRef = GetComponentsInChildren<NoeudFleuve>();
        

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

    private float Recupererhauteur()
    {
        float hauteur = 0;

        float tailleTuileY = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;

        hauteur = damierGen.lignes * (tailleTuileY * 3/4);
        

        return hauteur;
    }

    private float Recupererhauteur(float decalage)
    {
        float hauteur = 0;

        float tailleTuileY = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;

        hauteur = (damierGen.lignes - decalage) * (tailleTuileY * 3 / 4);

      
        return hauteur;
    }

    private float RecupererLargeur()
    {
        float largeur = 0;

        float tailleTuileX = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;

        largeur = (damierGen.colonnes) * (tailleTuileX);

        
        return largeur;
    }

    private float RecupererLargeur(float decalage)
    {
        float largeur    = 0;

        float tailleTuileX = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;

        largeur = (damierGen.colonnes - decalage) * (tailleTuileX);

        //print("colonne " + damierGen.colonnes + " -  décalage " + decalage + " = " + (damierGen.colonnes - decalage));
        return largeur;
    }
}
