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
    public SpriteRenderer Enemy;
    public SpriteRenderer Soldier;
    public SpriteRenderer FadeScreen;
    public SpriteRenderer[] ArmsOfBoth;

}
[Serializable]
public class KingScene 
{
    
    public SpriteRenderer KingSky;
    public SpriteRenderer BGMountains;
    public SpriteRenderer GroundWithSword;
    public SpriteRenderer King1;
    public SpriteRenderer King2;
    public SpriteRenderer FadeScreen;

    //public Transform KingCamera;
    public Vector3 CamPos;
    public SpriteRenderer[] AllKingSprites;
    

}


[Serializable]
public class PrincessScene
{
    public Vector3 CamPos;
    public Vector3 ZoomedInPos;
    public Vector3 CamFinalZoomPos;
    public SpriteRenderer Castle;
    public SpriteRenderer Trees;
    public SpriteRenderer Clouds;
    public SpriteRenderer Balcony;
    public SpriteRenderer FadeScreen;
    public Transform bg;
    public SpriteRenderer[] CastleSprites;
    public SpriteRenderer[] BalconySprites;

    

    

}





[Serializable]
public class Audios
{
    public AudioClip[] LighteningAudio;
    public AudioSource LightAS;
    public AudioSource AS_BG;
    public AudioSource AS_Ambience;
    public AudioSource AS_WarZone;

    public AudioClip[] Amb;


}









public class IntroSceneController : CutScenesCommon
{
    public Vector3 CamStartPos;
    public Transform Clouds;
    public Transform TreeForeGround;
    public Transform TreeWithGround;
    public Transform Rain;
    public GameObject Lightening;

    
    public Durations Dur;
    public SpriteRenderer[] NightSpr;
    public SpriteRenderer[] DaySpr;
    public SpriteRenderer[] BattleSceneSpr;
    


    public TwoEnemyScene twoEnemyScene;
    public KingScene kingScene;
    public PrincessScene princessScene;

