using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{

    [Serializable]
    public struct NamedScene
    {
        public string action;
        public DialogHandler scene;
    }
    public NamedScene[] scenes;

    public AudioSource audioPlayer;

    private Dictionary<string, DialogHandler> scenesDict;

    private DialogHandler currentScene;
    private DialogHandler previousScene;

    private void Start()
    {
        scenesDict = new Dictionary<string, DialogHandler>();
        for (int i = 0; i < scenes.Length; i++)
        {
            scenesDict[scenes[i].action] = scenes[i].scene;
        }

        NextScene("start");
    }

    public void PlayNarration(AudioClip line)
    {
        audioPlayer.Stop();
        audioPlayer.clip = line;
        audioPlayer.Play();
    }

    public void NextScene(string action)
    {
        if (currentScene)
        {
            previousScene = currentScene;
            currentScene.gameObject.SetActive(false);
        }
        currentScene = scenesDict[action];
        currentScene.Initialize(0);
        currentScene.gameObject.SetActive(true);
    }

    public void PreviousScene(int dialogIndex)
    {
        currentScene.gameObject.SetActive(false);
        currentScene = previousScene;
        currentScene.Initialize(dialogIndex);
        currentScene.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
