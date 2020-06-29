using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;  

[CreateAssetMenu(fileName = "NvCluster", menuName = "Evénements/Cluster")]
public class ClusterEvenement : ScriptableObject
{
    public List<EvenementProbable> listeEvenements = new List<EvenementProbable>();

    [Serializable]
    public struct EvenementProbable
    {
        [Range(0, 100)]
        public float proba;
        public Evenement evenement;
        [TextArea]
        public string descriptionRapide;
    }

    public void PiocherEvenement()
    {
        float aleaIactata = UnityEngine.Random.Range(0, 100f);
        float fond = 0;
        List<EvenementProbable> evenementsRetenus = new List<EvenementProbable>();

        if (aleaIactata == 0 && listeEvenements.Count > 0)
        {
            listeEvenements[0].evenement.LancerEvenement();
        }
        else if(aleaIactata == 100 && listeEvenements.Count > 0)
        {
            listeEvenements[listeEvenements.Count - 1].evenement.LancerEvenement();
        }
        for (int i = 0; i < listeEvenements.Count; i++)
        {
            
            if(aleaIactata <= listeEvenements[i].proba + fond && aleaIactata > fond)
            {
                listeEvenements[i].evenement.LancerEvenement();
                return;
            }
            fond += listeEvenements[i].proba;
        }
        Debug.LogError("Le cluster d'événement " + name + " n'a pas été rempli");
    }

    
}
