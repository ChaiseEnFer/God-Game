using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class underConstructionBuildManager : MonoBehaviour
{
    [SerializeField]
    private float constructionTime;

    public GameObject buildObjective;
    public GameObject buildParent;
    public GameObject parent;
    public string state;

    void Start()
    {
        state = "waiting";
        state = "start construction";
    }
    void Update()
    {
        if (state == "start construction")
        {
            state = "under construction";
            StartCoroutine(Construction());
        }
    }

    IEnumerator Construction()
    {
        yield return new WaitForSeconds(constructionTime);
        GameObject newBuild = Instantiate(buildObjective,transform.position, Quaternion.identity);
        newBuild.transform.parent = buildParent.transform;
        parent.GetComponent<spawnBuilding>().buildList.Add(newBuild);
        if (newBuild.tag == "house")
        {
            parent.GetComponent<spawnBuilding>().houseList.Add(newBuild);
        }
        parent.GetComponent<spawnBuilding>().buildList.Remove(gameObject);
        Destroy(gameObject);

    }
}
