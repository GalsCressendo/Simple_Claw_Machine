using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrizeAttribute", menuName = "Create/PrizeAttribute", order = 1)]
public class PrizeAttribute : ScriptableObject
{
    public string prizeName;
    public GameObject UI_Gameobject;
    [TextAreaAttribute]
    public string description;
}
