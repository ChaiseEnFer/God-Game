using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textTimer;

    [SerializeField]
    private TextMeshProUGUI _textEntitiesNumber;

    [SerializeField]
    private TextMeshProUGUI _textFoodQuantity;

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _textTimer.text = GameManager.Instance.SelectedCharacter.GetComponent<Clock>().ActualHour.ToString() + " : " + GameManager.Instance.SelectedCharacter.GetComponent<Clock>().ActualMinute.ToString(); ;
        _textEntitiesNumber.text = GameManager.Instance.EntitiesNumber.ToString(); ;
        _textFoodQuantity.text = GameManager.Instance.FoodQuantity.ToString(); ;
    }
}
