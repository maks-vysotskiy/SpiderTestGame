using System.Collections;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _transform;
    [SerializeField] Animator _animator;

    [SerializeField] private float _speed;
    [SerializeField] private float _animationSpeed;

    private SpiderMove _spiderMove;
    private SpiderPool _spiderPool;
    private PointManager _pointManager;

    private int _pointsForSpider = 1;
    private float _timeForDeath = 1.5f;

    public bool _isDeath;

    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    public float AnimationSpeed
    {
        get
        {
            return _animationSpeed;
        }
        set
        {
            _animationSpeed = value;
        }
    }

    public Transform Transform => _transform;

    public Rigidbody Rigidbody => _rigidbody;

    public Animator Animator => _animator;

    private void Start()
    {
        _isDeath = false;
        _spiderMove = new SpiderMove(this);
        _animator.speed = _animationSpeed;

        _spiderPool.OnDeactivate += Death;
    }

    private void FixedUpdate()
    {
        _spiderMove.FixedExecute(_isDeath);
    }

    private void Update()
    {
        _spiderMove.Execute(_isDeath);
    }

    private void OnMouseDown()
    {
        Death();
    }

    internal void GetPool(SpiderPool pool)
    {
        _spiderPool = pool;
    }

    internal void GetPointManager(PointManager pointManager)
    {
        _pointManager = pointManager;
    }

    private void Death()
    {
        if (_isDeath)
        {
            return;
        }

        _spiderPool.CalculateSpiderOnSceneAfterDeathSpider();
        _pointManager.CalculatePoints(_pointsForSpider);
        _isDeath = true;
        _animator.SetBool("Death", true);
        _spiderPool.OnDeactivate -= Death;

        StartCoroutine(WaitDeath());
    }

    private IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(_timeForDeath);
        _spiderPool.ReturnToPool(this);
    }
    
    public void ActivateSpiderSettings()
    {
        _spiderPool.OnDeactivate += Death;
        _isDeath = false;
    }

}
