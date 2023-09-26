using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


[Serializable]
public class AllClips 
{

    
    public AudioSource BG_AS;
    public AudioSource SingleClick_AS;
    public AudioClip[] AmbienceClips;
    public AudioClip Lamp;
    public AudioClip BodyFall;



}


[Serializable]
public class DoorScene
{
    public GameObject ParentObj;
    public Vector3 CamPos;
    public SpriteRenderer Princess;
    public SpriteRenderer BGOutSide;
    public SpriteRenderer CastleItems;
    public SpriteRenderer FadeScreen;
 


}

[Serializable]
public class JungleScene
{
    public Vector3 CamPos;
    public Vector3 AmeliaWalkPos;
    public Vector3 CamRotation;

    public GameObject ParentObj;
    public SpriteRenderer Bg;
    public SpriteRenderer Woods;
    public SpriteRenderer Princess;
    public SpriteRenderer FadeScreen;



}


[Serializable]
public class KnifeScene
{
    public Vector3 CamPos;
    public Vector3 AmeliaWalkPos;
    public Vector3 CamRotation;
    public Vector3 KnifEndPos;
    public Vector3 ArmRotation;
    

    public GameObject ParentObj;
    public SpriteRenderer Bg;
    public SpriteRenderer Woods;
    public SpriteRenderer Princess;
    public SpriteRenderer PrincessHand;
    public SpriteRenderer Lantern;
    public SpriteRenderer KnifeHand;
    public SpriteRenderer KnifeAttack;

    public SpriteRenderer FadeScreen;
    public Transform LntPivot;
    public Transform ArmPivot;



}










public class JungleSceneController : CutScenesCommon
{
    public AllClips allclips;

    [Header("                                                                   Sub Scenes")]
    [Space(20)]
    public DoorScene doorScene;
    public JungleScene jungleScene;
    public KnifeScene knifeScene;




    // Start is called before the first frame update
    public override void Start()
    {


        base.Start();
        StartCoroutine(CutSceneCor());
    }


    IEnumerator CutSceneCor()
    {
        SetAmbience(allclips.AmbienceClips[0]);
        yield return new WaitForSeconds(1);
        allclips.BG_AS.DOFade(0.15f, 5);




        StartDoorScene();
        yield return new WaitForSeconds(5);
        StartJungleScene();
        yield return new WaitForSeconds(4);


        StartKnifeScene();
        yield return new WaitForSeconds(10f);

        StartCoroutine(PlayAudio(NarrationClips[3], Narrations[3]));

        // allclips.SingleClick_AS.clip = allclips.Lamp;



    }








    void StartDoorScene() 
    {


        StartCoroutine(PlayAudio(NarrationClips[0], Narrations[0]));//Amelia quietly slips 
        doorScene.ParentObj.SetActive(true);
        Cam.position = doorScene.CamPos;
        doorScene.BGOutSide.transform.DOLocalMoveX(-3, 5);
        doorScene.CastleItems.transform.DOLocalMoveX(1,5);
        doorScene.Princess.transform.DOLocalMoveX(0.36f,5);
        doorScene.Princess.transform.DOLocalMoveY(-4.44f, 5).OnComplete(()=> 
        {

            doorScene.Princess.GetComponent<Animator>().enabled = false;


        });
        Cam.transform.DOLocalMoveX(-0.25f, 3);
        


    }
    void StartJungleScene()
    {

        doorScene.FadeScreen.DOFade(1, 1);
        Cam.position = jungleScene.CamPos;
        jungleScene.FadeScreen.DOFade(0, 3);
        doorScene.ParentObj.SetActive(false);
        Debug.Log("Jungle Scene Started");
        
        SetAmbience(allclips.AmbienceClips[1]);
        jungleScene.ParentObj.SetActive(true);
        
        
        StartCoroutine(PlayAudio(NarrationClips[1], Narrations[1],0.5f));//delves forest 
        jungleScene.Bg.transform.DOLocalMoveX(0.5f, 10);
        jungleScene.Woods.transform.DOLocalMoveX(-2.15f, 10);
        jungleScene.Princess.transform.DOLocalMove(jungleScene.AmeliaWalkPos, 8).OnComplete(()=> { jungleScene.Princess.GetComponent<Animator>().enabled=false; });

       // Cam.DORotate(jungleScene.CamRotation, 10);
        


    }


     void StartKnifeScene()
    {
        Debug.Log("Knife Scene Started");
        Cam.rotation = Quaternion.identity;
        knifeScene.FadeScreen.DOFade(0, 2);
        jungleScene.FadeScreen.DOFade(1, 2).OnComplete(()=>
        {
            

            jungleScene.ParentObj.SetActive(false);
            knifeScene.ParentObj.SetActive(true);
            Cam.position = knifeScene.CamPos;
            Cam.rotation = Quaternion.identity;

            StartCoroutine(PlayAudio(NarrationClips[2], Narrations[2]));
            knifeScene.Bg.transform.DOLocalMoveX(0.5f, 7).OnComplete(()=> { });
            knifeScene.Woods.transform.DOLocalMoveX(-2.15f, 10);
            knifeScene.Princess.transform.DOLocalMove(knifeScene.AmeliaWalkPos, 7).OnComplete(() => 
            {


                
                                                                           
                knifeScene.KnifeHand.transform.DOLocalMove(knifeScene.KnifEndPos,5).SetEase(Ease.OutQuad).OnComplete(()=> 
                {

                    
                    Debug.Log("attackkkkkkkkkkkkkkk");
                    knifeScene.KnifeAttack.gameObject.SetActive(true);
                    knifeScene.KnifeAttack.DOFade(1, 0);
                    knifeScene.KnifeHand.DOFade(0, 0);
                    knifeScene.Princess.GetComponent<AudioSource>().Play();
                    knifeScene.LntPivot.DOLocalMoveY(-8f, 1).OnComplete(()=> 
                    {
                        allclips.SingleClick_AS.clip = allclips.Lamp;
                        allclips.SingleClick_AS.Play();


                        knifeScene.ArmPivot.DOLocalRotate(knifeScene.ArmRotation,1f).SetEase(Ease.InQuad).OnComplete(()=> 
                        {

                            
                            

                        });

                        knifeScene.FadeScreen.DOFade(1, 2).OnComplete(() =>
                        {

                            allclips.SingleClick_AS.clip = allclips.BodyFall;
                            allclips.SingleClick_AS.Play();



                            AS_Ambience.DOFade(0, 3);
                            allclips.BG_AS.DOFade(0, 3);

                        });



                    });


                    

                });



            
            
            });





        });

       
        //  Cam.DORotate(jungleScene.CamRotation, 10);


    }







}
