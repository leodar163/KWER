using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demographie : MonoBehaviour
{
    [SerializeField] private GameObject popPrefab;
    [SerializeField] private Tribu tribu;

    public List<Pop> listePops;

    // Start is called before the first frame update
    void Start()
    {
        listePops = new List<Pop>(GetComponentsInChildren<Pop>());
        AjouterPop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AjusterRouePopulation()
    {
        float rayon = 0.6f;

        float index = 0;

        for (int i = 0; i < listePops.Count; i++)
        {
            float x = Mathf.Cos(index) * rayon;
            float y = Mathf.Sin(index) * rayon;

            Vector3 direction = new Vector3(x, y);

            listePops[i].transform.position = transform.position;
            listePops[i].transform.position += direction;

            index += Mathf.PI * 2 / listePops.Count;
        }
    }

    public void AjouterPlusieursPop(int nbrPop)
    {
        for (int i = 0; i < nbrPop; i++)
        {
            AjouterPop();
        }

        
    }

    public void AjouterPop()
    {
        GameObject nvPop = Instantiate(popPrefab, transform);

        listePops.Add(nvPop.GetComponent<Pop>());

        AjusterRouePopulation();
    }

    public void SacrifierPop()
    {
        if(listePops.Count > 1)
        {
            Pop popSacrfiee = listePops[listePops.Count - 1];
            listePops.RemoveAt(listePops.Count - 1);
            Destroy(popSacrfiee.gameObject);
        }
        AjusterRouePopulation();
    }

    public void AfficherIntefacePop(bool afficher)
    {
        gameObject.SetActive(afficher);
    }

}
