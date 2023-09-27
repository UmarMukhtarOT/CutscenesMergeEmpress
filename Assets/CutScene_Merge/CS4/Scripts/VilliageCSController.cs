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






}


[Serializable]
public class CourtRoom
{

    public GameObject SceneParent;
    public Vector3 CamPos;
    public SpriteRenderer Bg;
    public SpriteRenderer Door;
    public SpriteRenderer Shadow;
    
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
        SetAmbience(villiage_Audio.AmbienceClips[0]);
        StartCoroutine(PlayAudio(NarrationClips[0], Narrations[0], 4));//In a nearby village
        Villiage.SceneParent.SetActive(true);
        Cam.position = Villiage.CamPos;
        yield return new WaitForSeconds(1);
        villiage_Audio.BG_AS.DOFade(0.15f, 5);
       
        Villiage.Bg.transform.DOLocalMoveX(0.6f, 5);

      
        Villiage.Hut.transform.DOLocalMoveX(01, 5);

        
       // Villiage.Hut.transform.DOMoveZ(-3, 2);

        yield return new WaitForSeconds(6);

       

        yield return StartCoroutine(CottageSceneCor());



    }

    IEnumerator CottageSceneCor()
    {
        Cam.position = cottageScene.CamPos;
        cottageScene.SceneParent.SetActive(true);
        cottageScene.Tree.transform.DOLocalMoveX(0,8);
        cottageScene.Bg.transform.DOLocalMoveX(-1.5f, 8).OnComplete(()=> 
        {
           // FadeInOut(3);


        });
        yield return new WaitForSeconds(3);
        
        

       // FadeInOut();
        yield return StartCoroutine(CourtRoomSceneCor());




    }


    IEnumerator CourtRoomSceneCor()
    {
        SetAmbience(villiage_Audio.AmbienceClips[1]);

        StartCoroutine(PlayAudio(NarrationClips[1], Narrations[1], 3));//the princess recalls

        Cam.position = courtRoom.CamPos;
        cottageScene.SceneParent.SetActive(false);
        courtRoom.SceneParent.SetActive(true);
        courtRoom.Door.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1);

       
                                                                       //  courtRoom.Shadow.DOFade(1, 2);
        courtRoom.SceneParent.transform.DOLocalMoveZ(-3,6);

         yield return new WaitForSeconds(1);


    }





   











    void FadeInOut(float time=2) 
    {
        FadeScreen.DOFade(0,0);
        FadeScreen.DOFade(1, time).OnComplete(() => 
        {
            FadeScreen.DOFade(0, time).SetDelay(1);

           

        });



    }

}
