using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Classe dont l'unique instance est celle de l'objet "FenetreValidation"
/// </summary>
public class FenetreValidation : MonoBehaviour
{
    static private FenetreValidation cela;

    [Header("Description")]
    [SerializeField] private TextMeshProUGUI description;
    [Header("Icone")]
    [SerializeField] private Image icone;
    [SerializeField] private InfoBulle infobulleIcone;
    [Space]
    [Header("Boutons")]
    [SerializeField] private Button boutonValider;
    [SerializeField] private Button boutonAnnuler;
    [SerializeField] private TextMeshProUGUI texteBoutonValider;
    [SerializeField] private TextMeshProUGUI texteBoutonAnnuler;


    // Start is called before the first frame update
    void Start()
    {
        cela = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Ouvre la fenêtre de validation
    /// </summary>
    /// <param name="texteDescritpion">Texte explicatif ou question à propos de l'action à valider</param>
    /// <param name="texteValid">Texte à marquer dans le bouton de validation</param>
    /// <param name="texteAnnul">Texte à marquer dans le bouton d'annulation</param>
    /// <param name="appelValidation">Fonction à appeler si validation</param>
    /// <param name="sprIcone">Illustration affichée en haut de la fenêtre</param>
    /// <param name="texteInfobulleIcone">Texte de l'infobulle de l'icone</param>
    static public void OuvrirFenetreValidation(string texteDescritpion, string texteValid, string texteAnnul, 
        UnityAction appelValidation, Sprite sprIcone, string texteInfobulleIcone)
    {
        cela.gameObject.SetActive(true);
        ReinitBoutons();
        cela.description.text = texteDescritpion;
        cela.boutonValider.onClick.AddListener(appelValidation);
        cela.texteBoutonValider.text = texteValid;
        cela.texteBoutonAnnuler.text = texteAnnul;
        cela.icone.enabled = true;
        cela.icone.sprite = sprIcone;
        cela.infobulleIcone.texteInfoBulle = texteInfobulleIcone;
    }

    /// <summary>
    /// Ouvre la fenêtre de validation
    /// </summary>
    /// <param name="texteDescritpion">Texte explicatif ou question à propos de l'action à valider</param>
    /// <param name="texteValid">Texte à marquer dans le bouton de validation</param>
    /// <param name="texteAnnul">Texte à marquer dans le bouton d'annulation</param>
    /// <param name="appelValidation">Fonction à appeler si validation</param>
    static public void OuvrirFenetreValidation(string texteDescritpion, string texteValid, string texteAnnul,
        UnityAction appelValidation)
    {
        cela.gameObject.SetActive(true);
        ReinitBoutons();
        cela.description.text = texteDescritpion;
        cela.boutonValider.onClick.AddListener(appelValidation);
        cela.texteBoutonValider.text = texteValid;
        cela.texteBoutonAnnuler.text = texteAnnul;
        cela.icone.enabled = false;
        cela.infobulleIcone.enabled = false;
    }

    /// <summary>
    /// Ouvre la fenêtre de validation
    /// </summary>
    /// <param name="texteDescritpion">Texte explicatif ou question à propos de l'action à valider</param>
    /// <param name="appelValidation">Fonction à appeler si validation</param>
    static public void OuvrirFenetreValidation(string texteDescritpion, UnityAction appelValidation)
    {
        cela.gameObject.SetActive(true);
        ReinitBoutons();
        cela.description.text = texteDescritpion;
        cela.boutonValider.onClick.AddListener(appelValidation);
        cela.texteBoutonValider.text = "Valider";
        cela.texteBoutonAnnuler.text = "Annuler";
        cela.icone.enabled = false;
        cela.infobulleIcone.enabled = false;
    }

    private static void ReinitBoutons()
    {
        cela.gameObject.SetActive(true);
        cela.infobulleIcone.enabled = true;
        cela.boutonAnnuler.onClick.RemoveAllListeners();
        cela.boutonValider.onClick.RemoveAllListeners();
        cela.boutonAnnuler.onClick.AddListener(() => cela.gameObject.SetActive(false));
        cela.boutonValider.onClick.AddListener(() => cela.gameObject.SetActive(false));
    }
}
