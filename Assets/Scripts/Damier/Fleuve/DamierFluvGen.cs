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
        lignes = nbrLigne;

        damierFleuve = new NoeudFleuve[colonnes, lignes * 2];

        float tailleTuileX = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.x;
        float tailleTuileY = damierGen.tuileHexa.GetComponent<SpriteRenderer>().bounds.size.y;

        int indexY = -1;

        for (int y = 0; y < lignes; y++)
        {
            indexY = -1;

            for (int x = 0; x < colonnes; x++)
            {
                indexY += 2;

                Vector3 position1 = new Vector3();
                Vector3 position2 = new Vector3();

                if (y % 2 == 0)
                {
                    if (indexY == 1)
                    {
                        position1.x = tailleTuileX / 2 * x;
                        position1.y = tailleTuileY * y;

                        position2.x = position1.x;
                        position2.y = position2.y + tailleTuileY / 2;
                    }
                    else
                    {
                        position1.x = tailleTuileX * x;
                        position1.y = tailleTuileY * y;

                        position2.x = position1.x;
                        position2.y = position2.y + tailleTuileY / 2;
                    }


                    GameObject nvNoeud1 = Instantiate(noeudFleuve, transform);
                    GameObject nvNoeud2 = Instantiate(noeudFleuve, transform);


                    nvNoeud1.transform.position += position1;
                    nvNoeud2.transform.position += position2;

                    damierFleuve[x, indexY - 1] = nvNoeud1.GetComponent<NoeudFleuve>();
                    damierFleuve[x, indexY] = nvNoeud2.GetComponent<NoeudFleuve>();

                }
                else
                {
                    position1.x = tailleTuileX * x;
                    position1.y = tailleTuileY * y;

                    position2.x = position1.x;
                    position2.y = position2.y + tailleTuileY / 2;

                    GameObject nvNoeud1 = Instantiate(noeudFleuve, transform);
                    GameObject nvNoeud2 = Instantiate(noeudFleuve, transform);


                    nvNoeud1.transform.position += position1;
                    nvNoeud2.transform.position += position2;

                    damierFleuve[x, indexY - 1] = nvNoeud1.GetComponent<NoeudFleuve>();
                    damierFleuve[x, indexY] = nvNoeud2.GetComponent<NoeudFleuve>();
                }
            }
        }
    }

    #endregion


    #region MODIFICATEUR

    private void RenommerTuilesDamierFleuve()
    {
        damierFleuve = RecupDamierFleuve();

        //Renommer les tuiles 
        int x = 0;
        int y = 0;
        for (int i = 0; i < damierFleuve.Length; i++)
        {
            x = i % colonnes;

            if (i != 0 && x == 0)
            {
                y += 2;
            }

            damierFleuve[x, y].gameObject.name = "Tuile" + x + ":" + y;
            //print("intération " + i + " : " + damier[col, lig].gameObject.name + " : "+ damier[col, lig].transform.GetSiblingIndex());
        }
    }

    #endregion

    public void ClearDamierFleuve()
    {
        NoeudFleuve[] damier = FindObjectsOfType<NoeudFleuve>();

        foreach(NoeudFleuve noeud in damier)
        {
            DestroyImmediate(noeud);
        }
    }

    private NoeudFleuve[,] RecupDamierFleuve()
    {
        NoeudFleuve[,] damier = new NoeudFleuve[colonnes, lignes * 2];
        NoeudFleuve[] damierRef = GetComponentsInChildren<NoeudFleuve>();

        int index = -1;

        for (int y = 0; y < lignes; y++)
        {
            index = -1;

            for (int x = 0; x < colonnes; x++)
            {
                index += 2;

                damier[x, y] = damierRef[index - 1];
                damier[x, y] = damierRef[index];
                //print(damierRef[index].gameObject.name);
                index++;
            }
        }

        return damier;
    }
}
