using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class CounterLabel : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _counter;

  public void ChangeValue(int value)
  {
    _counter.text = value.ToString();
  }
}
