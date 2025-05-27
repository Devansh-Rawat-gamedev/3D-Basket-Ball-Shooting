using DefaultNamespace;
using DefaultNamespace.SO;
using PrimeTween;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.Serialization;

[RequireComponent(typeof(StarterAssetsInputs))]
public class BasketBallShooter : MonoBehaviour
{
    #region Serialized Fields

    [Header("Prefabs & References")]
    [SerializeField] private Basketball ballPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private ScoreData scoreData;
    
    [Header("Game Settings")]
    [SerializeField] private BasketBallType basketBallType;
    
    [Header("Player Type Settings")]
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private float spinForce = 5f;
    
    [Header("Charge Mechanics")]
    [SerializeField] private Transform chargedBallPosition;
    [SerializeField] private float chargeBallDuration = 0.2f;
    [SerializeField] private Ease chargeBallEase = Ease.OutBack;
    
    [Header("Events")]
    public UnityEvent onShotTaken;

    #endregion

    #region Private Runtime Fields

    private Rigidbody _ballInstanceRb;
    private StarterAssetsInputs _input;
    private Tween _pullTween;
    private Tween _shakeTween;

    #endregion

    private ObjectPool<Basketball> _ballPool; // TODO: implement pooling for balls

    #region Unity Callbacks

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
        SpawnBall();
        SubscribeInputEvents(true);
    }

    private void OnDestroy()
    {
        SubscribeInputEvents(false);
    }

    #endregion

    private void SubscribeInputEvents(bool subscribe = true)
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
        
        if(chargeBall)//when called first time charges the ball
        {
            SpawnBall();
            ChargeBall();
        }
        else//when called second time shoots the ball
        {
            scoreData.ShotsTaken++;
            onShotTaken.Invoke();
            ReleaseBall();
            _ballInstanceRb = null;
        }
    }

    private void ChargeBall()//pullback ball
    {
        _pullTween = Tween.LocalPosition(_ballInstanceRb.transform, chargedBallPosition.localPosition, chargeBallDuration, chargeBallEase).OnComplete(() =>
        {
            _shakeTween = Tween.ShakeLocalPosition(_ballInstanceRb.transform,new Vector3(0.5f,0.5f,0.5f),
                0.1f, 0.2f, true, Ease.OutBack,0,-1);
        });
    }

    private void ReleaseBall()
    {
        float pullAmount = _pullTween.isAlive?_pullTween.progress+0.3f:1;
        _pullTween.Stop();
        _shakeTween.Stop();
        _ballInstanceRb.transform.parent = null;
        _ballInstanceRb.isKinematic = false;
        _ballInstanceRb.AddForce(spawnParent.forward * shootForce * pullAmount, ForceMode.Impulse);
        _ballInstanceRb.AddTorque(-_ballInstanceRb.transform.right * spinForce, ForceMode.Impulse);
    }
    private void SpawnBall()
    {
        if (_ballInstanceRb)
        {
            Destroy(_ballInstanceRb.gameObject);
        }
        
        var ballInstance = Instantiate(ballPrefab, spawnParent);
        ballInstance.transform.rotation = spawnParent.rotation;
        ballInstance.InitBallType(basketBallType);

        _ballInstanceRb=ballInstance.GetComponent<Rigidbody>();
        _ballInstanceRb.isKinematic = true;
    }

    public void SetBallType(BasketBallType newBallType)
    {
        if (!newBallType) return;
        basketBallType = newBallType;
        if (_ballInstanceRb)
        {
            Destroy(_ballInstanceRb.gameObject);
        }
    }
}
