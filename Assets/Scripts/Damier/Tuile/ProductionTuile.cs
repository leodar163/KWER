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
            a.gainPigment *= b;

            return a;
        }

        public static Production operator /(Production a, float b)
        {
            a.gainNourriture /= b;
            a.gainPierre /= b;
            a.gainPeau /= b;
            a.gainPigment /= b;

            return a;
        }

        public static Production operator +(Production a, Production b)
        {
            a.slots += b.slots;
            a.gainNourriture += b.gainNourriture;
            a.gainPierre += b.gainPierre;
            a.gainPeau += b.gainPeau;
            a.gainPigment += b.gainPigment;

            return a;
        }
        public static Production operator -(Production a, Production b)
        {
            a.slots -= b.slots;
            a.gainNourriture -= b.gainNourriture;
            a.gainPierre -= b.gainPierre;
            a.gainPeau -= b.gainPeau;
            a.gainPigment -= b.gainPigment;

            return a;
        }
    }

    public Production prod;

    private void Awake()
    {
        prod = tuile.terrainTuile.Production;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ReinitProd()
    {
        prod = tuile.terrainTuile.Production;
    }
}
