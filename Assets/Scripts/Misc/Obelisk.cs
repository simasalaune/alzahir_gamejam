using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Obelisk : MonoBehaviour
{
    private PauseMenu pauseMenu;

    private bool canInteract = false; // Whether the player can interact with the obelisk
    public bool collected = false;
    [SerializeField] private GameObject currentLetter;
    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void Interact()
    {
        if (canInteract && !collected)
        {
            pauseMenu.SetObeliskCount();
            pauseMenu.ShowInteractable(false);
            collected = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            StartCoroutine(ShowLetter());
        }
    }

    public void ShowInteractionPrompt(bool show)
    {
        canInteract = show && !collected;
        pauseMenu.ShowInteractable(show);
    }
    IEnumerator ShowLetter()
    {
        currentLetter.SetActive(true);
        yield return new WaitForSeconds(4.2f);
        currentLetter.SetActive(false);
    }
}
