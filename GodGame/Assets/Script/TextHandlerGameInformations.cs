using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHandlerGameInformations : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textTimer;

    [SerializeField]
    private TextMeshProUGUI _textEntitiesNumber;

    [SerializeField]
    private TextMeshProUGUI _textFoodQuantity;

    [SerializeField]
    private TextMeshProUGUI _textDayNumber;
    [SerializeField]
    private TextMeshProUGUI _textWoodQuantity;
    [SerializeField]
    private TextMeshProUGUI _textStoneQuantity;

    public Clock Clock;

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _textTimer.text = Clock.ActualHour.ToString() + " : " + Mathf.RoundToInt(Clock.ActualMinute).ToString();
        _textDayNumber.text = "Jour : " + Clock.ActualDay;
        _textEntitiesNumber.text = "Population : " +GameManager.Instance.EntitiesNumber.ToString(); ;
        _textFoodQuantity.text = "Food : " +GameManager.Instance.FoodQuantity.ToString();
        _textWoodQuantity.text = "Wood : " + GameManager.Instance.WoodQuantity.ToString();
        _textStoneQuantity.text = "Stone : " + GameManager.Instance.StoneQuantity.ToString();
    }
}
