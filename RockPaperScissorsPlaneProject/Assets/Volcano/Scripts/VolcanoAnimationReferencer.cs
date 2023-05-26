using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoAnimationReferencer : MonoBehaviour
{
    public Volcano volcano;

    void CannonFire()
    {
        volcano.CannonFire();
    }

    void SummonPaper()
    {
        volcano.SummonPaper();
    }

    void Erupt(int index)
    {
        volcano.Erupt(index);
    }
}
