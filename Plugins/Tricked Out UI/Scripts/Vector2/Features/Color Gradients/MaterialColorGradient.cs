using JacobHomanics.TrickedOutUI;
using UnityEngine;

public class MaterialColorGradient : BaseColorGradient
{
    public Material mat;

    void Update()
    {
        mat.color = CalculateColor();
    }

    void Reset()
    {
        if (!mat)
            mat = GetComponent<Renderer>().material;
    }
}
