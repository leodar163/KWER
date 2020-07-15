using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendreFinTour : CustomYieldInstruction
{
    int nbrTour;

    public override bool keepWaiting
    {
        get
        {
            return nbrTour > 0;
        }
    }

    public AttendreFinTour() 
    { 
        nbrTour = 1;
        TourParTour.Defaut.eventNouveauTour.AddListener(DecremTour);
    }
    public AttendreFinTour(int nombreDeTour) 
    { 
        nbrTour = nombreDeTour;
        TourParTour.Defaut.eventNouveauTour.AddListener(DecremTour);
    }

    private void DecremTour()
    {
        nbrTour--;
    }

}
