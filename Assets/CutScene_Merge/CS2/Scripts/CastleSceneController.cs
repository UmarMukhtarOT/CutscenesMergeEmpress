using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

[Serializable]
public class Durations
{


    public float InitialDelay;
    public float DayToNight;
    public float CamZoom;
    //   public float ForgroundMove;
    // public float CloudsMove;
    public float CracksAppear;

}



public class CastleSceneController : CutScenesCommon
{

    public SpriteRenderer cracks;
    public Transform Clouds;
    public Transform TreeForeGround;
    public Transform TreeWithGround;
    public GameObject AnimatedBirds;

    public Durations Dur;


    public SpriteRenderer[] NightSpr;
    public SpriteRenderer[] DaySpr;
    
    IEnumerator TypeCor;




    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        AnimatedBirds.SetActive(true);
        cracks.gameObject.SetActive(false);


        foreach (var item in NightSpr)
        {
            item.DOFade(0, 0);
        }


        StartCoroutine(Cutscene());







    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(Dur.InitialDelay);

        PlayAudio(NarrationClips[0], Narrations[0]);

        //TreeForeGround.DOMoveX(-1, Dur.ForgroundMove*1.5f);
        //TreeWithGround.DOMoveX(0, Dur.ForgroundMove);
        //Clouds.DOMoveX(-1.7f, Dur.CloudsMove); 


        TreeForeGround.DOMoveX(-1, Dur.CamZoom * 1.5f);
        TreeWithGround.DOMoveX(0, Dur.CamZoom);
        Clouds.DOMoveX(-1.7f, Dur.CamZoom);
        Cam.DORotate(new Vector3(0, 3.75f, 0), Dur.CamZoom / 2);

        yield return new WaitForSeconds(5);

        foreach (var item in NightSpr)
        {
            item.DOFade(1, Dur.DayToNight);
        }
        foreach (var item in DaySpr)
        {
            item.DOFade(0, Dur.DayToNight);
        }


        Cam.DOMoveZ(-15, Dur.CamZoom).OnComplete(() =>
        {
            cracks.enabled = true;


        });


        yield return new WaitForSeconds(Dur.CracksAppear / 2);



        PlayAudio(NarrationClips[1], Narrations[1]);

        cracks.gameObject.SetActive(true);
        cracks.GetComponent<AudioSource>().Play();
        cracks.DOFade(1, 2);
        StartCrackFlicker();
        yield return new WaitForSeconds(0.5f);

        cracks.DOFade(1, 0);
        cracks.enabled = true;
        yield return new WaitForSeconds(5);

        PauseAllDotweeen();
        OnCutSceneCompleted();

    }



    //void PlayAudio(AudioClip AC, string str)
    //{
        
    //    //if (TypeCor != null)
    //    //{
    //    //    StopCoroutine(TypeCor);
    //    //}
    //    //TypeCor = base.TypingText(str);
    //    //StartCoroutine(TypeCor);

    //    Narrtext.text = "";
    //    Narrtext.text = str;
    //    Narr_AS.Stop();
    //    Narr_AS.PlayOneShot(AC);


    //}




    private void StartCrackFlicker()
    {

        cracks.DOFade(1, 0.15f).OnComplete(() =>
        {

            cracks.DOFade(0, 0.15f).OnComplete(() =>
            {

                cracks.DOFade(1, 0.15f);


            });



        });



    }

    private void PauseAllDotweeen()
    {
        Cam.transform.DOPause();
        cracks.transform.DOPause();
        Clouds.transform.DOPause();
        TreeForeGround.transform.DOPause();
        TreeWithGround.transform.DOPause();

        foreach (var item in DaySpr)
        {
            item.DOPause();
        }

    }

    public void OnCutSceneCompleted()
    {
        Debug.Log("**********CutScene Finished**********");
    
    }



   
}
