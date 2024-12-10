using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButtonMove : MonoBehaviour
{
    private string _move = "";
    private int _speed = 6;

    void Update()
    {
        if (_move == "down")
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(960, -160, 0), _speed * Time.deltaTime);
        }
        if (_move == "up")
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(960, 140, 0), _speed * Time.deltaTime);
        }

    }

    public void moveUp()
    {
        _move = "up";
    }
    public void moveDown()
    {
        _move = "down";
    }
}
