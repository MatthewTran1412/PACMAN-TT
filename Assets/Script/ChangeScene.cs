using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string scenename;
    private void Update() {
        if(Input.anyKeyDown)
            SceneManager.LoadScene(scenename);
    }
}
