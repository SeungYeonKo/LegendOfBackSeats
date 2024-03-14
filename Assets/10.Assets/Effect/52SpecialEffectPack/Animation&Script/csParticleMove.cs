using UnityEngine;
using System.Collections;

public class csParticleMove : MonoBehaviour
{
    public float FireBallSpeed = 0.5f;
    private Rigidbody _rigidbody;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * FireBallSpeed;
    }

    void Update () {
        transform.Translate(Vector3.back * FireBallSpeed);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
