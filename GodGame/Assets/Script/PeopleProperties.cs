using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleProperties : MonoBehaviour
{
    public string EntityName;
    //public string JobName; //sous la pression je fus dans l'obkigation de créer cette variable.
    public int Job;
    public bool IsTired = false;
    public int Tireness = 100;
    public int Age;

    [SerializeField]
    public Material Miner;

    [SerializeField]
    public Material Timber;

    [SerializeField]
    public Material Wanderer;

    [SerializeField]
    public Material Harvester;

    [SerializeField]
    public Material Mason;




    private void Start()
    {
        ChangeSkin();
    }

    /// <summary>
    /// Cette fonction sera appelée pour vérifier si l'unité doit se reposer
    /// </summary>
    public void CheckTired()
    {
        if (!IsTired)
        {
            if (Tireness <= 0)
            {
                IsTired = true;
            }
        }
        else
        {
            if (Tireness > 0)
            {
                IsTired = false;
            }
        }
    }

    /// <summary>
    /// fait mourir par l'age
    /// </summary>
    public void CheckForAge()
    {
        if (Age >= 7)
        {
            Death();
        }
    }

    public void Death()
    {
        if (GameManager.Instance.SelectedCharacter == this.gameObject)
        {
            UIManager.Instance.HidePeopleInfoPanel();
            UIManager.Instance.HideSchoolPanel();
        }
        Destroy(gameObject);
    }

    public void ChangeSkin()
    {
        switch (Job) 
        { 
            case 0:
                gameObject.GetComponent<MeshRenderer>().material = Wanderer;
                break;
            case 1:
                gameObject.GetComponent<MeshRenderer>().material = Harvester;
                break;
            case 2:
                gameObject.GetComponent<MeshRenderer>().material = Timber;
                break;
            case 3:
                gameObject.GetComponent <MeshRenderer>().material = Miner; 
                break;
            case 4:
                gameObject.GetComponent<MeshRenderer>().material = Mason;
                break;
        }
    }
}