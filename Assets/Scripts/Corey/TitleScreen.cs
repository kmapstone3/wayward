using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Play()
    {
       SceneManager.LoadScene("Puzzle 0");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Credits()
    {
        SceneManager.LoadScene("End Screen");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
   
}
