using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FreezePlayer : MonoBehaviour
{
    [SerializeField]
    private float freezeInterval = 10f; // how often the player is frozen

    [SerializeField]
    private KeyCode[] inputSequence; // the sequence of keys the player needs to press to unfreeze

    [SerializeField]
    private float inputTimeout = 4f; // how long the player has to input the correct sequence

    public bool isFrozen = false; // flag to indicate if the player is currently frozen
    private float timeUntilNextFreeze; // time until the next freeze
    private float timeUntilInputTimeout; // time until the input timeout
    private int currentInputIndex; // current index in the input sequence

    private GameObject player;
    KeyCode[] newcodes;
    // UI elements
    [SerializeField] private GameObject freezeOverlay;
    [SerializeField] private TMP_Text inputSequenceText;
    [SerializeField] private Image[] keyCodeImages;

    [SerializeField] private Image timeTick;
    [SerializeField] private Sprite imageYes;
    [SerializeField] private Sprite imageNo;
    [SerializeField] private Sprite imageQuestion;

    void Start()
    {
        timeUntilNextFreeze = Random.Range(freezeInterval - 5f, freezeInterval + 5f); // randomly offset the first freeze time
        freezeOverlay.SetActive(false); // hide the freeze overlay
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (!isFrozen)
        {
            // decrement time until next freeze
            timeUntilNextFreeze -= Time.deltaTime;
            float timeRemainingPercentage = Mathf.Clamp01(timeUntilInputTimeout / inputTimeout);
            timeTick.fillAmount = timeRemainingPercentage;
            GameObject.Find("Player").GetComponent<PlayerShooting>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            GameObject.Find("Shoot Position").GetComponent<WeaponPivot>().enabled = true;

            if (timeUntilNextFreeze <= 0f)
            {
                // freeze the player
                isFrozen = true;
                timeUntilInputTimeout = inputTimeout;
                currentInputIndex = 0;

                foreach (Image image in keyCodeImages)
                {
                    image.sprite = imageQuestion;
                }

                // show the freeze overlay and generate a new input sequence
                freezeOverlay.SetActive(true);
                newcodes = GenerateInputSequence_New();
                inputSequenceText.text = GenerateInputSequence_Text(newcodes);

                // reset the key code images to default color
                foreach (Image image in keyCodeImages)
                {
                    image.color = Color.white;
                }

                // reset the time until the next freeze
                timeUntilNextFreeze = Random.Range(freezeInterval - 5f, freezeInterval + 5f);
            }
        }
        else
        {
            // decrement time until input timeout
            timeUntilInputTimeout -= Time.deltaTime;
            float timeRemainingPercentage = Mathf.Clamp01(timeUntilInputTimeout / inputTimeout);
            timeTick.fillAmount = timeRemainingPercentage;
            GameObject.Find("Player").GetComponent<PlayerShooting>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("Shoot Position").GetComponent<WeaponPivot>().enabled = false;

            if (timeUntilInputTimeout <= 0f)
            {
                player.GetComponent<PlayerHealth>().FreezeFail();
                isFrozen = false;
                freezeOverlay.SetActive(false);
            }
            if (Input.anyKeyDown)
            {
                // check if the pressed key matches the current input sequence
                if (Input.GetKeyDown(newcodes[currentInputIndex]))
                {
                    keyCodeImages[currentInputIndex].sprite = imageYes;

                    currentInputIndex++;

                    // check if the input sequence is complete
                    if (currentInputIndex >= inputSequence.Length)
                    {
                        // unfreeze the player and hide the freeze overlay
                        isFrozen = false;
                        freezeOverlay.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerShooting>().ChangeProjectile();
                        player.GetComponent<ChangeHead>().ChangeForm(GameObject.Find("Player").GetComponent<PlayerShooting>().currentHead);
                    }

                }
                else
                {
                    // reset the input sequence if the player pressed the wrong key
                    currentInputIndex = 0;

                    // reset the key code images to default color
                    foreach (Image image in keyCodeImages)
                    {
                        image.sprite = imageQuestion;
                    }
                }
            }
                
        }
    }

    // generates a random input sequence based on the available keycodes
    private string GenerateInputSequence_Text(KeyCode[] inputSequence)
    {
        string sequence = "";

        for (int i = 0; i < inputSequence.Length; i++)
        {
            sequence += inputSequence[i].ToString() + " ";
        }

        return sequence;
    }

    private KeyCode[] GenerateInputSequence_New()
    {
        KeyCode[] newInputSequence = new KeyCode[inputSequence.Length];

        for (int i = 0; i < inputSequence.Length; i++)
        {
            newInputSequence[i] = inputSequence[Random.Range(0, inputSequence.Length)];
        }

        return newInputSequence;
    }
}