using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ProductionTuile : MonoBehaviour
{
    [SerializeField] private TuileManager tuile;

    public struct Production
    {
        public int slots;
        public float gainNourriture;
        public float gainPierre;
        public float gainPeau;
        public float gainPigment;

        public Production(int nbrSlots, float nourritureSlot, float pierreSlot, float peauSlot, float pigmentSot)
        {
            slots = nbrSlots;
            gainNourriture = nourritureSlot;
            gainPierre = pierreSlot;
            gainPeau = peauSlot;
            gainPigment = pigmentSot;
        }

        public static Production operator *(Production a, float b)
        {
            a.gainNourriture *= b;
            a.gainPierre *= b;
            a.gainPeau *= b;
            a.gainPierre *= b;

            return a;
        }

        public static Production operator /(Production a, float b)
        {
            a.gainNourriture /= b;
            a.gainPierre /= b;
            a.gainPeau /= b;
            a.gainPierre /= b;

            return a;
        }
    }

    public Production prod;

    // Start is called before the first frame update
    void Start()
    {
        prod = tuile.terrainTuile.Production;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
