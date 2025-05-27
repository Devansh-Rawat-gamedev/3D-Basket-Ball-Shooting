using System;
using UnityEngine;
using UnityEngine.Events;

public class Basket : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
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
            playerData.ShotsMade++;
            onShotMade.Invoke();
            Destroy(other.gameObject, 2f);
        }
    }
}
