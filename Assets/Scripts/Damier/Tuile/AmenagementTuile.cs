using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Schema;
using UnityEngine;

public class AmenagementTuile : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRend;

    private int paliersValides = 0;
    [HideInInspector] public bool amenagementEstActif;

    private Amenagement amenagement;

    public Amenagement Amenagement
    {
        get { return amenagement; }
    }

    public int slotsAmenagement
    {
        get
        {
            if (amenagementEstActif) return Amenagement.Slots;
            return 0;
        }
    }

    public Production gainAmenagement
    {
        get
        {
            if (amenagementEstActif)
            {
                if (Calendrier.Actuel && Calendrier.Actuel.Hiver) return amenagement.gainAmenagementHiver;
                else return amenagement.gainAmenagementEte;
            }
            else
            {
                Production Prod = ScriptableObject.CreateInstance<Production>();
                Prod.Initialiser();
                return Prod;
            } 

        }
    }

    public void AmenagerTuile(Amenagement nvlAmenagement)
    {
        if (!amenagement)
        {
            amenagement = nvlAmenagement;
            ValiderPalier();
        }
        else if (nvlAmenagement == amenagement)
        {
            ValiderPalier();
        }

        Amenagement.eventAmenagement.Invoke();
    }

    private void ValiderPalier()
    {
        if (paliersValides < Amenagement.palier) paliersValides++;

        RevetirSpriteSaison();

        if (paliersValides == Amenagement.palier) amenagementEstActif = true;
    }

    public void RevetirSpriteSaison()
    {
        if(amenagement)
        {
            if (Calendrier.Actuel.Hiver) spriteRend.sprite = amenagement.spritesHiver[paliersValides - 1];
            else spriteRend.sprite = amenagement.spritesEte[paliersValides - 1];
        }
    }
}
