using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title : MonoBehaviour
{
    [SerializeField]
    GameObject mini;
    [SerializeField]
    GameObject turbo;
    [SerializeField]
    GameObject neww;
    [SerializeField]
    GameObject le64;
    [SerializeField]
    GameObject super;
    [SerializeField]
    GameObject deux;
    [SerializeField]
    GameObject montp;

    private int _state = 10;
    private int _speed = 6;
    private string _move = "";
    private float _timer = 0f;


    // Update is called once per frame
    void Update()
    {
        if (_state == 10)
        {
            _timer += Time.deltaTime;
            if (_timer > 2f)
            {
                _state = 0;
            }
        }
        if (_state == 0)
        {
            mini.transform.position = Vector3.Lerp(mini.transform.position, new Vector3(616, 580, 0), _speed * Time.deltaTime);
            if (mini.transform.position.x > 611)
            {
                _state = 1;
            }
        }
        if (_state == 1)
        {
            turbo.transform.position = Vector3.Lerp(turbo.transform.position, new Vector3(1311, 692, 0), _speed * Time.deltaTime);
            if (turbo.transform.position.y < 697)
            {
                _state = 2;
            }
        }
        if (_state == 2)
        {
            neww.transform.position = Vector3.Lerp(neww.transform.position, new Vector3(613, 816, 0), _speed * Time.deltaTime);
            if (neww.transform.position.x > 608)
            {
                _state = 3;
            }
        }
        if (_state == 3)
        {
            le64.transform.position = Vector3.Lerp(le64.transform.position, new Vector3(1415, 357, 0), _speed * Time.deltaTime);
            if (le64.transform.position.x < 1420)
            {
                _state = 4;
            }
        }
        if (_state == 4)
        {
            super.transform.position = Vector3.Lerp(super.transform.position, new Vector3(975, 732, 0), _speed * Time.deltaTime);
            if (super.transform.position.y < 737)
            {
                _state = 5;
            }
        }
        if (_state == 5)
        {
            deux.transform.position = Vector3.Lerp(deux.transform.position, new Vector3(1389, 572, 0), _speed * Time.deltaTime);
            if (deux.transform.position.x < 1394)
            {
                _state = 6;
            }
        }
        if (_state == 6)
        {
            montp.transform.position = Vector3.Lerp(montp.transform.position, new Vector3(1004, 439, 0), _speed * Time.deltaTime);
            if (montp.transform.position.y > 434)
            {
                _state = 7;
                Debug.Log("qrgrdswg");
            }
        }
        if (_move == "up")
        {

            transform.position = Vector3.Lerp(transform.position, new Vector3(0,1000, 0), _speed * Time.deltaTime);
        }
        if (_move == "down")
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), _speed * Time.deltaTime);
        }

    }

    public void moveUp()
    {
        mini.transform.position = new Vector3(616, 580, 0);
        turbo.transform.position =new Vector3(1311, 692, 0);
        neww.transform.position = new Vector3(613, 816, 0);
        le64.transform.position = new Vector3(1415, 357, 0);
        super.transform.position = new Vector3(975, 732, 0);
        deux.transform.position = new Vector3(1389, 572, 0);
        montp.transform.position = new Vector3(1004, 439, 0);
        _move = "up";
    }
    public void moveDown()
    {
        _move = "down";
    }
}
