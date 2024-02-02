using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string nextScene;
    public Animator fadeSystem;
    public int door;

    private bool isInRange = false;
    private Text interactUI;

    public PlayerMove playerMoveScript;
    

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled=false;
        }
    }

    void Start()
    {
        interactUI = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        PlayerPrefs.SetInt("door", door);
        LoadAndSaveData.instance.SaveData();
        //playerMoveScript.enabled = false;
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        //playerMoveScript.enabled = true;
        SceneManager.LoadScene(nextScene);
    }




}
