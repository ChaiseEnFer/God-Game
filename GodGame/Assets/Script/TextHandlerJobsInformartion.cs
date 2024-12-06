using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHandlerJobsInformartion : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textWanderer;

    [SerializeField]
    private TextMeshProUGUI _textFoodHarvester;

    [SerializeField]
    private TextMeshProUGUI _textTimber;

    [SerializeField]
    private TextMeshProUGUI _textMiner;

    [SerializeField]
    private TextMeshProUGUI _textMason;

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _textWanderer.text = GameManager.Instance.Wanderers.Count.ToString();

        _textFoodHarvester.text = GameManager.Instance.FoodHarvesters.Count.ToString();
        
        _textTimber.text = GameManager.Instance.Timbers.Count.ToString();
        
        _textMiner.text = GameManager.Instance.Miners.Count.ToString();
        
        _textMason.text = GameManager.Instance.Masons.Count.ToString();
    }
}