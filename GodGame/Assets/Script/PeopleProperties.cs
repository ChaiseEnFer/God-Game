using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleProperties : MonoBehaviour
{
    public string EntityName = "";
    public string JobName = ""; //sous la pression je fus dans l'obkigation de créer cette variable.
    public int Job;
    public bool IsTired = false;
    public int Tireness = 100;
    public int Age;



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

    private void Death()
    {
        Destroy(gameObject);
    }
}