using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
   Pig[] pigs;
   [SerializeField]string nextLevelName;
    // Start is called before the first frame update
    void OnEnable()
    {
        pigs = FindObjectsOfType<Pig>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PigsDead())
            {
                GoToNextLevel();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void GoToNextLevel()
    {
        
        SceneManager.LoadScene(nextLevelName);
     
    }
    bool PigsDead()
    {
        foreach (var pig in pigs)
        {
            if (pig.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;




    }
    



}
