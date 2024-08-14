using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLikeBehaviour : MonoBehaviour
{
    public float Movespeed = 5;
    public float Steerspeed = 250;
    public float BodySpeed = 5;
    public int Gap = 20;

    public string mainChickenTag = "Player";
    public string treeTag = "Tree";
    public string ChickenBodyPartTag = "ChickenBodyPart";
    public string fenceTag = "Fence";
    public GameObject BodyPart;
    private GameObject firstSpawnedBodyPart;

    public List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    public OpenEndScreen OpenEndScreen;
    private Animator animation;
    public AudioSource collisionGameOverSound;
    public AudioSource backgroundMusic;
    public AudioSource collisionAppleSound;

    private Vector3 InitialSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the initial spawn position, makes the first spawned body part null to avoid imediate collision and sets the animation
        firstSpawnedBodyPart = null;
        InitialSpawn = transform.position;
        animation = GetComponent<Animator>();
    }

 

    // Update is called once per frame
    void Update()
    {

        // Enables the player to move the chicken
        transform.position += transform.forward * Movespeed * Time.deltaTime;

        // Enables the player to rotate the chicken
        float changeDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, changeDirection * Steerspeed * Time.deltaTime);

        // Saves the chicken current position
        PositionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in BodyParts)
        {
            // Moves the body parts taking into account the main chicken position
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the "snakes" path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Rotate body towards the point along the "snakes" path
            body.transform.LookAt(point);

            index++;
        }

        // Animation control
        if (Movespeed != 0)
        {
            animation.SetInteger("Walk", 1);
        }
        else
        {
            animation.SetInteger("Walk", 0);
        }
    }

    public void GrowChickens() 
    {

        Vector3 spawnPosition;

        if (BodyParts.Count == 0)
        {
            // If there are no body parts, spawn behind the main chicken
            spawnPosition = transform.position - transform.forward * Gap;
        }
        else
        {
            // If there are body parts, spawn behind the last body part
            GameObject lastBodyPart = BodyParts[BodyParts.Count - 1];
            spawnPosition = lastBodyPart.transform.position - lastBodyPart.transform.forward * Gap;
        }

        // Instantiate the prefab body part and add it to the list
        GameObject body = Instantiate(BodyPart);

        // Defines the first spawned body part to avoid collision and adds another body part to the list
        if (firstSpawnedBodyPart == null)
        {
            firstSpawnedBodyPart = body;
            body.tag = "FirstBodyPart";
        }
        else
        {
            body.tag = "ChickenBodyPartTag";
        }

        BodyParts.Add(body);
    }

    // Responsible for increasing the speed of the chicken
    public void IncreaseMovespeed(float amount)
    {
        Movespeed += amount;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Ignores the collision with the first spawned body part
        if (other.gameObject == firstSpawnedBodyPart)
        {
            return;
        }

        // If the chicken collides with the tree, fence, body part or main chicken, the game is over and the end screen is shown
        if (other.CompareTag(treeTag) || other.CompareTag(fenceTag) || other.CompareTag(ChickenBodyPartTag) || other.CompareTag(mainChickenTag))
        {
            Time.timeScale = 0;
            collisionGameOverSound.Play();
            backgroundMusic.Stop();
            OpenEndScreen.ShowEndGameMenu();
        }
    }

    public void ResetPosition()
    {
        // Resets the position of the chicken, the rotation, the speed and destroys all body parts
        transform.position = InitialSpawn;
        transform.rotation = Quaternion.identity;
        Movespeed = 5;

        foreach (var body in BodyParts)
        {
            Destroy(body);
        }
        BodyParts.Clear();
    }
}
