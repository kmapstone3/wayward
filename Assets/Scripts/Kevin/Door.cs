using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private List<Character> playersInRange = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !playersInRange.Contains(other.GetComponent<Character>()))
            playersInRange.Add(other.GetComponent<Character>());

        if(playersInRange.Count >= 2)
            LoadScene();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && playersInRange.Contains(other.GetComponent<Character>()))
            playersInRange.Remove(other.GetComponent<Character>());
    }
}
