using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        Enter, grassHill01_stoneHouse, grassHill01_landslideIncident, grassHill02, grassHill03, grassHill04, stoneHouse, hunterLodge, landslideIncident, officeOutdoor, officeIndoor, modern
    }

    public DestinationTag _destinationTag; // 需要被TransitionController存取，為自身的目的地名稱
    
    // public DestinationTag GetAndSetDestinationTag()
    // {
    //     get { return _destinationTag; }
    //     set { _destinationTag = value; }
    // }
}
