using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHandlerCharacterInformation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textName;

    [SerializeField]
    private TextMeshProUGUI _textJob;

    [SerializeField]
    private TextMeshProUGUI _textCharacterTireness;

    [SerializeField]
    private TextMeshProUGUI _textCharacterAge;


    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _textName.text = GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().EntityName;
        _textJob.text = GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().JobName;

        if (GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().IsTired)
        {
            _textCharacterTireness.text = "Tired";
        }
        else
        {
            _textCharacterTireness.text = "Not tired";
        }

        _textCharacterAge.text = GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().Age.ToString();
    }
}
