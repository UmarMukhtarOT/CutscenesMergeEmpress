using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class CutScenesCommon : MonoBehaviour
{
    [Header("                                                                  Common Data")]
    [Space(20)]
    public float NarrTypingSpeed = 0.05f;
    public Transform Cam;
    public TextMeshProUGUI Narrtext;
    public AudioSource Narr_AS;
    public AudioSource AS_Ambience;
    public string[] Narrations;
    public AudioClip[] NarrationClips;

    [Space(50)]
    [Header("                                                                  Scene Data")]
    [Space(20)]
    public string SceneDatas = "Scene Data";

    // Start is called before the first frame update
    public virtual void Start()
    {
        Narrtext.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }



    }



    public IEnumerator TypingText(string str)
    {
        Narrtext.text = "";

        for (int i = 0; i < str.Length; i++)
        {
            Narrtext.text += str[i];

            yield return new WaitForSeconds(NarrTypingSpeed);


        }



    }

    public virtual void Skip()
    {
        DOTween.PauseAll();

    }




    public void SetAmbience(AudioClip clip)
    {


        AS_Ambience.DOFade(0, 0.4f).OnComplete(() =>
        {

            AS_Ambience.clip = clip;
            AS_Ambience.Play();
            AS_Ambience.DOFade(0.2f, 0.4f);


        });




    }




    public IEnumerator PlayAudio(AudioClip AC, string str, float initialDelay = 0)
    {
        yield return new WaitForSeconds(initialDelay);

        Narrtext.gameObject.SetActive(true);

        Narrtext.text = "";
        Narrtext.text = str;

        Narr_AS.Stop();
        Narr_AS.PlayOneShot(AC);
        yield return new WaitUntil(() => Narr_AS.isPlaying == false);

        yield return new WaitForSeconds(2);
        Narrtext.gameObject.SetActive(false);

    }

}
