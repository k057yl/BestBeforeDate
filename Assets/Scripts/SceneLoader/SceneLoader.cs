using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const int SCENE_NUMBER = 1;
    private const int WAIT_TIME = 5;
    
    
    void Start()
    {
        StartCoroutine(WaitStart());
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(WAIT_TIME);
        SceneManager.LoadScene(SCENE_NUMBER);
    }
}
