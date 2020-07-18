using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetSpawn : MonoBehaviour
{
    [SerializeField] private GameObject pillard;
    public void SpawnPillard()
    {
        TuileManager[] tuilesFront = DamierGen.Actuel.RecupTuilesFrontalieres();

        TuileManager tuileSpawn = tuilesFront[Random.Range(0, tuilesFront.Length - 1)];

        Pillard nvPillard = Instantiate(pillard, tuileSpawn.transform.position,new Quaternion()).GetComponent<Pillard>();

        Hostile pillardHost = nvPillard.GetComponent<Hostile>();
        Guerrier[] guerriers = FindObjectsOfType<Guerrier>();
        int ptAtt = 0;
        int ptDef = 0;

        for (int i = 0; i < guerriers.Length; i++)
        {
            ptAtt += guerriers[i].attaque;
            ptDef += guerriers[i].defense;
        }

        pillardHost.attaque = Random.Range(ptAtt / guerriers.Length, ptAtt / guerriers.Length +1);
        pillardHost.defense = Random.Range(ptDef / guerriers.Length, ptAtt / guerriers.Length + 1);

        Demographie[] demographies = FindObjectsOfType<Demographie>();
        int popTotale = 0;

        for (int i = 0; i < demographies.Length; i++)
        {
            popTotale += demographies[i].taillePopulation;
        }

        pillardHost.nbrCombattant = Random.Range(popTotale / demographies.Length, popTotale / demographies.Length + 2);
    }

    public void SpawnTroupeau()
    {
        GameObject troupeauASpawn = ListeAnimaux.Defaut.domesticables[Random.Range(0, ListeAnimaux.Defaut.domesticables.Count - 1)];

        TuileManager[] tuilesFront = DamierGen.Actuel.RecupTuilesFrontalieres();
        List<TuileManager> tuilesPlaine = new List<TuileManager>();

        for (int i = 0; i < tuilesFront.Length; i++)
        {
            if(tuilesFront[i].terrainTuile is TerrainPlaine)
            {
                tuilesPlaine.Add(tuilesPlaine[i]);
            }
        }

        TuileManager tuileSpawn = tuilesPlaine[Random.Range(0, tuilesPlaine.Count - 1)];

        Instantiate(troupeauASpawn, tuileSpawn.transform.position, new Quaternion());
    }

    public void SpawnLoup()
    {
        GameObject Loup = null;

        foreach (GameObject predateur in ListeAnimaux.Defaut.Predateurs)
        {
            if(predateur.name == "Loup")
            {
                Loup = predateur;
                break;
            }
        }

        TuileManager[] tuilesFront = DamierGen.Actuel.RecupTuilesFrontalieres();

        TuileManager tuileSpawn = tuilesFront[Random.Range(0, tuilesFront.Length - 1)];

        if (Loup)
        {
            Hostile nvHostile = Instantiate(Loup, tuileSpawn.transform.position, new Quaternion()).GetComponent<Hostile>();
            int min;
            int max;

            Guerrier[] guerriers = FindObjectsOfType<Guerrier>();
            int ptAtt = 0;
            int ptDef = 0;

            for (int i = 0; i < guerriers.Length; i++)
            {
                ptAtt += guerriers[i].attaque;
                ptDef += guerriers[i].defense;
            }

            if (nvHostile.attaqueMin == -1) min = ptAtt / guerriers.Length;
            else min = nvHostile.attaqueMin;
            if (nvHostile.attaqueMax == -1) max = ptAtt / guerriers.Length + 1;
            else max = nvHostile.attaqueMax;

            nvHostile.attaque = Random.Range(min, max);

            if (nvHostile.defenseMin== -1) min = ptDef / guerriers.Length;
            else min = nvHostile.defenseMin;
            if (nvHostile.defenseMax == -1) max = ptDef / guerriers.Length + 1;
            else max = nvHostile.defenseMax;

            nvHostile.defense = Random.Range(min, max);


            Demographie[] demographies = FindObjectsOfType<Demographie>();
            int popTotale = 0;

            for (int i = 0; i < demographies.Length; i++)
            {
                popTotale += demographies[i].taillePopulation;
            }

            if (nvHostile.nbrCombattantMin == -1) min = popTotale / demographies.Length;
            else min = nvHostile.nbrCombattantMin;
            if (nvHostile.nbrCombattantMax == -1) max = popTotale / demographies.Length + 2;
            else max = nvHostile.nbrCombattantMax;

            nvHostile.nbrCombattant = Random.Range(min, max);
        }
        else Debug.LogError("Y a pas de Loup dans la liste des animaux !!!");
    }
}
