using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueContainer;
    DialogueWindow dialogueScript;

    [SerializeField]
    public List<DialogueBox> lines = new List<DialogueBox>();

    DialogueBox currentBox;
    int currentLineIndex;
    bool exhaustedDialogue;

    private void Awake() {
        dialogueScript = dialogueContainer.GetComponent<DialogueWindow>();
        exhaustedDialogue = false;
        dialogueScript.ConnectWithManager(this);
        currentLineIndex = -1;
    }

    private void Update() {
        if (!exhaustedDialogue && Input.GetKeyDown(KeyCode.C)) {
            dialogueScript.SkipDialogue();
        }
    }

    public void PlayNextDialogue() {

        if (currentLineIndex < lines.Count - 1) {
            currentLineIndex++;
            currentBox = lines[currentLineIndex];
            dialogueScript.Show(currentBox.text, currentBox.textSpeed);
        } else {
            HasExhaustedDialogue();
        }

    }

    void HasExhaustedDialogue() {
        exhaustedDialogue = true;
        dialogueScript.Close();
    }

    public void TurnoffScreen() {
        dialogueScript.Close();
    }

    [Serializable]
    public struct DialogueBox {
        [TextArea]
        public string text;
        public float textSpeed;
    }
}
