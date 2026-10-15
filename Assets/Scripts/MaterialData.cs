using System;
using UnityEngine;

[Serializable]
public class MaterialData
{
    // PROPERTIES
    [SerializeField] private float strength; // resistance to breaking
    [SerializeField] private float hardness; // resistance to deforming
    [SerializeField] private float elasticity; // stretchiness
    [SerializeField] private float ductility; // ability to deform without shattering
    [SerializeField] private float thermalConductivity;
    [SerializeField] private float electricConductivity;
    [SerializeField] private float density;
    [SerializeField] private float meltingPoint;
    [SerializeField] private float reactivity;
    [SerializeField] private float radioactivity;
    // STATE
    [SerializeField] private float temperature;
    [SerializeField] private float stability; // how unlikely it is to be lost in a reaction
}
