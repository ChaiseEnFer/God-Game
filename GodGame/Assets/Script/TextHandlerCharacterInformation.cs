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
        _textName.text = "Name : " +GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().EntityName;
        _textJob.text = "Job : " + SetJobName();

        if (GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().IsTired)
        {
            _textCharacterTireness.text = "Tired";
        }
        else
        {
            _textCharacterTireness.text = "Not tired";
        }

        _textCharacterAge.text = "Age : " +GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().Age.ToString();
    }

    private string SetJobName()
    {
        string _jobName = "Ca marche pas";

        switch (GameManager.Instance.SelectedCharacter.GetComponent<PeopleProperties>().Job)
        {
            case 0:
                _jobName = "Wanderer";
                break;
            case 1:
                _jobName = "Food Harvester";
                break;
            case 2:
                _jobName = "Timber";
                break;
            case 3:
                _jobName = "Miner";
                break;
            case 4:
                _jobName = "Mason";
                break;
        }
        return _jobName;
    }
}
