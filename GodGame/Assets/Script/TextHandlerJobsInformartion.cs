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
        _textWanderer.text = "Wanderers" + ":" + GameManager.Instance.Wanderers.Count.ToString();

        _textFoodHarvester.text = "FoodHarvesters" + ":" + GameManager.Instance.FoodHarvesters.Count.ToString();
        
        _textTimber.text = "Timbers" + ":" + GameManager.Instance.Timbers.Count.ToString();
        
        _textMiner.text = "Miners" + ":" + GameManager.Instance.Miners.Count.ToString();
        
        _textMason.text = "Masons" + ":" + GameManager.Instance.Masons.Count.ToString();
    }
}