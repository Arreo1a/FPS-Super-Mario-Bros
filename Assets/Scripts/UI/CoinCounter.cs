using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public int _coinCount;

    [SerializeField] private TextMeshProUGUI _coinCounterText;

    void Update() {
        _coinCounterText.text = _coinCount.ToString();
    }

    public void AddCoin(int amountToAdd) {
        _coinCount += amountToAdd;
    }
}
