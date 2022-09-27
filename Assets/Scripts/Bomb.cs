using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * create a new bomb script because bomb will be handled differently from fruit
 */


public class Bomb : MonoBehaviour
{
    private AudioSource bombSound;

    private void Start()
    {
        bombSound = GetComponent<AudioSource>();
    }


    // code comes fruit but no "Slice" function instead trigger Game Over Sequence
    private void OnTriggerEnter(Collider other)
    {

        // we tagged our blade as the "Player"
        if (other.CompareTag("Player"))
        {
            bombSound.Play();
            // call explode function in game manager
            FindObjectOfType<GameManager>().Explode();

            
        }
    }
}
