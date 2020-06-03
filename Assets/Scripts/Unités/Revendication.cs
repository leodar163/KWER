using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Revendication : MonoBehaviour
{

    public MonoBehaviour parent;
    private List<RevendicationTuile> tuilesRevendiquees = new List<RevendicationTuile>();

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
        RevendiquerTuile(tuileActuelle, revendiquer);

        foreach (TuileManager tuile in tuileActuelle.connections)
        {
            RevendiquerTuile(tuile, revendiquer);
        }
    }

    private void RevendiquerTuile(TuileManager tuile, bool revendiquer)
    {
        if (revendiquer)
        {
            if (!tuile.revendication.revendicateurs.Contains(this))
            {
                tuile.revendication.revendicateurs.Add(this);
                tuilesRevendiquees.Add(tuile.revendication);
            }
        }
        else
        {
            if (tuile.revendication.revendicateurs.Contains(this))
            {
                tuile.revendication.revendicateurs.Remove(this);
                tuilesRevendiquees.Remove(tuile.revendication);
            }
        }
    }

    public bool EstAnimal
    {
        get
        {
            if (parent is Troupeau)
            {
                return true;
            }
            else return false;
        }

    }

    public bool EstTribu
    {
        get
        {
            if (parent is Tribu)
            {
                return true;
            }
            else return false;
        }
    }

    public bool EstPillard
    {
        get
        {
            if (parent is Pillard)
            {
                return true;
            }
            else return false;
        }
    }

    public bool EstPredateur
    {
        get
        {
            if (EstAnimal)
            {
                Troupeau animal = (Troupeau)parent;
                if (animal.predateur)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }

    public bool EstDomesticable
    {
        get
        {
            if (EstAnimal)
            {
                Troupeau animal = (Troupeau)parent;
                if (animal.domesticable)
                {
                    return true;
                }
                else return false;

            }
            else return false;
        }
    }

    public bool EstMegaFaune
    {
        get
        {
            if (EstAnimal)
            {
                Troupeau animal = (Troupeau)parent;
                if (animal.megaFaune)
                {
                    return true;
                }
                else return false;

            }
            else return false;
        }
    }

    public List<Revendication> TrouverRevendicateursAPortee()
    {
        List<Revendication> revendicateurs = new List<Revendication>();

        foreach(RevendicationTuile tuile in tuilesRevendiquees)
        {
            foreach(Revendication revendicateur in tuile.revendicateurs)
            {
                print(revendicateur);
                if(!revendicateurs.Contains(revendicateur))
                {
                    revendicateurs.Add(revendicateur);
                }
            }
        }
        return revendicateurs;
    }
}
