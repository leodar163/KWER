using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceInfoBulle : MonoBehaviour
{
    private TextMeshProUGUI texteMP;
    private RectTransform rectT;
    [SerializeField] private Image image;
    private float alphaDefaut;

    private static InterfaceInfoBulle cela;

    public static InterfaceInfoBulle Actuel
    {
        get
        {
            if(!cela)
            {
                cela = FindObjectOfType<InterfaceInfoBulle>();
            }
            return cela;
        }
    }

    private void Awake()
    {
        texteMP = GetComponentInChildren<TextMeshProUGUI>();
        rectT = GetComponent<RectTransform>();
        if(!image)image = GetComponentInChildren<Image>();
        alphaDefaut = image.color.a;
        CacherBulle();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SuivreSouris();
    }



    private void SuivreSouris()
    {
        MAJPivot();

        rectT.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y, 0);
    }

    private void MAJPivot()
    {
        Resolution resol = Screen.currentResolution;
        Vector2 nvPivot = new Vector2();

        if (rectT.position.x < resol.width / 2)
        {
            nvPivot.x = 0;
        }
        else
        {
            nvPivot.x = 1;
        }

        if (rectT.position.y < resol.height / 2)
        {
            nvPivot.y = 0;
        }
        else
        {
            nvPivot.y = 1;
        }

        rectT.pivot = nvPivot;
    }

    public void AfficherBulle(string texte)
    {
        texteMP.text = texte;

        Color plusdalpha = texteMP.color;
        plusdalpha.a = 1;
        texteMP.color = plusdalpha;

        plusdalpha = image.color;
        plusdalpha.a = alphaDefaut;
        image.color = plusdalpha;
    }

    public void CacherBulle()
    {
        Color plusdalpha = texteMP.color;
        plusdalpha.a = 0;
        texteMP.color = plusdalpha;

        plusdalpha = image.color;
        plusdalpha.a = 0;
        image.color = plusdalpha;
    }

}
