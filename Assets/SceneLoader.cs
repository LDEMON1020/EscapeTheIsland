using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
