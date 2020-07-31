using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAmbiance : SonManager
{
    [Space]
    [SerializeField] private AudioClip sonForet;
    [SerializeField] private AudioSource sourceForet;
    [Space]
    [SerializeField] private AudioClip sonNuage;
    [SerializeField] private AudioSource sourceNuage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        JouerSon(sonForet, sourceForet);
        JouerSon(sonNuage, sourceNuage);
    }

    // Update is called once per frame
    void Update()
    {
        float[] norm = Normalise.Normaliser(new float[3] { CameraControle.Actuel.cam.orthographicSize, CameraControle.Actuel.minZoom, CameraControle.Actuel.maxZoom - 4 });
        sourceForet.volume = 1 - norm[0];
        sourceNuage.volume = norm[0];
    }
}
