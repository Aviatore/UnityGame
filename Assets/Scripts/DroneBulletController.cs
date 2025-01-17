using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DroneBulletController : MonoBehaviour
    {
        public GameObject explosion;
        public float bulletSpeed ;
        
        private Transform _playerTransform;
        private float _destinationPosition;

        private void Awake()
        {

            _playerTransform = GetPlayerTransform();
                
            if (transform.position.x > _playerTransform.position.x)
            {
                _destinationPosition = _playerTransform.position.x - 60;
            }
            else if (transform.position.x < _playerTransform.position.x)
            {
                _destinationPosition = _playerTransform.position.x + 60;
            }
            else
            {
                _destinationPosition = _playerTransform.position.x;
            }
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(_destinationPosition, transform.position.y, transform.position.z), 
                bulletSpeed * Time.deltaTime);

            if (Math.Abs(transform.position.x - _destinationPosition) < 25)
            {
                Destroy(gameObject);
            }
        }

        private Transform GetPlayerTransform()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null)
            {
                _playerTransform = player.transform;
            }

            return _playerTransform;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
                return;

            if (other.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<AudioManager>().PlaySound("BulletHitPlayer");
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                return;
            }
            
            Destroy(gameObject);
        }
    }
}