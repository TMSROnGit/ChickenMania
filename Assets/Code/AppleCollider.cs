using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollider : MonoBehaviour
{
    public GameObject BodyPart;
    public string mainChickenTag = "Player";
    public SnakeLikeBehaviour SnakeLikeBehaviour;
    public float IncreaseSpeedAmount = 0.5f;

    public List<GameObject> BodyParts = new List<GameObject>();

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // Calls the catchedApple method to destroy the apple and spawn a new one
        // Calls the method to grow the chicken and play the apple collision sound
        if (other.CompareTag(mainChickenTag))
        {
            AppleSpawning appleSpawning = FindObjectOfType<AppleSpawning>();
            appleSpawning.catchedApple();
            GrowChickenBodyOnCollid();
        }

        // If the apple collides with the player, increase the score
        if (other.gameObject.tag == "Player")
        {
            ScoreUI.scoreCount += 100;
        }
    }

    private void GrowChickenBodyOnCollid()
    {
        // Grow the chicken, increase the speed and plays the apple collision sound
        SnakeLikeBehaviour SnakeLikeBehaviour = FindObjectOfType<SnakeLikeBehaviour>();
        SnakeLikeBehaviour.GrowChickens();
        SnakeLikeBehaviour.Movespeed += IncreaseSpeedAmount;
        SnakeLikeBehaviour.collisionAppleSound.Play();
    }
}
