using UnityEngine;

internal sealed class SpiderMove
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;

    private float _speed;


    public SpiderMove(Spider spider)
    {
        _rigidbody = spider.Rigidbody;
        _transform = spider.Transform;
        _animator = spider.Animator;
        _speed = spider.Speed;
    }

    public void FixedExecute(bool isDeath)
    {
        if (isDeath) return;

        _rigidbody.velocity = _transform.forward * _speed * Time.fixedDeltaTime;


    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.TryGetComponent(out Borders border))
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);

            var inDirection = collision.contacts[0].point - _transform.position;
            var normal = collision.contacts[0].normal;
            var newForward = Vector3.Reflect(inDirection, normal);

            _transform.forward = new Vector3(newForward.x, 0, newForward.z);
                       
        }
    }


}