    public Audios audios;




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
        //SetPrincessScene();
    }


    IEnumerator Cutscene()
    {

       

        
        yield return new WaitForSeconds(Dur.InitialDelay);
        
        audios.AS_BG.Play();

        SetAmbience(audios.Amb[0]);//Morning

        
        TreeForeGround.DOMoveX(-1, Dur.CamZoom * 1.5f);
        TreeWithGround.DOMoveX(0, Dur.CamZoom);
        Clouds.DOMoveX(-1.7f, Dur.CamZoom);
        Cam.DORotate(new Vector3(0, 3.75f, 0), Dur.CamZoom / 2);
        yield return new WaitForSeconds(Dur.InitialDelay);

        StartCoroutine(PlayAudio(NarrationClips[0], Narrations[0]));//Kingdom



        Cam.DOMoveZ(-15, Dur.CamZoom);
        yield return new WaitForSeconds(4);

        DayToNight();
        //yield return new WaitForSeconds(2);
        audios.LightAS.PlayOneShot(audios.LighteningAudio[0]);
        Lightening.SetActive(true);

       // yield return new WaitForSeconds(1);

        Rain.gameObject.SetActive(true);
        Rain.GetComponent<SpriteRenderer>().DOFade(1,2);
        SetAmbience(audios.Amb[2]);//Rain


        Cam.DORotate(Vector3.zero, Dur.CamZoom);
        Cam.DOMoveX(-12, Dur.CamZoom/2).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
           
        
        
        });




        StartCoroutine(PlayAudio(NarrationClips[1], Narrations[1]));//Now Faces

        showBattle();

        audios.LightAS.PlayOneShot(audios.LighteningAudio[1]);

        Lightening.SetActive(true);
        yield return new WaitForSeconds(4f);

        StartCoroutine(PlayAudio(NarrationClips[2], Narrations[2]));//As your army.. spirit forces
        yield return new WaitForSeconds(1);
      

        



        Start2EnemyScene();

        yield return new WaitForSeconds(2);
        Lightening.SetActive(false);
        
        twoEnemyScene.FadeScreen.DOFade(1,3f).OnComplete(()=> 
        {
            SetKingScene();
        
        
        });
        yield return new WaitForSeconds(8);

        SetPrincessScene();




    }
    IEnumerator PlayAudio(AudioClip AC, string str)
    {

        Narrtext.gameObject.SetActive(true);

        Narrtext.text = "";
        Narrtext.text = str;

        Narr_AS.Stop();
        Narr_AS.PlayOneShot(AC);
        yield return new WaitUntil(() => Narr_AS.isPlaying == false);

        yield return new WaitForSeconds(2);
        Narrtext.gameObject.SetActive(false);

    }

    void showBattle()
    {
        audios.AS_WarZone.Play();
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
       
        SetAmbience(audios.Amb[1]);//Night

    }



    void Start2EnemyScene()
    {
      
        twoEnemyScene.Enemy.DOFade(1, 1);
        twoEnemyScene.Soldier.DOFade(1, 1);
        twoEnemyScene.ArmsOfBoth[0].DOFade(1, 1);
        twoEnemyScene.ArmsOfBoth[1].DOFade(1, 1);
        twoEnemyScene.BushOnGround.DOFade(0, 1);
        twoEnemyScene.Enemy.transform.DOLocalMoveX(0,3);
        twoEnemyScene.Soldier.transform.DOLocalMoveX(-2.28f, 3);
        twoEnemyScene.Soldier.GetComponent<Animator>().enabled = true;
        twoEnemyScene.Enemy.GetComponent<Animator>().enabled = true;
        


    }

    void SetKingScene()
    {
        audios.AS_WarZone.DOFade(0,4);
        Cam.transform.position = kingScene.CamPos;
        Rain.gameObject.SetActive(false);
        kingScene.FadeScreen.gameObject.SetActive(true);
        SetAmbience(audios.Amb[3]);//Sand
        audios.AS_Ambience.volume = 0.2f;
        audios.AS_WarZone.Stop();
        kingScene.FadeScreen.DOFade(0, 3);

        kingScene.GroundWithSword.transform.DOLocalMoveX(-13, 12);
        kingScene.BGMountains.transform.DOLocalMoveX(-16, 12);
        kingScene.KingSky.transform.DOLocalMoveX(-15, 8);
        kingScene.KingSky.transform.DOLocalMoveY(-1, 8);
        StartCoroutine(PlayAudio(NarrationClips[3], Narrations[3])); //Heavy price
        kingScene.King1.DOFade(0, 1.5f).SetDelay(2).SetEase(Ease.InQuad);
        kingScene.King2.DOFade(1, 1.5f).SetDelay(2).SetEase(Ease.OutQuad);


    }

    void SetPrincessScene() 
    {
        kingScene.FadeScreen.gameObject.SetActive(true);
        princessScene.bg.DOLocalMoveX(-5,10).SetEase(Ease.InQuad);
        kingScene.FadeScreen.DOFade(1,2).OnComplete(()=> 
        {

            Cam.transform.position = princessScene.CamPos;


        });
        SetAmbience(audios.Amb[1]);//night
        
        princessScene.FadeScreen.gameObject.SetActive(true);
        princessScene.FadeScreen.DOFade(0, 3).OnComplete(() =>
        {
            princessScene.Trees.transform.DOLocalMoveX(4, 10).SetEase(Ease.InOutQuad);
            princessScene.Castle.transform.DOLocalMoveX(01f, 10);
            StartCoroutine(PlayAudio(NarrationClips[4], Narrations[4])); //Princess last hope
            Cam.DOMove(princessScene.ZoomedInPos, 4.5f).OnComplete(() =>
            {

                for (int i = 0; i < princessScene.CastleSprites.Length; i++)
                {

                    princessScene.CastleSprites[i].DOFade(0, 2);
                    princessScene.BalconySprites[i].DOFade(1, 2);

                }
               

                princessScene.Clouds.transform.DOLocalMoveX(-1.76f, 5);
                princessScene.Balcony.transform.DOLocalMoveX(1.5f, 5).OnComplete(()=> 
                {

                    audios.AS_Ambience.DOFade(0, 2);
                    audios.AS_BG.DOFade(0, 2);
                
                
                });
                //Cam.DOMove(princessScene.CamFinalZoomPos, 5).SetDelay(3);

            });

        });
        
       


    }














    void SetAmbience(AudioClip clip)
    {

        audios.AS_Ambience.DOFade(0, 0.4f).OnComplete(()=>
        { 
            
            audios.AS_Ambience.clip = clip;
            audios.AS_Ambience.Play();
            audios.AS_Ambience.DOFade(0.2f, 0.4f);


        });
        
       


    }


}
