using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Basket : MonoBehaviour
{
    [FormerlySerializedAs("playerData")] [SerializeField]
    private ScoreData scoreData;
    public UnityEvent onShotMade;
    private bool _isBallInBasket = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Basketball basketball)) return;
        if (basketball.transform.position.y > transform.position.y)
        {
            _isBallInBasket = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Basketball basketball)) return;
        if (basketball.transform.position.y < transform.position.y && _isBallInBasket)
        {
            _isBallInBasket = false;
            scoreData.ShotsMade++;
            onShotMade.Invoke();
            Destroy(basketball.gameObject, 3f);
        }
    }
}
