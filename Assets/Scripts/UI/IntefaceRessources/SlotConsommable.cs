using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotConsommable : MonoBehaviour, IPointerEnterHandler 
{
    [SerializeField] private GameObject iconeConsommable;
    private RectTransform rectTConso;
    private InfoBulle infoBulleConso;
    private Image imageConso;
    private Button boutonConso;

    private Consommable consommable;

    List<TuileManager> tuilesAmenageables = new List<TuileManager>();

    /// <summary>
    /// Assigne la variable consommable et initialise le slot
    /// </summary>
    public Consommable ConsommableAssigne
    {
        get
        {
            return consommable;
        }
        set
        {
            if(value == null)
            {
                DesactiverConsommable();
            }
            else
            {
                consommable = value;
                ActiverConsommable();
            }
        }
    }

    private bool dragNDrop = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTConso = iconeConsommable.GetComponent<RectTransform>();
        infoBulleConso = iconeConsommable.GetComponent<InfoBulle>();
        imageConso = iconeConsommable.GetComponent<Image>();
        boutonConso = iconeConsommable.GetComponent<Button>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(dragNDrop)
        {
            foreach(TuileManager tuile in tuilesAmenageables)
            {
                tuile.ColorerTuile(tuile.couleurTuileSurChemin);
            }
            rectTConso.position = Input.mousePosition;
            if(Input.GetMouseButtonUp(0))
            {
                ControleSouris.Actuel.controleEstActif = true;
                LayerMask maskTuile = LayerMask.GetMask("Tuile");

                Collider2D checkTuile = Physics2D.OverlapBox(Input.mousePosition, new Vector2(0.01f, 0.01f), 0, maskTuile);

                if(checkTuile)
                {
                    TuileManager tuile = checkTuile.GetComponent<TuileManager>();
                    if (tuilesAmenageables.Contains(tuile))
                    {
                        ConsommerConsommable();
                        consommable.amenagement.AmenagerTuile(tuile);
                    }
                }
            }
        }
    }

    private void ActiverConsommable()
    {
        iconeConsommable.SetActive(true);

        imageConso.sprite = consommable.icone;
        infoBulleConso.texteInfoBulle = consommable.TexteInfoBulle;

        boutonConso.onClick.RemoveAllListeners();
        if (consommable.type == Consommable.typeConsommable.amenagement)
        {
            boutonConso.onClick.AddListener(() => ControleSouris.Actuel.controleEstActif = false);
            boutonConso.onClick.AddListener(() => dragNDrop = tuilesAmenageables.Count > 0);
        }
        else if(consommable.type == Consommable.typeConsommable.buff)
        {
            boutonConso.onClick.AddListener(consommable.buff.activerBuff);
            boutonConso.onClick.AddListener(ConsommerConsommable);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TrouverTuilesAmenageables();
    }

    private void TrouverTuilesAmenageables()
    {
        string messageAlerte = "\n<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
            + ">Aucune tuile à portée ne peut être aménager<color=\"white\">";
        tuilesAmenageables.Clear();
        for (int i = 0; i < Tribu.TribukiJoue.revendication.tuilesRevendiquees.Count; i++)
        {
            if (!consommable.amenagement.terrainsAmenageables.Contains(Tribu.TribukiJoue.revendication.tuilesRevendiquees[i].tuile.terrainTuile)
                && Tribu.TribukiJoue.revendication.tuilesRevendiquees[i].tuile == Tribu.TribukiJoue.tuileActuelle)
            {
                tuilesAmenageables.Add(Tribu.TribukiJoue.revendication.tuilesRevendiquees[i].tuile);
            }
        }

        if (tuilesAmenageables.Count > 0)
        {
            if (infoBulleConso.texteInfoBulle.Contains(messageAlerte))
                infoBulleConso.texteInfoBulle = infoBulleConso.texteInfoBulle.Remove(infoBulleConso.texteInfoBulle.IndexOf(messageAlerte), messageAlerte.Length);
        }
        else
        {
            if (!infoBulleConso.texteInfoBulle.Contains(messageAlerte)) iconeConsommable.GetComponent<InfoBulle>().texteInfoBulle += messageAlerte;
        }
    }

    private void ConsommerConsommable()
    {
        Tribu.TribukiJoue.stockRessources.consommables.Remove(consommable);
        iconeConsommable.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        dragNDrop = false;
    }

    private void DesactiverConsommable()
    {
        iconeConsommable.SetActive(false);
    }


}
