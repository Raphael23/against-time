﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    public Text dialogText;
    private string _textToDisplay;

    [SerializeField] private int _timeBetweenLetters;
    [SerializeField] private Character _character;
    private CharacterDialogue _characterDialogue;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void UpdateToStateOfDialogue(DialogueState dialogueState)
    {
        if(dialogueState == DialogueState.Disabled && this.gameObject.activeSelf == true)
        {
            _textToDisplay = "";
            this.gameObject.SetActive(false);
        }
        else if(dialogueState == DialogueState.Typing)
        {
            DisplayText();
        }
    }
    private void DisplayText()
    {
        if (_characterDialogue == null)
            _characterDialogue = _character.gameObject.GetComponent<CharacterDialogue>();

        _textToDisplay = _characterDialogue.DialogueText;
        this.gameObject.SetActive(true);

        char[] charArr = _textToDisplay.ToCharArray();
        string charText = "";
        foreach (char ch in charArr)
        {
                charText = charText + ch;
                dialogText.text = _textToDisplay;
         
        }
        _characterDialogue.DoneTyping();
        _textToDisplay = "";
    }

    private void OnEnable()
    {
       CharacterDialogue.OnDialogueStateChanged += UpdateToStateOfDialogue;
    }
    private void OnDestroy()
    {
        CharacterDialogue.OnDialogueStateChanged -= UpdateToStateOfDialogue;
    }
    public void OnMouseDown()
    {
        _characterDialogue.NextState();
    }
}
