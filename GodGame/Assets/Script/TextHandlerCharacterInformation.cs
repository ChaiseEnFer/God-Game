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


    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _textName.text = GameManager.Instance.selectedCharacter.GetComponent<PeopleProperties>().entityName;
        _textJob.text = GameManager.Instance.selectedCharacter.GetComponent<PeopleProperties>().jobName;

        if (GameManager.Instance.selectedCharacter.GetComponent<PeopleProperties>().isTired)
        {
            _textCharacterTireness.text = "Tired";
        }
        else
        {
            _textCharacterTireness.text = "Not tired";
        }
    }
}
