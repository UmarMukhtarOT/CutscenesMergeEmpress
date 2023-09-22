using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


[Serializable]
public class TwoEnemyScene 
{
    
    public SpriteRenderer BushOnGround;
    public SpriteRenderer[] Enemy;
    public SpriteRenderer[] Soldier;

}





public class IntroSceneController : CutScenesCommon
{
    public Vector3 CamStartPos;
    public Transform Clouds;
    public Transform TreeForeGround;
    public Transform TreeWithGround;
    public Transform Rain;
    public GameObject Lightening;

    public AudioClip []LighteningAudio;
    public Durations Dur;
    public SpriteRenderer[] NightSpr;
    public SpriteRenderer[] DaySpr;
    public SpriteRenderer[] BattleSceneSpr;
    IEnumerator TypeCor;
    public AudioSource LightAS;

    public TwoEnemyScene twoEnemyScene;




    private void Awake()
    {
        Cam.position = CamStartPos;
    }


    // Start is called before the first frame update
    public override void Start()
    {
        
        base.Start();
        Rain.gameObject.SetActive(false);
        Lightening.SetActive(false);
        StartCoroutine(Cutscene());
    }


    IEnumerator Cutscene()
    {

        yield return new WaitForSeconds(Dur.InitialDelay);

        StartCoroutine(PlayAudio(NarrationClips[0], Narrations[0]));
        TreeForeGround.DOMoveX(-1, Dur.CamZoom * 1.5f);
        TreeWithGround.DOMoveX(0, Dur.CamZoom);
        Clouds.DOMoveX(-1.7f, Dur.CamZoom);
        Cam.DORotate(new Vector3(0, 3.75f, 0), Dur.CamZoom / 2);
        



        Cam.DOMoveZ(-15, Dur.CamZoom);
        yield return new WaitForSeconds(4);

        DayToNight();
        yield return new WaitForSeconds(2);
        LightAS.PlayOneShot( LighteningAudio[0]);
        Lightening.SetActive(true);

        yield return new WaitForSeconds(1);

        Rain.gameObject.SetActive(true);

        Cam.DORotate(Vector3.zero, Dur.CamZoom);
        Cam.DOMoveX(-12, Dur.CamZoom).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            Cam.DOMoveZ(-17f, Dur.CamZoom).OnComplete(() => {  }); 
        
        
        });


        StartCoroutine(PlayAudio(NarrationClips[1], Narrations[1]));
       

        showBattle();
        yield return new WaitForSeconds(10);

        
        LightAS.PlayOneShot(LighteningAudio[1]);

        Lightening.SetActive(true);
        yield return new WaitForSeconds(1);

        StartFightScene();

    }
    IEnumerator PlayAudio(AudioClip AC, string str)
    {

        Narrtext.gameObject.SetActive(true);

        Narrtext.text = "";
        Narrtext.text = str;

        Narr_AS.Stop();
        Narr_AS.PlayOneShot(AC);
        yield return new WaitUntil(() => Narr_AS.isPlaying == false);

        yield return new WaitForSeconds(1);
        Narrtext.gameObject.SetActive(false);

    }

    void showBattle()
    {

        foreach (var item in BattleSceneSpr)
        {
            item.DOFade(1, 2);
        }
        


    }


    void DayToNight() 
    {

        NightSpr[0].DOFade(1, Dur.DayToNight);
        NightSpr[1].DOFade(1, Dur.DayToNight);
        NightSpr[2].DOFade(1, Dur.DayToNight);
        NightSpr[3].DOFade(1, Dur.DayToNight);
        NightSpr[4].DOFade(1, Dur.DayToNight);
        NightSpr[5].DOFade(1, Dur.DayToNight);
        NightSpr[6].DOFade(1, Dur.DayToNight);
        //NightSpr[7].DOFade(1, Dur.DayToNight);

        DaySpr[0].DOFade(0, Dur.DayToNight);
        DaySpr[1].DOFade(0, Dur.DayToNight);
        DaySpr[2].DOFade(0, Dur.DayToNight);
        DaySpr[3].DOFade(0, Dur.DayToNight);
        DaySpr[4].DOFade(0, Dur.DayToNight);
        DaySpr[5].DOFade(0, Dur.DayToNight);


    }



    void StartFightScene()
    {
        Cam.DOMoveZ(-10f, Dur.CamZoom/2).OnComplete(() => 
        {


           
           

            twoEnemyScene.Enemy[0].DOFade(1, 1);
            twoEnemyScene.Soldier[0].DOFade(1, 1);
            twoEnemyScene.BushOnGround.DOFade(0, 1);

            StartCoroutine(PlayAudio(NarrationClips[2], Narrations[2]));


        });
        


    }





}
