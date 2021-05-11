using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueWindow : MonoBehaviour
{

    const float maxTextTime = 0.1f;

    CanvasGroup group;
    DialogueManager dialogueManager;
    public TMP_Text text;
    string currentText;
    public bool isTyping;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    public void ConnectWithManager(DialogueManager _dialogueManager) {
        dialogueManager = _dialogueManager;
    }

    public void Show(string _text, float _textSpeed) {
        group.alpha = 1;
        currentText = _text;
        StartCoroutine(DisplayText(_textSpeed));
    }

    public void Close() {
        StopAllCoroutines();
        group.alpha = 0;

    }

    //skips dialogue display or asks for next dialogue
    public void SkipDialogue() {
        if (isTyping) {
            StopAllCoroutines();
            text.text = currentText;
            isTyping = false;
        } else {
            dialogueManager.PlayNextDialogue();
        }
    }

    private IEnumerator DisplayText(float textSpeed) {
        text.text = "";

        string originalText = currentText;
        string displayedText = "";
        int alphaIndex = 0;
        isTyping = true;

        foreach (char c in currentText.ToCharArray()) {
            alphaIndex++;
            text.text = originalText;
            displayedText = text.text.Insert(alphaIndex, "<color=#00000000>");
            text.text = displayedText;

            yield return new WaitForSecondsRealtime(maxTextTime / textSpeed);
        }

        isTyping = false;
    }

}
