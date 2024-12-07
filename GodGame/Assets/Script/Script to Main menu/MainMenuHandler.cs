using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
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
