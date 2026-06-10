using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    //public DialogueData dialogueData;
    [SerializeField] private TMP_Text nameText;
	[SerializeField] private TMP_Text dialogueText;

	private Queue<string> sentences = new Queue<string>();

    public static event Action DialogueEnded;

	void Start () {
		//sentences = new Queue<string>();
        //StartDialogue();
	}

    void OnEnable() {
        ScoreScreen.StartDialogue += StartDialogue;
        IntroUI.StartIntroDialogue += StartDialogue;
    }

    void OnDisable() {
        ScoreScreen.StartDialogue -= StartDialogue;
        IntroUI.StartIntroDialogue -= StartDialogue;
    }

	public void StartDialogue(DialogueData dialogueData)
	{
        Debug.Log(dialogueData.speakerName);
		nameText.text = dialogueData.speakerName;

		sentences.Clear();

		foreach (string sentence in dialogueData.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.03f);
		}
	}

	void EndDialogue()
	{
		DialogueEnded?.Invoke();
	}
}
