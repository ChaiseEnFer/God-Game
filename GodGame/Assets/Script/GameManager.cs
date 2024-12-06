using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //Listes contenant les peoples selon leur métier
    public List<GameObject> wanderers = new();
    public List<GameObject> foodHarvesters = new();
    public List<GameObject> timbers = new();
    public List<GameObject> miners = new();
    public List<GameObject> masons = new();

    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
