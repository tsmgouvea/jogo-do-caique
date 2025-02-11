using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections; 
public class SceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        // Only specifying the sceneName or scenebuildIndex will Load the Scene with the Single Mode
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

}
