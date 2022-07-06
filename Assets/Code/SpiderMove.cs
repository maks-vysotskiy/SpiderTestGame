using UnityEngine;

internal sealed class SpiderMove
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Spider _spider;

    public SpiderMove(Spider spider)
    {
        _spider = spider;
        _rigidbody = spider.Rigidbody;
        _transform = spider.Transform;
    }

    public void Execute(bool isDeath)
    {
        if (isDeath)
        {
            return;
        }
        var ray = new Ray(_transform.position, _transform.forward);
        Debug.DrawRay(_transform.position, _transform.forward);
        var normalRay = new Vector3();

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1))
        {
            if (hit.collider.TryGetComponent(out Borders border))
            {
                normalRay = hit.normal;
                var newForward = Vector3.Reflect(_transform.forward, normalRay);
                _transform.forward = new Vector3(newForward.x, 0, newForward.z);
            }
        }
    }

    public void FixedExecute(bool isDeath)
    {
        if (isDeath)
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);
            return;
        }
        var speed = _spider.Speed;
        _rigidbody.velocity = _transform.forward * speed * Time.fixedDeltaTime;
    }
}

