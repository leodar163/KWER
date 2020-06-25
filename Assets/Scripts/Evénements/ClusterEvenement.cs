using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

[CreateAssetMenu(fileName = "NvCluster", menuName = "Evénements/Cluster")]
public class ClusterEvenement : ScriptableObject
{
    [SerializeField] private List<EvenementProbable> listeEvenement = new List<EvenementProbable>();

    [Serializable]
    private struct EvenementProbable
    {
        [Range(0, 100)]
        public float proba;
        public Evenement evenement;
        [TextArea]
        public string descriptionRapide;
    }

    public void PiocherEvenement()
    {
        float aleaIactata = UnityEngine.Random.Range(0, 100);

        List<EvenementProbable> evenementsRetenus = new List<EvenementProbable>();

        foreach (EvenementProbable evenement in listeEvenement)
        {
            if (evenement.proba >= aleaIactata)
            {
                evenementsRetenus.Add(evenement);
            }
        }

        if(evenementsRetenus.Count > 0)
        {
            int aleumJactatum = UnityEngine.Random.Range(0, evenementsRetenus.Count - 1);
            evenementsRetenus[aleumJactatum].evenement.LancerEvenement();
        }

    }

    
}
