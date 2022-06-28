using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
   public UIManager uimanager;
   public SoundManager soundmanager;
   //Player Movement
   public float currentRunningSpeed;
   public float limitX;
   public float xSpeed;
   private float _lastTouchedX;

   public GameObject walls;
   private Animator _anim;
   private Rigidbody _rb;
   
   private bool _gameStart;
  
   void Start() 
   {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _gameStart = false;
        Time.timeScale = 1;
   }

   void Update() 
   {
    if(Variables.dead == false)
    {
        float newX = 0;
        float touchXDelta = 0;
        if(Input.touchCount > 0) // Touch controll
        {
            _gameStart = true;

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {    
                _lastTouchedX = Input.GetTouch(0).position.x;
                _anim.SetBool("canRun",true);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                 touchXDelta = 5 * (Input.GetTouch(0).position.x - _lastTouchedX) / Screen.width;
                 _lastTouchedX = Input.GetTouch(0).position.x;
            }
            
        } 
        else if(Input.GetMouseButton(0)) // Mouse controll
        {
            _gameStart = true;
            touchXDelta = Input.GetAxis("Mouse X");
            _anim.SetBool("canRun",true);
            
        }
            newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
            newX = Mathf.Clamp(newX,-limitX,limitX); 
            if(_gameStart == true)
            {
                Vector3 newPosition = new Vector3(newX,transform.position.y,transform.position.z + currentRunningSpeed * Time.deltaTime);
                transform.position = newPosition;
            }
    }
   }

   public void OnCollisionEnter(Collision other) 
   {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Die();
            StartCoroutine("Restart");
        }
   }
   public void OnTriggerEnter(Collider other) 
   {
        if(other.gameObject.CompareTag("Finish"))
        {
            walls.SetActive(true);
            transform.position = new Vector3(0f,transform.position.y,transform.position.z);
            StartCoroutine("Finish");
            uimanager.FinishGame();
        }
   }
    public void Die()
    {
        _anim.SetBool("dead",true);
        soundmanager.DeadPlayerSound();
        Variables.dead = true;
    }

    public IEnumerator Finish()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Variables.dead = true;
        _anim.SetBool("canRun",false);
    }
    public IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 0;
        uimanager.RestartButtonActive();
    }

}
