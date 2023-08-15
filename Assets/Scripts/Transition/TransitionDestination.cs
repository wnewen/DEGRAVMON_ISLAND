using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        Enter, grassHill01, grassHill02, grassHill03, grassHill04, stoneHouse, hunterLodge, landslideIncident, officeOutdoor, officeIndoor, modern
    }

    public DestinationTag _destinationTag; // 需要被TransitionController存取
    
    // public DestinationTag GetAndSetDestinationTag()
    // {
    //     get { return _destinationTag; }
    //     set { _destinationTag = value; }
    // }
}
