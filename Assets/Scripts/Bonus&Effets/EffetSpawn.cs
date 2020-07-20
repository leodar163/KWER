using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffetSpawn : MonoBehaviour
{
    [SerializeField] private GameObject pillard;
    public void SpawnPillard()
    {
        TuileManager[] tuilesFront = DamierGen.Actuel.RecupTuilesFrontalieres();

        List<TuileManager> piocheTuile = new List<TuileManager>();

        foreach (TuileManager tuile in tuilesFront)
        {
            if (!tuile.estOccupee) piocheTuile.Add(tuile);
        }

        TuileManager tuileSpawn = tuilesFront[Random.Range(0, piocheTuile.Count - 1)];

        Vector3 positionSpawn = tuileSpawn.transform.position;
        positionSpawn.z = -3;

        Pillard nvPillard = Instantiate(pillard, positionSpawn, new Quaternion()).GetComponent<Pillard>();

        Hostile pillardHost = nvPillard.GetComponent<Hostile>();
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

        if (pillardHost.attaqueMin == -1) min = ptAtt / guerriers.Length;
        else min = pillardHost.attaqueMin;
        if (pillardHost.attaqueMax == -1) max = ptAtt / guerriers.Length + 2;
        else max = pillardHost.attaqueMax;

        pillardHost.attaque = Random.Range(min, max);

        if (pillardHost.defenseMin == -1) min = ptDef / guerriers.Length;
        else min = pillardHost.defenseMin;
        if (pillardHost.defenseMax == -1) max = ptDef / guerriers.Length + 1;
        else max = pillardHost.defenseMax;

        pillardHost.defense = Random.Range(min, max);

        Tribu[] tribus = FindObjectsOfType<Tribu>();
        List<Demographie> demographies = new List<Demographie>();

        foreach (Tribu tribu in tribus)
        {
            demographies.Add(tribu.demographie);
        }

        int popTotale = 0;

        for (int i = 0; i < demographies.Count; i++)
        {
            popTotale += demographies[i].taillePopulation;
        }

        if (pillardHost.nbrCombattantMin == -1) min = popTotale / demographies.Count;
        else min = pillardHost.nbrCombattantMin;
        if (pillardHost.nbrCombattantMax == -1) max = popTotale / demographies.Count + 2;
        else max = pillardHost.nbrCombattantMax;

        pillardHost.nbrCombattant = Random.Range(min, max);

        CameraControle.Actuel.CentrerCamera(nvPillard.transform.position);
    }

    public void SpawnTroupeau()
    {
        GameObject troupeauASpawn = ListeAnimaux.Defaut.domesticables[Random.Range(0, ListeAnimaux.Defaut.domesticables.Count - 1)];

        TuileManager[] tuilesFront = DamierGen.Actuel.RecupTuilesFrontalieres();
        List<TuileManager> tuilesPlaine = new List<TuileManager>();

        for (int i = 0; i < tuilesFront.Length; i++)
        {
            if(tuilesFront[i].terrainTuile is TerrainPlaine && !tuilesFront[i].estOccupee)
            {
                tuilesPlaine.Add(tuilesFront[i]);
            }
        }

        TuileManager tuileSpawn = tuilesPlaine[Random.Range(0, tuilesPlaine.Count - 1)];

        Vector3 positionSpawn = tuileSpawn.transform.position;
        positionSpawn.z = -3;

        GameObject nvTroupeau = Instantiate(troupeauASpawn, positionSpawn, new Quaternion());

        CameraControle.Actuel.CentrerCamera(nvTroupeau.transform.position);
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

        List<TuileManager> piocheTuile = new List<TuileManager>();

        foreach (TuileManager tuile in tuilesFront)
        {
            if (!tuile.estOccupee) piocheTuile.Add(tuile);
        }

        TuileManager tuileSpawn = tuilesFront[Random.Range(0, piocheTuile.Count - 1)];

        if (Loup)
        {
            Vector3 positionSpawn = tuileSpawn.transform.position;
            positionSpawn.z = -3;

            Hostile nvHostile = Instantiate(Loup, positionSpawn, new Quaternion()).GetComponent<Hostile>();

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

            Tribu[] tribus = FindObjectsOfType<Tribu>();
            List<Demographie> demographies = new List<Demographie>();

            foreach (Tribu tribu in tribus)
            {
                demographies.Add(tribu.demographie);
            }

            int popTotale = 0;

            for (int i = 0; i < demographies.Count; i++)
            {
                popTotale += demographies[i].taillePopulation;
            }

            if (nvHostile.nbrCombattantMin == -1) min = popTotale / demographies.Count;
            else min = nvHostile.nbrCombattantMin;
            if (nvHostile.nbrCombattantMax == -1) max = popTotale / demographies.Count + 2;
            else max = nvHostile.nbrCombattantMax;

            nvHostile.nbrCombattant = Random.Range(min, max);

            CameraControle.Actuel.CentrerCamera(nvHostile.transform.position);
        }
        else Debug.LogError("Y a pas de Loup dans la liste des animaux !!!");
    }
}
