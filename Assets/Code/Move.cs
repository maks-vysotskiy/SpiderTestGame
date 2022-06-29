using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _transform;
    [SerializeField] float _speed;
       
      

    private void FixedUpdate()
    {
        _rigidbody.velocity = _transform.forward * _speed * Time.fixedDeltaTime;
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Borders border))
        {
            _transform.forward = Vector3.Reflect(collision.contacts[0].point, collision.contacts[0].normal);
           
        }
    }


}
