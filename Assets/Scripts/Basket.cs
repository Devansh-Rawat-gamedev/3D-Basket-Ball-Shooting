using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Basket : MonoBehaviour
{
    [FormerlySerializedAs("playerData")] [SerializeField]
    private ScoreData scoreData;
    public UnityEvent onShotMade;
    bool isBallInBasket = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position.y > transform.position.y)
        {
            isBallInBasket = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Basketball")) return;
        if (other.transform.position.y < transform.position.y && isBallInBasket)
        {
            isBallInBasket = false;
            scoreData.ShotsMade++;
            onShotMade.Invoke();
            Destroy(other.gameObject, 2f);
        }
    }
}
