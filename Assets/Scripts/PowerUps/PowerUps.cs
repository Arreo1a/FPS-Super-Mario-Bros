using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private Rigidbody m_Rigidbody;
    
    [SerializeField] private float _spawnForceY; 
    [SerializeField] private float _spawnForceX; 

    [SerializeField] private float _maxSpeed;

    bool _isSpawned = false;

    [SerializeField] float _maxHeight = 2f;

    void Start() {
        _maxHeight += transform.position.y;   
    }

    void Update() {
        SpawnFromBlock();
        if(GetComponent<Rigidbody>().velocity.magnitude > _maxSpeed) {
            m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * _maxSpeed;
        }

        if (GetComponent<Rigidbody>().position.y >= _maxHeight) {
            GetComponent<Rigidbody>().velocity =  Vector3.zero;
        }

        if (m_Rigidbody.velocity.y < 2) {
            m_Rigidbody.AddForce(new Vector3(0, -400 * 10, 0) * Time.deltaTime);
        }
    }

    public void SpawnFromBlock() {
        if (!_isSpawned) {
            m_Rigidbody.AddForce(new Vector3(0, 5 * _spawnForceY , 2 * _spawnForceX) * Time.deltaTime);
            _isSpawned = true;
        }
    }


    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Destroy(gameObject);
            Debug.Log("Mario hit");
        }
        Debug.Log("Something hit");
    }
}
