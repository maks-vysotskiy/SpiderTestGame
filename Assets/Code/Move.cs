using UnityEngine;
using Random = UnityEngine.Random;

public class Move : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _transform;
    [SerializeField] Animator _animator;
    [SerializeField] float _speed;

    private bool _isDeath = false;
    
   

   
    private void FixedUpdate()
    {
        if (_isDeath) return;
       _rigidbody.velocity = _transform.forward * _speed * Time.fixedDeltaTime;

        //if (_rigidbody.velocity.x < 0.01)
        //{
        //    Debug.Log("1");
        //    _transform.rotation = Quaternion.Euler(_transform.rotation.x, _transform.rotation.y + 10, _transform.rotation.z);
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.TryGetComponent(out Borders border))
        {
            //var inDirection = collision.contacts[0].point - transform.position;
            //var normal = collision.contacts[0].normal;
            //var newForward = Vector3.Reflect(inDirection, normal);

            //_transform.forward = new Vector3(newForward.x, 0, newForward.z);


            var zMove = _transform.position.z;
            var yRot = _transform.rotation.y;

            _transform.rotation = Quaternion.Euler(_transform.rotation.x, -_transform.rotation.y, _transform.rotation.z);
            //_transform.position = 
            
        }
    }

    private void OnMouseDown()
    {
        _isDeath = true;
        _rigidbody.velocity = new Vector3(0, 0, 0);
        _animator.SetBool("Death", true);

    }


}
