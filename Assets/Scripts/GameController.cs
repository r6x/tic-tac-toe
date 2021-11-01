using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingProgress;
    public GameObject settings;
    public Image loadingBar;
    [Header("Загрузка сцены")]
    public float loadSpeed = 0.05f;
    public string sceneName = "SampleScene";
    [Header("Музыка")]
    public Slider musicVolume;
    public AudioSource music;
    public bool destroyMusic = false;
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        if (music.isPlaying)
        {
            music.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(music != null)
        {
            music.volume = musicVolume.value;
        }
        
    }
    
    public void StartButton()
    {
        if(destroyMusic == false)
        {
            DontDestroyOnLoad(music);
        }
        StartCoroutine(loadScene());
    }

    public void HidePanel(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ShowPanel(GameObject obj)
    {
        obj.SetActive(true);
    }
     
    IEnumerator pause()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            loadingBar.fillAmount += loadSpeed;
            Debug.Log("pause coroutine");
            if(loadingBar.fillAmount >= 1.0)
            {
                break;
            }
        }
        
    }

    IEnumerator loadScene()
    {
        yield return StartCoroutine(pause());
        Debug.Log("scene loaded");
        SceneManager.LoadScene(sceneName);
    }
}
