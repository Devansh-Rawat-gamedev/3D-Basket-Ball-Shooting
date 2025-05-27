using DefaultNamespace.SO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Basketball : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _collider;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
            
    }
    // Initializes the basketball with the specified type given by shooter.
    public void InitBallType(BasketBallType ballType)
    {
        _rb.mass = ballType.Mass;
        _collider.material = ballType.BbPhysicsMaterial;
        if (ballType.UseRadius)
        {
            SetColliderRadius(ballType.Radius);
        }
        else
        {
            SetColliderRadius(ballType.BbGameObject.GetComponent<Renderer>().bounds.size.x); 
        }
        if (ballType.BbGameObject)
        {
            var instantiatedObject = Instantiate(ballType.BbGameObject, transform.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(transform);
            instantiatedObject.transform.localRotation = ballType.BbGameObject.transform.rotation;
        }

        if (ballType.UsePhysicsMaterial)
        {
            _collider.material = ballType.BbPhysicsMaterial;
        }
        else
        {
                
            _collider.material =ballType.PhysicsMaterial;
        }
    }
    //helper
    void SetColliderRadius(float size)
    {
        switch (_collider)
        {
            case SphereCollider sphereCollider:
                sphereCollider.radius = size;
                break;
            case CapsuleCollider capsuleCollider:
                capsuleCollider.radius = size;
                break;
            case BoxCollider boxCollider:
                boxCollider.size = new Vector3(size * 2, size * 2, size * 2);
                break;
            default:
                Debug.LogWarning("Unsupported collider type for radius setting.");
                break;
        }
    }
        
}
