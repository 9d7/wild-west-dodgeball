using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Actor", order = 1)]
public class ActorScriptableObject : ScriptableObject
{
    public Color textColor;
    public Sprite actorImage;
    public bool flippedX;
}
