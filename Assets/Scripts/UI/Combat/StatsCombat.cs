﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Http.Headers;

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


    private List<GameObject> listePointsAttaque = new List<GameObject>();
    private List<GameObject> listePointsDefense = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        pointAttaque.gameObject.SetActive(false);
        pointDefense.gameObject.SetActive(false);
    }

    public void MAJStats(int nbrCombattant, int nbrPtAttaque, int nbrPtDefense)
    {
        if (nbrCombattant != int.Parse(nombreCombattant.text)) nombreCombattant.text = "" + nbrCombattant;

        AjouterPointAttaque(nbrPtAttaque - listePointsAttaque.Count);
        AjouterPointDefense(nbrPtDefense - listePointsDefense.Count);
    }

    private void AjouterPointAttaque(int nbrPoint)
    {
        if(nbrPoint > 0)
        {
            GameObject nvPoint = Instantiate(pointAttaque, zonePointsAttaque.transform);
            nvPoint.SetActive(true);
            listePointsAttaque.Add(nvPoint);
        }
        else if(nbrPoint < 0)
        {
            Destroy(listePointsAttaque[listePointsAttaque.Count - 1]);
            listePointsAttaque.RemoveAt(listePointsAttaque.Count - 1);
        }
    }

    private void AjouterPointDefense(int nbrPoint)
    {
        if (nbrPoint > 0)
        {
            GameObject nvPoint = Instantiate(pointDefense, zonePointsDefense.transform);
            nvPoint.SetActive(true);
            listePointsDefense.Add(nvPoint);
        }
        else if (nbrPoint < 0)
        {
            Destroy(listePointsDefense[listePointsDefense.Count - 1]);
            listePointsDefense.RemoveAt(listePointsDefense.Count - 1);
        }
    }

}
