using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class gestionBouttonFin : MonoBehaviour
{
    [SerializeField]
    private float _timeToChangeScene;

    public TransitionSettings Transition;

    public void LoadScene(string _sceneName)
    {
        TransitionManager.Instance().Transition(_sceneName, Transition, _timeToChangeScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
