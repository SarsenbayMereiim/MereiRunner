using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Image SettingImage;
    public Image BestScoreImage;
    public TMP_Text scoreText;
    public AudioSource Audio;
    public Slider volumeSlider;
    public Image VolumeImage;

    public float bestScore;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bestScore = PlayerPrefs.GetFloat("MyScore");
        scoreText.text = bestScore.ToString();
        
        Audio.volume = volumeSlider.value;
    }
    public void Setting()
    {
        SettingImage.gameObject.SetActive(true);
    }
    public void Exit()
    {
        SettingImage.gameObject.SetActive(false);
    }
    public void BestScore()
    {
        BestScoreImage.gameObject.SetActive(true);
    }
    public void ExitBestScore()
    {
        BestScoreImage.gameObject.SetActive(false);
    }
    public void ExitVolume()
    {
        VolumeImage.gameObject.SetActive(false);
    }
    public void Volume()
    {
        VolumeImage.gameObject.SetActive(true);
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
