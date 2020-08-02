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
    [SerializeField] private InfoBulle infobulle;
    private string texteInfoBulleDefaut;
    private RectTransform rectTConso;
    private Image imageConso;
    private Button boutonConso;

    private bool controlesActifs = false;

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
                ActiverSlotConsommable();
            }
        }
    }

    private bool dragNDrop = false;

    private void Awake()
    {
        texteInfoBulleDefaut = infobulle.texteInfoBulle;
        rectTConso = iconeConsommable.GetComponent<RectTransform>();
        imageConso = iconeConsommable.GetComponent<Image>();
        boutonConso = iconeConsommable.GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
            if(Input.GetMouseButtonUp(0) && controlesActifs)
            {
                ControleSouris.Actuel.controleEstActif = true;
                LayerMask maskTuile = LayerMask.GetMask("Tuile");

                Collider2D checkTuile = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector2(0.01f, 0.01f), 0, maskTuile);

                if(checkTuile)
                {
                    TuileManager tuile = checkTuile.GetComponent<TuileManager>();
                    if (tuilesAmenageables.Contains(tuile))
                    {
                        consommable.amenagement.AmenagerTuile(tuile);
                        ConsommerConsommable();
                        return;
                    }
                }
                ReinitConso();
            }
            if(dragNDrop)controlesActifs = true;
        }
    }

    private void ActiverSlotConsommable()
    {
        iconeConsommable.SetActive(true);

        imageConso.sprite = consommable.icone;
        infobulle.texteInfoBulle = consommable.TexteInfoBulle;

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
        if(consommable && consommable.amenagement)TrouverTuilesAmenageables();
    }

    private void TrouverTuilesAmenageables()
    {
        string messageAlerte = "\n<color=#" + ColorUtility.ToHtmlStringRGBA(ListeCouleurs.Defaut.couleurAlerteTexteInterface)
            + ">Aucune tuile à portée ne peut être aménager<color=\"white\">";
        tuilesAmenageables.Clear();
        for (int i = 0; i < Tribu.TribukiJoue.revendication.tuilesRevendiquees.Count; i++)
        {
            TuileManager tuile = Tribu.TribukiJoue.revendication.tuilesRevendiquees[i].tuile;

            if (consommable.amenagement.terrainsAmenageables.Contains(tuile.terrainTuile.nom))
            {
                if (tuile != Tribu.TribukiJoue.tuileActuelle)
                {
                    
                    if ((tuile.tuileAmenagement.Amenagement == consommable.amenagement && !tuile.tuileAmenagement.amenagementEstActif)
                        || tuile.tuileAmenagement.Amenagement == null)
                    {

                        tuilesAmenageables.Add(Tribu.TribukiJoue.revendication.tuilesRevendiquees[i].tuile);
                    }
                }
                
            }
        }

        if (tuilesAmenageables.Count > 0)
        {
            if (infobulle.texteInfoBulle.Contains(messageAlerte))
                infobulle.texteInfoBulle = infobulle.texteInfoBulle.Remove(infobulle.texteInfoBulle.IndexOf(messageAlerte), messageAlerte.Length);
        }
        else
        {
            if (!infobulle.texteInfoBulle.Contains(messageAlerte)) iconeConsommable.GetComponent<InfoBulle>().texteInfoBulle += messageAlerte;
        }
    }

    private void ReinitConso()
    {
        foreach (TuileManager tuile in tuilesAmenageables)
        {
            tuile.ColorerTuile(Color.white);
        }
        rectTConso.position = GetComponent<RectTransform>().position;
        dragNDrop = false;
        controlesActifs = false;
    }
    private void ConsommerConsommable()
    {
        
        Tribu.TribukiJoue.stockRessources.consommables.Remove(consommable);
        ReinitConso();
    }

    private void DesactiverConsommable()
    {
        iconeConsommable.SetActive(false);
        infobulle.texteInfoBulle = texteInfoBulleDefaut;
    }


}
