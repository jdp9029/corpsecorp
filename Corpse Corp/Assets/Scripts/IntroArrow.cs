using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene("UpdatedScene2");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
