using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bouton : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Color couleurOver;
    [SerializeField] private Color couleurEnfonce;
    private SpriteRenderer spR;

    public UnityEvent eventBouton;

    // Start is called before the first frame update
    void Start()
    {
        spR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        spR.color = couleurOver;
    }

    private void OnMouseExit()
    {
        spR.color = Color.white;
    }

    private void OnMouseDown()
    {
        spR.color = couleurEnfonce;
    }

    private void OnMouseUpAsButton()
    {
        spR.color = Color.white;
        eventBouton.Invoke();
    }
}
