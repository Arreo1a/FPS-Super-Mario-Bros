using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    [SerializeField] private GameObject _emptyBlock;
    [SerializeField] private GameObject _coinSpawnEffect;
    
    [SerializeField] private Transform _itemSpawnPoint;

    [Header("PowerUps")]
    [SerializeField] private GameObject _powerUp;
    [SerializeField] private bool _hasMultipleCoins = false;

    private Animator _anim;
    private Animator _itemSpawnPoint_anim;

    [Header("Block With Coins")]
    [SerializeField] float _giveCoinCountdown = 0f;
    [SerializeField] float _timeBeforeNextCoin = 0.2f;
    [SerializeField] float _timeLeftSinceFirstHit = 3f;

    bool _wasHit = false; 
    bool _hasNoHealth = false;

    CoinCounter _coinCounter_script;


    void Awake() {
        _coinCounter_script = GameObject.FindObjectOfType(typeof(CoinCounter)) as CoinCounter;
    }

    private void Start() {
        _anim = gameObject.GetComponent<Animator>();
    }


    void Update() {
        _giveCoinCountdown -= Time.deltaTime;

        if (_wasHit && _hasMultipleCoins) {
            _timeLeftSinceFirstHit -= Time.deltaTime;
        }
    }
    
    public void GotHit() {
        if (_powerUp != null) {
            while (!_wasHit) {    
                HasPowerUp();
            }
        } else if (_hasMultipleCoins && _powerUp == null) {
            HasCoins();
            _wasHit = true;
        }
        else {
            _coinCounter_script.AddCoin(1);
            _hasNoHealth = true;
            _anim.SetBool("isHit", true);
        }
    }

    void SpawnItem() {
        Instantiate(_powerUp, _itemSpawnPoint.position, transform.rotation);
    }

    void HasPowerUp() {
        _anim.SetBool("isHit", true);
        SpawnItem();
        _wasHit = true;
        _hasNoHealth = true;
    }

    void HasCoins() {
        if (_timeLeftSinceFirstHit < 0) {
            if (_giveCoinCountdown <= 0) {
                _giveCoinCountdown = _timeBeforeNextCoin;
                _coinCounter_script.AddCoin(1);

                var coinEffect = Instantiate(_coinSpawnEffect, _itemSpawnPoint.position, transform.rotation);
                Destroy(coinEffect, 0.3f);

                _anim.SetBool("isHit", true);

                _hasNoHealth = true;
            }
        } else if (_giveCoinCountdown <= 0) {
                _giveCoinCountdown = _timeBeforeNextCoin;
                _coinCounter_script.AddCoin(1);

                var coinEffect = Instantiate(_coinSpawnEffect, _itemSpawnPoint.position, transform.rotation);
                Destroy(coinEffect, 0.3f);

                _anim.SetBool("isHit", true);
        }
    }


    void DestroyBlock() {
        if (_hasNoHealth) {
            gameObject.SetActive(false);
            _emptyBlock.SetActive(true);
        }
        else {
            _anim.SetBool("isHit", false);
        }
    }
}