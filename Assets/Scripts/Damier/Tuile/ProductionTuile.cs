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
        public float nourritureParSlot;
        public float pierreParSlot;
        public float peauParSlot;
        public float pigmentParSlot;

        public Production(int nbrSlots, float nourritureSlot, float pierreSlot, float peauSlot, float pigmentSot)
        {
            slots = nbrSlots;
            nourritureParSlot = nourritureSlot;
            pierreParSlot = pierreSlot;
            peauParSlot = peauSlot;
            pigmentParSlot = pigmentSot;
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
