using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI phaseText;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;


    void Update()
    {
        phaseText.text = "Current Phase: " + GlobalVariables.currentPhase.ToString();
    }

    /*public void OnMusicChangeVolume()
    {
        float volumeInDB = Mathf.Log10(Mathf.Max(musicSlider.value,0.00001f))*20f;
        mixer.SetFloat("ExposedMusicVolume", volumeInDB);
    }*/
}
