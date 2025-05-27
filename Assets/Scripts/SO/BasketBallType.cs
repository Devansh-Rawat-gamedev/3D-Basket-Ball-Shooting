using System;
using UnityEngine;

namespace DefaultNamespace.SO
{
    [CreateAssetMenu(fileName = "BasketBallType", menuName = "Scriptable Objects/BasketBallType", order = 0)]
    public class BasketBallType : ScriptableObject
    {
        [field: SerializeField]public string TypeName { get; private set;  }
        [field: SerializeField]public float Mass { get; private set; }
        [field: SerializeField]public bool UseRadius { get; private set; }
        [field: SerializeField]public float Radius { get; private set; }
        [field: SerializeField]public GameObject BbGameObject { get; private set; }
        [field: SerializeField]public bool UsePhysicsMaterial { get; private set; }
        [field: SerializeField]public PhysicsMaterial BbPhysicsMaterial { get; private set; }
        [field: SerializeField]public float Bounciness { get; private set; }
        [field: SerializeField]public PhysicsMaterialCombine BounceCombine { get; private set; }
        [field: SerializeField]public float DynamicFriction { get; private set; }
        [field: SerializeField]public float StaticFriction { get; private set; }
        
    }
}
