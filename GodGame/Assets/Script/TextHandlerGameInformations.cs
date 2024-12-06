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
        //_textTimer.text = $"{GameManager.Instance.selectedCharacter.GetComponent<Clock>().Hour}" + " : " + $"{GameManager.Instance.selectedCharacter.GetComponent<Clock>().Minute}";
        //_textEntitiesNumber.text = GameManager.Instance.selectedCharacter.GetComponent<PeopleProperties>().EntitiesNumber;
        //_textFoodQuantity.text = GameManager.Instance.selectedCharacter.GetComponent<PeopleProperties>().FoodQuantity;
    }
}
