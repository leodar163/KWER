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

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        cela = this;

        compteur = GetComponentInChildren<TextMeshProUGUI>();

        MiseAJourCalendrier(0);
    }
    void Update()
    {

    }

    public void MiseAJourCalendrier(int nbrTour)
    {
        StartCoroutine(MettreCalendrierAJour(nbrTour));
    }

    private IEnumerator MettreCalendrierAJour(int nbrTour)
    {
        float difference = roueCalendrier.eulerAngles.z + 180 / dureeSaison;
        Vector3 rotationCible = roueCalendrier.eulerAngles;
        rotationCible.z += 180 / dureeSaison;
        while (!(roueCalendrier.eulerAngles.z > rotationCible.z -1 && roueCalendrier.eulerAngles.z < rotationCible.z + 1)) 
        {
            roueCalendrier.eulerAngles = Vector3.Lerp(roueCalendrier.eulerAngles, rotationCible, 4f * Time.deltaTime);
            if (roueCalendrier.eulerAngles.z > 355)
            {
                rotationCible.z -= 360;
                roueCalendrier.eulerAngles = new Vector3();
            }
            


            yield return new WaitForEndOfFrame();
        }

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
