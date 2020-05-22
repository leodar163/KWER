using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campement : MonoBehaviour
{
    public Tribu tribu;
    [SerializeField] private Craft craft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherInterfaceCampement(bool afficher)
    {
        gameObject.SetActive(afficher);
        craft.AfficherInterfaceCraft(afficher);
    }

    public void MonterCampement()
    {
        craft.GenererPanelsRecette();
    }

}
