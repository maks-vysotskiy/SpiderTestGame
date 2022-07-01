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

    private float _timeForDeath = 2.5f;
    private bool _isDeath;

    public float Speed => _speed;
    public float AnimationSpeed => _animationSpeed;

    public Transform Transform => _transform;

    public Rigidbody Rigidbody => _rigidbody;

    public Animator Animator => _animator;

    private void Start()
    {
        _isDeath = false;
        _spiderMove = new SpiderMove(this);
        _animator.speed = _animationSpeed;
    }

    private void FixedUpdate()
    {
        _spiderMove.FixedExecute(_isDeath);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _spiderMove.OnCollisionEnter(collision);

    }

    private void OnMouseDown()
    {
       Death();
    }

    internal void GetPool(SpiderPool pool)
    {
        _spiderPool = pool;
    }

    private void Death()
    {
        _isDeath = true;
        _rigidbody.velocity = new Vector3(0, 0, 0);
        _animator.SetBool("Death", true);
        StartCoroutine(WaitDeath());
    }

    private IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(_timeForDeath);
        _isDeath = false;
        _spiderPool.ReturnToPool(this);
    }

}
