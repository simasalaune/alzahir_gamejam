using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textComponent;

    [SerializeField]
    private string textToWrite;

    [SerializeField]
    private float timeBetweenCharacters = 0.1f;

    [SerializeField]
    private GameObject obelisk1, obelisk2;

    private bool skipText = false;

    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        obelisk1.SetActive(false);
        obelisk2.SetActive(false);
        textComponent.text = "";

        for (int i = 0; i < textToWrite.Length; i++)
        {
            if (skipText)
            {
                textComponent.text = textToWrite;
                break;
            }

            textComponent.text += textToWrite[i];

            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }

    public void OnSkipButtonPressed()
    {
        skipText = true;
    }
}