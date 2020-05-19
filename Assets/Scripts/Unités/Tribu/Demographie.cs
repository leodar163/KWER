using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demographie : MonoBehaviour
{
    [SerializeField] private GameObject popPrefab;
    [SerializeField] private Tribu tribu;

    public List<Pop> listePopsCampement;
    public List<Pop> listePopsExpedition;

    public int taillePopulation
    {
        get
        {
            return listePopsExpedition.Count + listePopsCampement.Count;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        listePopsCampement = new List<Pop>(GetComponentsInChildren<Pop>());
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

        for (int i = 0; i < listePopsCampement.Count; i++)
        {
            float x = Mathf.Cos(index) * rayon;
            float y = Mathf.Sin(index) * rayon;

            Vector3 direction = new Vector3(x, y);

            listePopsCampement[i].transform.position = transform.position;
            listePopsCampement[i].transform.position += direction;

            index += Mathf.PI * 2 / listePopsCampement.Count;
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

        listePopsCampement.Add(nvPop.GetComponent<Pop>());

        AjusterRouePopulation();
    }

    public void AjouterPop(Pop popAjoutee)
    {
        if(listePopsExpedition.Contains(popAjoutee))
        {
            listePopsExpedition.Remove(popAjoutee);
            listePopsCampement.Add(popAjoutee);

            AjusterRouePopulation();
        }
    }

    public void RetirerPop()
    {

        Pop popRetiree = listePopsCampement[listePopsCampement.Count - 1];
        listePopsCampement.RemoveAt(listePopsCampement.Count - 1);
        Destroy(popRetiree.gameObject);

        AjusterRouePopulation();
    
    }

    public Pop RetirerPop( bool detruir)
    {
        Pop popRetiree = listePopsCampement[listePopsCampement.Count - 1];
        listePopsCampement.RemoveAt(listePopsCampement.Count - 1);

        if (detruir)
        {
            Destroy(popRetiree.gameObject);
            popRetiree = null;
        }
        else
        {
            listePopsExpedition.Add(popRetiree);
        }
        
        AjusterRouePopulation();

        return popRetiree;
        
    }

    public void AfficherIntefacePop(bool afficher)
    {
        gameObject.SetActive(afficher);
    }

}
