using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    int camIndex;
    float speed = 1;
    bool posSeted;
    MainMenuUI mainMenuUI;

    [SerializeField] bool camAnim;

    // Start is called before the first frame update
    void Start()
    {
        camIndex = 1;
        mainMenuUI = FindAnyObjectByType<MainMenuUI>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (camAnim)
        {
            switch (camIndex)
            {
                case 1:
                    CamPos1();
                    break;
            }
        }
        else
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }

    void CamPos1()
    {
        if (!posSeted)
        {
            transform.position = new Vector3(-6, transform.position.y, transform.position.z);
            posSeted = true;
        }

        if (transform.position.x <= 6 && posSeted)
        {
            mainMenuUI.sceneAlphaController = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            mainMenuUI.sceneAlphaController = true;
            camIndex = 2;
            posSeted = false;
        }
    }
}
