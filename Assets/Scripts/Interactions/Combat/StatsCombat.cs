using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StatsCombat : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI nombreCombattant;
    [Space]
    [Space]
    [SerializeField] private GameObject zonePointsAttaque;
    [SerializeField] private GameObject pointAttaque;
    [Space]
    [SerializeField] private GameObject zonePointsDefense;
    [SerializeField] private GameObject pointDefense;
    [Space]
    [SerializeField] private GameObject pointBonus;

    private List<GameObject> listePointsAttaque = new List<GameObject>();
    private List<GameObject> listePointsDefense = new List<GameObject>();
    private List<GameObject> listeBonusAttaque = new List<GameObject>();
    private List<GameObject> listeBonusDefense = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        pointAttaque.SetActive(false);
        pointDefense.SetActive(false);
        pointBonus.SetActive(false);
    }

    public void MAJStats(int nbrCombattant, int nbrPtAttaque, int nbrPtDefense)
    {
        if (nbrCombattant != int.Parse(nombreCombattant.text)) nombreCombattant.text = "" + nbrCombattant;
        AjouterPointAttaque(nbrPtAttaque - listePointsAttaque.Count);
        AjouterPointDefense(nbrPtDefense - listePointsDefense.Count);
    }

    public void MAJStats(Hostile hostile)
    {
        if (hostile.nbrCombattant != int.Parse(nombreCombattant.text)) nombreCombattant.text = "" + hostile.nbrCombattant;
        AjouterPointAttaque(hostile.attaque - listePointsAttaque.Count);
        AjouterPointDefense(hostile.defense - listePointsDefense.Count);
    }

    public void MAJStats(Guerrier guerrier)
    {
        if (guerrier.nbrGuerrier != int.Parse(nombreCombattant.text)) nombreCombattant.text = "" + guerrier.nbrGuerrier;
        AjouterPointAttaque(guerrier.attaque - listePointsAttaque.Count);
        AjouterPointDefense(guerrier.defense - listePointsDefense.Count);
        AjouterBonusAttaque((guerrier.attaqueTotale - guerrier.attaque) - listeBonusAttaque.Count);
        AjouterBonusDefense((guerrier.defenseTotale - guerrier.defense) - listeBonusDefense.Count);
    }

    private void AjouterPointAttaque(int nbrPoint)
    {
        if(nbrPoint > 0)
        {
            for (int i = 0; i < nbrPoint; i++)
            {
                GameObject nvPoint = Instantiate(pointAttaque, zonePointsAttaque.transform);
                nvPoint.SetActive(true);
                listePointsAttaque.Add(nvPoint);
            }
        }
        else if(nbrPoint < 0)
        {
            for (int i = 0; i < Mathf.Abs(nbrPoint); i++)
            {
                Destroy(listePointsAttaque[listePointsAttaque.Count - 1]);
                listePointsAttaque.RemoveAt(listePointsAttaque.Count - 1);
            }
        }
    }

    private void AjouterPointDefense(int nbrPoint)
    {
        
        if (nbrPoint > 0)
        {
            for (int i = 0; i < nbrPoint; i++)
            {
                GameObject nvPoint = Instantiate(pointDefense, zonePointsDefense.transform);
                nvPoint.SetActive(true);
                listePointsDefense.Add(nvPoint);
            }
        }
        else if (nbrPoint < 0)
        {
            for (int i = 0; i < Mathf.Abs(nbrPoint); i++)
            {
                Destroy(listePointsDefense[listePointsDefense.Count - 1]);
                listePointsDefense.RemoveAt(listePointsDefense.Count - 1);
            }
        }
    }

    private void AjouterBonusAttaque(int nbrPoint)
    {
        if (nbrPoint > 0)
        {
            for (int i = 0; i < nbrPoint; i++)
            {
                GameObject nvPoint = Instantiate(pointBonus, zonePointsAttaque.transform);
                nvPoint.SetActive(true);
                listeBonusAttaque.Add(nvPoint);
            }
        }
        else if (nbrPoint < 0)
        {
            for (int i = 0; i < Mathf.Abs(nbrPoint); i++)
            {
                Destroy(listeBonusAttaque[listeBonusAttaque.Count - 1]);
                listeBonusAttaque.RemoveAt(listeBonusAttaque.Count - 1);
            }
        }
    }

    private void AjouterBonusDefense(int nbrPoint)
    {
        if (nbrPoint > 0)
        {
            for (int i = 0; i < nbrPoint; i++)
            {
                GameObject nvPoint = Instantiate(pointBonus, zonePointsDefense.transform);
                nvPoint.SetActive(true);
                listeBonusDefense.Add(nvPoint);
            }
        }
        else if (nbrPoint < 0)
        {
            for (int i = 0; i < Mathf.Abs(nbrPoint); i++)
            {
                Destroy(listeBonusDefense[listeBonusDefense.Count - 1]);
                listeBonusDefense.RemoveAt(listeBonusDefense.Count - 1);
            }
        }
    }
}
