using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player;
    public SoundManager soundmanager;
    //Start Game UI Objects
    public GameObject StartButton;
    public GameObject TapToMove;
    public GameObject TapImage;

    // Dead Screen UI Objects
    public GameObject RestartButton;
    public GameObject RestartText;
    // Wall UI Objects
    public GameObject countDownText;
    public Text countDownDisplay;
    public int countDownTime = 5;


    //Finished Game UI Objects
    public GameObject PlayAgainButton;
    public GameObject RadialShine;
    public GameObject Completed;
    public GameObject PlayAgainText;

    //UI Settings
    
    
    public void Start() 
    {
        //Save system for sound
        if(PlayerPrefs.HasKey("Sound") == false)
        {
            PlayerPrefs.SetInt("Sound",1);
        }
        //Save system for vibration
        if(PlayerPrefs.HasKey("Vibration") == false)
        {
            PlayerPrefs.SetInt("Vibration",1);
        }
    }
    
    public void Update() 
    {
        if(RadialShine == true)
        {
            RadialShine.GetComponent<RectTransform>().Rotate(new Vector3(0,0,15f * Time.deltaTime));
        }
        
    }
    public void StartGame()
    {
        StartButton.SetActive(false);
        TapToMove.SetActive(false);
        TapImage.SetActive(false);
        Variables.dead = false;
    }
    public void RestartButtonActive()
    {
        RestartButton.SetActive(true);
        RestartText.SetActive(true);
    }

    public void RestartGame()
    {
        Variables.dead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void FinishGame()
    {
        StartCoroutine(CountDownStart());

    }
    IEnumerator CountDownStart()
    {
        countDownText.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDownText.SetActive(false);
        countDownDisplay.gameObject.SetActive(true);
        while(countDownTime >= 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f); //to make the time integer

            countDownTime --;

            if(countDownTime <0)
            {
                
                player.walls.SetActive(false);
                countDownDisplay.gameObject.SetActive(false);
                PlayAgainButton.SetActive(true);
                RadialShine.SetActive(true);
                soundmanager.FinishSound();
                yield return new WaitForSecondsRealtime(1f);
                Completed.SetActive(true);
                yield return new WaitForSecondsRealtime(1f);
                PlayAgainText.SetActive(true);
            }
        }

        
        // Finish game music

    }

    //Settings Button Fuctions
    

}
