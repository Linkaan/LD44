using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogHandler : MonoBehaviour
{

    public string[] dialog;
    public string[] actions;
    public AudioClip[] narration;

    public SceneHandler sceneHandler;

    public TextMeshProUGUI script;
    public Button next;
    public Button[] buttons;

    private int dialogIndex = 0;

    public void Initialize(int dialogIndex)
    {
        this.dialogIndex = dialogIndex;
        next.gameObject.SetActive(true);
        buttons[0].transform.gameObject.SetActive(false);
        buttons[1].transform.gameObject.SetActive(false);
        buttons[2].transform.gameObject.SetActive(false);
        buttons[3].transform.gameObject.SetActive(false);

        NextDialog();
    }

    public void NextDialog()
    {
        if (dialogIndex == dialog.Length && actions.Length == 0)
        {
            sceneHandler.PreviousScene(4);
            return;
        }

        if (dialogIndex < narration.Length) sceneHandler.PlayNarration(narration[dialogIndex]);
        script.text = dialog[dialogIndex++];        

        if (dialogIndex == dialog.Length)
        {            
            if (actions.Length == 0) return;

            next.gameObject.SetActive(false);
            for (int i = 0; i < actions.Length; i++)
            {
                buttons[i].transform.gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = actions[i];
            }
        }
    }
}
