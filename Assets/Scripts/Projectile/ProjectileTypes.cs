using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ProjectileTypes", order = 1)]
public class ProjectileTypes : ScriptableObject
{
    public List<ProjectileType> types;

    public ProjectileType? this[string str]
    {
        get
        {
            foreach (ProjectileType type in types)
            {
                if (type.name == str) return type;
            }

            return null;
        }
    }
}
