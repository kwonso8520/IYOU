using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobyManager : MonoBehaviour
{
    // Start is called before the first frame update
   public void LoadPlayScene()
    {
        SceneManager.LoadScene("main");
    }
}