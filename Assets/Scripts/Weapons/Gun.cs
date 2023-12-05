using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public static Gun instance;

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _shootPoint;


    [Header("Effects")]
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private LineRenderer _bulletTrail;

    [Header("Stats")]
    [SerializeField] private bool _isAutomaticWeapon = false;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _fireRate = 10f;
    [SerializeField] private float _range = 200f;

    [Header("Hit/ImpactEffects")]
    [SerializeField] private GameObject _brickBlock_impactEffect;

    float _nextTimeToFire = 0f;
    bool _isPressingShoot = false;



    void Awake() {

    }

    void Update() {
        if (_isPressingShoot && Time.time >= _nextTimeToFire ) {
            _nextTimeToFire = Time.time + 1f/_fireRate;
            Shoot();
        }

    }

    void Shoot() {
        _muzzleFlash.Play();
        RaycastHit hitInfo;
    
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, _range)) {
            EnemyHealth enemyHealth = hitInfo.transform.GetComponent<EnemyHealth>();
            BrickBlock brickBlock = hitInfo.transform.GetComponent<BrickBlock>();
            QuestionBlock questionBlock = hitInfo.transform.GetComponent<QuestionBlock>();

            if (enemyHealth != null) {
                enemyHealth.TakeDamage(_damage);
            } else if (brickBlock != null) {
                brickBlock.TakeDamage(_damage);
                Instantiate(_brickBlock_impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            } else if (questionBlock != null) {
                questionBlock.GotHit();
            }

            
            
        } else {
            
        }
    }

    private void SpawnBulletTrail(Vector3 hitPoint) {
        GameObject bulletTrailEffect = Instantiate(_bulletTrail.gameObject, _shootPoint.position, Quaternion.identity);

        LineRenderer lineRenderer = bulletTrailEffect.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, _shootPoint.position);
        lineRenderer.SetPosition(1, hitPoint);

        Destroy(bulletTrailEffect, 1f);
    }

    public void Fire_Action(InputAction.CallbackContext context) {
        if (_isAutomaticWeapon) {
            if (context.performed) {
                _isPressingShoot = true;
            } else {
                _isPressingShoot = false;
            }
        } else {
            if (context.started) {
                Shoot();
            }
        }
    }
}
