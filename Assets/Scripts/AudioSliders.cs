using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliders : MonoBehaviour
{
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        masterVolume.value = GameManager.Instance.masterVolume;
        musicVolume.value = GameManager.Instance.musicVolume;
        sfxVolume.value = GameManager.Instance.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.masterVolume = masterVolume.value;
        GameManager.Instance.musicVolume = musicVolume.value;
        GameManager.Instance.sfxVolume = sfxVolume.value;
    }
}
