using System;
using DefaultNamespace;
using DefaultNamespace.SO;
using PrimeTween;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput))]
public class BasketBallShooter : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    Basketball ballPrefab;
    
    [SerializeField]
    private PlayerData playerData;
    [Header("Basketball Settings")]
    [SerializeField]
    BasketBallType basketBallType;
    [SerializeField]
    Transform spawnParent;
    
    [FormerlySerializedAs("ShootForce")]
    [Header("Ball Settings")]
    [SerializeField]
    private float shootForce = 10f;
    public UnityEvent onShotTaken;
    [Header("Charge Ball Settings")]
    [SerializeField]
    Transform chargedBallPosition;
    [SerializeField]
    float chargeBallDuration = 0.2f;
    [SerializeField]
    Ease chargeBallEase = Ease.OutBack;
    
    
    #endregion
    private Rigidbody _ballInstanceRb;
    private StarterAssetsInputs _input;
    private Tween pullTween;
    private Tween shakeTween;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
        SpawnBall();
        
        SubscribeInputEvents();
    }

    public void SubscribeInputEvents(bool subscribe = true)
    {
        if (subscribe)
        {
            _input.OnShot += OnShot;
            
        }
        else
        {
            _input.OnShot -= OnShot;
        }
    }

    private void OnShot(bool chargeBall)
    {
        
        if(chargeBall)
        {
            SpawnBall();
            ChargeBall();
        }
        else
        {
            playerData.ShotsTaken++;
            onShotTaken.Invoke();
            ReleaseBall();
            _ballInstanceRb = null;
        }
    }

    private void ChargeBall()//pullback ball
    {
        pullTween = Tween.LocalPosition(_ballInstanceRb.transform, chargedBallPosition.localPosition, chargeBallDuration, chargeBallEase).OnComplete(() =>
        {
            shakeTween = Tween.ShakeLocalPosition(_ballInstanceRb.transform,new Vector3(0.5f,0.5f,0.5f),
                0.1f, 0.2f, true, Ease.OutBack,0,-1);
        });
    }

    private void ReleaseBall()
    {
        float pullAmount = pullTween.isAlive?pullTween.progress+0.3f:1;
        pullTween.Stop();
        shakeTween.Stop();
        _ballInstanceRb.transform.parent = null;
        _ballInstanceRb.isKinematic = false;
        _ballInstanceRb.AddForce(spawnParent.forward * shootForce * pullAmount, ForceMode.Impulse);
        _ballInstanceRb.AddTorque(-_ballInstanceRb.transform.right * shootForce, ForceMode.Impulse);
    }

    void SpawnBall()
    {
        if (_ballInstanceRb)
        {
            Destroy(_ballInstanceRb.gameObject);
        }
        
        var ballInstance = Instantiate(ballPrefab, spawnParent);
        ballInstance.InitBallType(basketBallType);

        _ballInstanceRb=ballInstance.GetComponent<Rigidbody>();
        _ballInstanceRb.isKinematic = true;
    }
    private void OnDestroy()
    {
        _input.OnShot -= OnShot;
    }
}
