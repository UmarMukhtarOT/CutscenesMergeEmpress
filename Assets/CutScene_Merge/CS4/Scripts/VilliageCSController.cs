using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


[Serializable]
public class Villiage_Audio
{

    public AudioSource BG_AS;
    public AudioSource SingleClick_AS;
    public AudioClip[] AmbienceClips;
    public AudioClip DoorOpenClip;
    public AudioClip HeyYouClip;




}


[Serializable]
public class Villiage_Scene
{

    public GameObject SceneParent;

    public Vector3 CamPos;

    public SpriteRenderer Bg;
    public SpriteRenderer Clouds;
    public SpriteRenderer Hut;






}
[Serializable]
public class CottageScene
{

    public GameObject SceneParent;

    public Vector3 CamPos;

    public SpriteRenderer Bg;
    public SpriteRenderer Tree;
    public SpriteRenderer Cottage;
    public SpriteRenderer Soldier;
    public Animator SoldierArmPivot;






}


[Serializable]
public class CourtRoom
{

    public GameObject SceneParent;
    public Vector3 CamPos;
    public SpriteRenderer Bg;
    public SpriteRenderer Door;
    public SpriteRenderer Shadow;



    public SpriteRenderer CulpFaceUp;
    public GameObject GlowEyesSceneParent;




    public SpriteRenderer[] CulpritSprs;


}








public class VilliageCSController : CutScenesCommon
{

    public Villiage_Audio villiage_Audio;
    public SpriteRenderer FadeScreen;
    [Header("                                                                   Sub Scenes")]
    [Space(20)]



    public Villiage_Scene Villiage;
    public CottageScene cottageScene;
    public CourtRoom courtRoom;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(VilliageSceneCor());




    }

    IEnumerator VilliageSceneCor()
    {
        StartCoroutine(PlayAudio(NarrationClips[0], Narrations[0], 4.5f));//In a nearby village
        SetAmbience(villiage_Audio.AmbienceClips[0]);

        Villiage.SceneParent.SetActive(true);
        Cam.position = Villiage.CamPos;
        
      

        Villiage.Bg.transform.DOLocalMoveX(0.6f, 3);


        Villiage.Hut.transform.DOLocalMoveX(01, 3).OnComplete(() => 
        {
           
           

        });
        Villiage.Hut.transform.DOMoveZ(-5, 6);


        yield return new WaitForSeconds(4);
        FadeInOut(1f);

        yield return new WaitForSeconds(1);


        AS_Ambience.DOFade(0, 2);


        yield return StartCoroutine(CottageSceneCor());



    }

    IEnumerator CottageSceneCor()
    {


        Cam.position = cottageScene.CamPos;
        cottageScene.SceneParent.SetActive(true);
        cottageScene.Tree.transform.DOLocalMoveX(0, 8);
        cottageScene.Bg.transform.DOLocalMoveX(-1.5f, 8);
        cottageScene.SoldierArmPivot.enabled = true;

        yield return new WaitForSeconds(1);

        villiage_Audio.SingleClick_AS.PlayOneShot(villiage_Audio.HeyYouClip);
        yield return new WaitForSeconds(4);



       
        yield return StartCoroutine(CourtRoomSceneCor());




    }


    IEnumerator CourtRoomSceneCor()
    {
        

        StartCoroutine(PlayAudio(NarrationClips[1], Narrations[1], 3));//the princess recalls

        Cam.position = courtRoom.CamPos;
        cottageScene.SceneParent.SetActive(false);
        courtRoom.SceneParent.SetActive(true);
        courtRoom.Door.GetComponent<Animator>().enabled = true;

        villiage_Audio.SingleClick_AS.PlayOneShot(villiage_Audio.DoorOpenClip);
        courtRoom.GlowEyesSceneParent.SetActive(true);
        yield return new WaitForSeconds(3);








        for (int i = 0; i < courtRoom.CulpritSprs.Length; i++)
        {

            courtRoom.CulpritSprs[i].DOFade(1, 2);

        }


        yield return new WaitForSeconds(3);

        courtRoom.CulpFaceUp.DOFade(1, 1);


        villiage_Audio.BG_AS.DOFade(0, 2);
        yield return new WaitForSeconds(2);
        OnComplete();
        



    }






    public void OnComplete()
    {


        PauseAllTweens();

    }



    void PauseAllTweens()
    {

        Cam.DOPause();
        cottageScene.SceneParent.transform.DOPause();
        cottageScene.Tree.transform.DOPause();
        cottageScene.Bg.DOPause();
        cottageScene.SoldierArmPivot.DOPause();
        //=======================================================

        Villiage.SceneParent.transform.DOPause();
        Villiage.Bg.transform.DOPause();
        Villiage.Hut.transform.DOPause();
        Villiage.Hut.transform.DOPause();
        FadeScreen.DOPause();


        //=======================================================



       


      

       


        AS_Ambience.DOPause();


    }









    void FadeInOut(float time = 2)
    {
        FadeScreen.DOFade(0, 0);
        FadeScreen.DOFade(1, time).OnComplete(() =>
        {
            FadeScreen.DOFade(0, time);



        });



    }







}
