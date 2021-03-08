using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogText;

    [SerializeField] private Image actorImage;
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private float timeBetweenLines;
    private CanvasGroup cg;
    [SerializeField] private Dialog[] initialDialog;

    private bool shouldSkipNextLine = false;

    [SerializeField] private Animator showInstructionsAnim;
    

    [Serializable]
    public struct Dialog
    {
        public ActorScriptableObject actor;
        public string content;
    }

    public void RunDialog(Dialog[] diag, bool initial)
    {
        StartCoroutine(RunDialogRoutine(diag, initial));
    }

    public void OnSkip()
    {
        shouldSkipNextLine = true;
    }

    IEnumerator RunDialogRoutine(Dialog[] diag, bool initialDialog)
    {
        shouldSkipNextLine = false;
        cg.alpha = 1;
        for (int i = 0; i < diag.Length; i++)
        {
            dialogText.text = "";
            actorImage.sprite = diag[i].actor.actorImage;
            dialogText.color = diag[i].actor.textColor;
            for (int j = 0; j < diag[i].content.Length; j++)
            {
                dialogText.text += diag[i].content[j];
                if (shouldSkipNextLine)
                {
                    dialogText.text = diag[i].content;
                    shouldSkipNextLine = false;
                    break;
                }
                yield return new WaitForSecondsRealtime(timeBetweenCharacters);
            }

            yield return new WaitForSecondsRealtime(timeBetweenLines);
        }

        if (initialDialog)
        {
            GameManager.Instance.StartAction();
            showInstructionsAnim.SetTrigger("Start");
        }
        cg.alpha = 0;
    }
    // Update is called once per frame
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        RunDialog(initialDialog, true);
    }
}
