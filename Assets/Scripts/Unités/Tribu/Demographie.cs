using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Demographie : MonoBehaviour
{
    [SerializeField] private GameObject popParent;
    [SerializeField] private Tribu tribu;

    [Header("Interface")]
    [SerializeField] private GameObject affichage;
    [SerializeField] private Button boutonAjout;
    [SerializeField] private Button boutonSuppression;
    [SerializeField] private TextMeshProUGUI texteTotalPop;

    [Header("Couts et gains")]
    [SerializeField] private Production coutPop;
    private Production CoutPop
    {
        get
        {
            return coutPop * tribu.bonus.bonusMultCoutPop;
        }
        set
        {
            coutPop = value;
        }
    }
    [SerializeField] private Production GainSacrificePop;

    public List<Pop> listePopsCampement = new List<Pop>();
    public List<Pop> listePopsExpedition = new List<Pop>();
    public List<Pop> listePopsGuerrier = new List<Pop>();

    private bool modePopInfinie = false;

    public int taillePopulation
    {
        get
        {
            return listePopsExpedition.Count + listePopsCampement.Count + listePopsGuerrier.Count;
        }
    }

    void Start()
    {
        popParent.SetActive(false);
        AjouterPop();
        InstancierProduction();
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJBoutonsPop);
        InterfaceRessource.Actuel.EventInterfaceMAJ.AddListener(MAJTotalPop);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            modePopInfinie = !modePopInfinie;
            MAJBoutonsPop();
        }
    }

    private void MAJTotalPop()
    {
        texteTotalPop.text = "" + taillePopulation;
    }

    private void InstancierProduction()
    {
        Production nvCoutPop = ScriptableObject.CreateInstance<Production>();
        nvCoutPop.gains = (float[])CoutPop.gains.Clone();
        CoutPop = nvCoutPop;

        Production nvGainSacrificie = ScriptableObject.CreateInstance<Production>();
        nvGainSacrificie.gains = (float[])GainSacrificePop.gains.Clone();
        GainSacrificePop = nvGainSacrificie;
    }

    private void AjusterRouePopulation()
    {
        float rayon = 0.5f;

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
        if(!modePopInfinie)tribu.stockRessources.RessourcesEnStock -= CoutPop;

        GameObject nvPop = Instantiate(popParent, transform);
        nvPop.SetActive(true);
        listePopsCampement.Add(nvPop.GetComponent<Pop>());
        tribu.stockRessources.CalculerGain();
        tribu.stockRessources.AjouterCapacitePop();
        AjusterRouePopulation();
    }

    public void RetournerPop(Pop popAjoutee)
    {
        if(listePopsExpedition.Contains(popAjoutee))
        {
            listePopsExpedition.Remove(popAjoutee);
            listePopsCampement.Add(popAjoutee);

            AjusterRouePopulation();
        }
    }

    public Pop EngagerGuerrier()
    {
        if (listePopsCampement.Count > 0)
        {
            Pop popRetiree = listePopsCampement[listePopsCampement.Count - 1];
            listePopsCampement.RemoveAt(listePopsCampement.Count - 1);

            listePopsGuerrier.Add(popRetiree);
            tribu.stockRessources.CalculerGain();
            tribu.guerrier.nbrGuerrier++;

            AjusterRouePopulation();

            return popRetiree;
        }
        else return null;
    }

    public Pop DesengagerGuerrier(bool detruir)
    {
        Pop popRetiree = listePopsGuerrier[listePopsGuerrier.Count - 1];
        listePopsGuerrier.RemoveAt(listePopsGuerrier.Count - 1);

        if (detruir)
        {
            Destroy(popRetiree.gameObject);
            popRetiree = null;
            tribu.stockRessources.RetirerCapacitePop();
        }
        else
        {
            listePopsCampement.Add(popRetiree);
        }

        tribu.stockRessources.CalculerGain();
        tribu.guerrier.nbrGuerrier--;

        
        AjusterRouePopulation();

        return popRetiree;
    }

    public Pop RetirerPop( bool detruir)
    {
        Pop popRetiree = listePopsCampement[listePopsCampement.Count - 1];
        listePopsCampement.RemoveAt(listePopsCampement.Count - 1);

        if (detruir)
        {
            Destroy(popRetiree.gameObject);
            popRetiree = null;
            tribu.stockRessources.RetirerCapacitePop();
        }
        else
        {
            listePopsExpedition.Add(popRetiree);
        }

        tribu.stockRessources.CalculerGain();
        

        AjusterRouePopulation();

        return popRetiree;
    }


    public void SupprimerPop()
    {
        if (listePopsCampement.Count > 0)
        {
            Pop popRetiree = listePopsCampement[listePopsCampement.Count - 1];
            listePopsCampement.RemoveAt(listePopsCampement.Count - 1);
            Destroy(popRetiree.gameObject);
            tribu.stockRessources.RetirerCapacitePop();
        }   AjusterRouePopulation();
    }

    #region INTERFACE
    public void AfficherIntefacePop(bool afficher)
    {
        gameObject.SetActive(afficher);
    }

    private void MAJBoutonsPop()
    {
        MAJBoutonAjout();
        MAJBoutonSuppression();
    }

    private void MAJBoutonAjout()
    {
        InfoBulle infobulleBouton = boutonAjout.GetComponent<InfoBulle>();
        infobulleBouton.texteInfoBulle = "Augmenter Population";
        bool manqueRessource = false;
        
        if (modePopInfinie) boutonAjout.interactable = true;
        else
        {
            for (int i = 0; i < CoutPop.gains.Length; i++)
            {
                //écrit le coût dans l'infobulle
                if (CoutPop.gains[i] > 0)
                {
                    infobulleBouton.texteInfoBulle += "\n <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">-"
                        + CoutPop.gains[i] + "<color=\"white\"> " + ListeRessources.Defaut.listeDesRessources[i].nom;


                    if (tribu.stockRessources.RessourcesEnStock.gains[i] < CoutPop.gains[i])
                    {
                        manqueRessource = true;
                        infobulleBouton.texteInfoBulle += "<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + "> (insuffisant)";
                    }
                }
            }
            boutonAjout.interactable = !manqueRessource;
        }
    }

    private void MAJBoutonSuppression()
    {
        InfoBulle infoblleBouton = boutonSuppression.GetComponent<InfoBulle>();
        infoblleBouton.texteInfoBulle = "Sacrifier Population";
        if (modePopInfinie) boutonSuppression.interactable = true;
        else
        {
            for (int i = 0; i < GainSacrificePop.gains.Length; i++)
            {
                //écrit le gain dans l'infobulle
                if (GainSacrificePop.gains[i] > 0)
                {
                    infoblleBouton.texteInfoBulle += "\n <color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurTexteBonus) + ">+"
                        + CoutPop.gains[i] + "<color=\"white\"> " + ListeRessources.Defaut.listeDesRessources[i].nom;

                }
            }

            if (taillePopulation == 1)
            {
                boutonSuppression.interactable = false;
                infoblleBouton.texteInfoBulle += "\n<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                    + "Pas assez de population pour en sacrifier" + "<color=\"white\"> " ;
            }
            else if (listePopsCampement.Count == 0)
            {
                boutonSuppression.interactable = false;
                infoblleBouton.texteInfoBulle += "\n<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface) + ">"
                    + "Impossible de sacrifier des populations occupées" + "<color=\"white\"> ";
            }
            else boutonSuppression.interactable = true;
        }
    }
    #endregion
}
