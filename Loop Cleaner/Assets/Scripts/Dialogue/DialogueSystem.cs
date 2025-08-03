using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;
        [TextArea] public string text;
    }

    [Header("UI")]
    public Text speakerText;
    public Text dialogueText;
    public float textSpeed = 0.05f;

    [Header("Dialogue")]
    public List<DialogueLine> lines = new List<DialogueLine>();

    private int _currentLine;
    private bool _isTyping;
    private System.Action _onComplete;

    public void StartDialogue(System.Action onComplete)
    {
        _currentLine = 0;
        _onComplete = onComplete;
        gameObject.SetActive(true);
        DisplayNextLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isTyping)
            {
                CompleteLine();
            }
            else
            {
                DisplayNextLine();
            }
        }
    }

    private void DisplayNextLine()
    {
        if (_currentLine >= lines.Count)
        {
            _onComplete?.Invoke();
            return;
        }

        DialogueLine line = lines[_currentLine];
        speakerText.text = line.speaker;
        StartCoroutine(TypeText(line.text));
        _currentLine++;
    }

    private System.Collections.IEnumerator TypeText(string text)
    {
        _isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        _isTyping = false;
    }

    private void CompleteLine()
    {
        StopAllCoroutines();
        dialogueText.text = lines[_currentLine - 1].text;
        _isTyping = false;
    }
}