using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinCounter _coinCounter_script;

    void Awake() {
        _coinCounter_script = GameObject.FindObjectOfType(typeof(CoinCounter)) as CoinCounter;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            _coinCounter_script.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
