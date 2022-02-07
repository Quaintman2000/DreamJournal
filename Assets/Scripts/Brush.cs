using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBrush", menuName = "Drawing/Brush")]
public class Brush : ScriptableObject
{
    // The gameobject that will be spawned to represent this brush.
    public GameObject brushObject;
    public enum BrushType { Continuous, NonContinuous}
    // The defined brush type for this brush.
    public BrushType brushType;

    [Tooltip("Mostly used for performance issues. It removes the number of points of a continuous line base on high the tolerance number is.")]
    [Range(0,2)]
    public float tolerance;

    // TODO: Create a member variable for the animation filter.



}
