using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Revendication))]
public class Hostile : MonoBehaviour
{
    TuileManager tuileActuelle;
    Revendication revendication;

    //Gestion tour par tour
    public bool peutAttaquer = false;

    // Start is called before the first frame update
    void Start()
    {
        revendication = GetComponent<Revendication>();

        TrouverTuileActuelle();

        revendication.RevendiquerTerritoire(tuileActuelle, true);
    }

    private void TrouverTuileActuelle()
    {
        LayerMask layerMaskTuile = LayerMask.GetMask("Tuile");

        Collider2D checkTuile = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0, layerMaskTuile);

        if (checkTuile)
        {
            tuileActuelle = checkTuile.gameObject.GetComponent<TuileManager>();
            transform.position = new Vector3(tuileActuelle.transform.position.x, tuileActuelle.transform.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PasserTour()
    {
        if(revendication.tuilesDisputees.Count > 0)
        {
            peutAttaquer = true;
            for (int i = 0; i < revendication.tuilesDisputees.Count; i++)
            {
                Attaquer(revendication.tuilesDisputees[i].revendicateur);
            }
        }
        peutAttaquer = false;
    }

    private void Attaquer(Revendication revendicateur)
    {
        print(gameObject.name + " attaque " + revendicateur.gameObject.name);
    }
}
