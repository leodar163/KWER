using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Events;
using System.Linq.Expressions;
using System;

public class Calendrier : MonoBehaviour
{
    #region SINGLETON
    private static Calendrier cela;
    public static Calendrier Actuel
    {
        get
        {
            if(cela == null)
            {
                cela = FindObjectOfType<Calendrier>();
            }
            return cela;
        }
    }
    #endregion

    TextMeshProUGUI compteur;
    [SerializeField] int dureeSaison;
    private bool hiver = false;
    public bool Hiver
    {
        get
        {
            return hiver;
        }
    }
    [SerializeField] private RectTransform roueCalendrier;
    public UnityEvent EventChangementDeSaison;

    [SerializeField] private float vitesseRotation = 60f;
    private float differenceRotation;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        cela = this;

        differenceRotation = roueCalendrier.localEulerAngles.z;
        compteur = GetComponentInChildren<TextMeshProUGUI>();

        MiseAJourCalendrier(0);
    }
    void Update()
    {
        Rotater();   
    }

    private void Rotater()
    {
        if(!(Mathf.Clamp(roueCalendrier.localEulerAngles.z, differenceRotation - 1, differenceRotation + 1) == roueCalendrier.localEulerAngles.z))
        {
            roueCalendrier.localEulerAngles += new Vector3(0, 0,  vitesseRotation* Time.deltaTime);
        }   
    }

    public void MiseAJourCalendrier(int nbrTour)
    {
        StartCoroutine(MettreCalendrierAJour(nbrTour));
    }

    private IEnumerator MettreCalendrierAJour(int nbrTour)
    {
        differenceRotation += 180 / dureeSaison;
        if (differenceRotation > 360) differenceRotation -= 360;

        yield return new WaitUntil(() => Mathf.Clamp(roueCalendrier.localEulerAngles.z, differenceRotation - 1, differenceRotation + 1) == roueCalendrier.localEulerAngles.z);

        MiseAJourCompteurCalendrier(nbrTour);
    }

    private void MiseAJourCompteurCalendrier(int nbrTour)
    {
        int decompte = dureeSaison - nbrTour % dureeSaison;
        compteur.text = nbrTour.ToString();

        //On commence à vraiment faire les modif' qu'à partir du premier tour;
        if(nbrTour != 0)
        {
            if (decompte == 4)
            {
                hiver = !hiver;
                EventChangementDeSaison.Invoke();
            }
            TourParTour.Defaut.calendrierMAJ = true;
        }
    }

}
