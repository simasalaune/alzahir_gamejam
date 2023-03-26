using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHead : MonoBehaviour
{
    [SerializeField] private Sprite[] heads;

    [SerializeField] private Sprite[] letters;

    [SerializeField] private GameObject currentHead;
    [SerializeField] private GameObject currentLetter;


    public void ChangeForm(int newHead)
    {
        currentHead.GetComponent<SpriteRenderer>().sprite = heads[newHead];
        currentLetter.GetComponent<SpriteRenderer>().sprite = letters[newHead];
        StartCoroutine(ShowLetter());
    }

    IEnumerator ShowLetter()
    {
        currentLetter.SetActive(true);
        yield return new WaitForSeconds(4.2f);
        currentLetter.SetActive(false);
    }
}
