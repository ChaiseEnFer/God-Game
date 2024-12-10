using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditMove : MonoBehaviour
{
    private string _move = "";
    private int _speed = 6;

    void Update()
    {
        if (_move == "left")
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(960, 490, 0), _speed * Time.deltaTime);
        }
        if (_move == "right")
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(2291, 490, 0), _speed * Time.deltaTime);
        }

    }

    public void moveLeft()
    {
        _move = "left";
    }
    public void moveRight()
    {
        _move = "right";
    }
}
