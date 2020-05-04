using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revendication : MonoBehaviour
{

    public List<TuileManager> tuilesRevendiquees;
    public List<TuileManager> tuilesDisputees;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevendiquerTerritoire(TuileManager tuileActuelle, bool revendiquer)
    {
        tuilesRevendiquees.Clear();
        tuilesRevendiquees.Clear();

        RevendiquerTuile(tuileActuelle, revendiquer);

        foreach(TuileManager tuile in tuileActuelle.connections)
        {
            RevendiquerTuile(tuile, revendiquer);
        }
    }

    private void RevendiquerTuile(TuileManager tuile, bool revendiquer)
    {
        
        if(!tuile.estRevendiquee)
        {
            tuilesRevendiquees.Add(tuile);
            tuile.estRevendiquee = revendiquer;
            if(revendiquer)
            {
                tuile.revendicateur = this;
            }
            else
            {
                tuile.revendicateur = null;
            }
        }
        else
        {
            tuilesDisputees.Add(tuile);
        }
    }


    //revendiquer une tuile libre
    //dispuster une tuile déjà revendiquer
    //protéger une tuile revendiqué disputer par quelqu'un d'autre
}
