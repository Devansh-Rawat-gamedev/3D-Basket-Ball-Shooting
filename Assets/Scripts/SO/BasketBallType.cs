using System;
using UnityEngine;

namespace DefaultNamespace.SO
{
    [CreateAssetMenu(fileName = "BasketBallType", menuName = "Scriptable Objects/BasketBallType", order = 0)]
    public class BasketBallType : ScriptableObject
    {
        [field: SerializeField] public string TypeName { get; private set; }
        [field: SerializeField] public float Mass { get; private set; }
        [field: SerializeField] public bool UseRadius { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public GameObject BbGameObject { get; private set; }
        [field: SerializeField] public bool UsePhysicsMaterial { get; private set; }
        [field: SerializeField] public PhysicsMaterial BbPhysicsMaterial { get; private set; }
        [field: SerializeField] public float Bounciness { get; private set; }
        [field: SerializeField] public PhysicsMaterialCombine BounceCombine { get; private set; }
        [field: SerializeField] public float DynamicFriction { get; private set; }
        [field: SerializeField] public float StaticFriction { get; private set; }
        [field: SerializeField] public PhysicsMaterialCombine FrictionCombine { get; private set; }
[NonSerialized]
        public PhysicsMaterial PhysicsMaterial ;//made using other fields
        
        // This is a custom physics material that will be used for the basketball
        private void OnValidate()
        {
            InitPhysicsMaterial(out PhysicsMaterial);
        }
        private void Awake()
        {
            InitPhysicsMaterial(out PhysicsMaterial);
        }
        
        
        //helper
        private void InitPhysicsMaterial(out PhysicsMaterial physicsMaterial)
        {
            physicsMaterial = new PhysicsMaterial
            {
                bounciness = Bounciness,
                staticFriction = StaticFriction,
                dynamicFriction = DynamicFriction,
                bounceCombine = BounceCombine,
                frictionCombine = FrictionCombine
            };
        }
    }
}
