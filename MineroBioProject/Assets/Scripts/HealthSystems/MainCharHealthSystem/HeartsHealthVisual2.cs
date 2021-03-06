
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Script that keeps controll of all the visuals in the health system of the player
 */
public class HeartsHealthVisual2 : MonoBehaviour
{
    public static HeartsHealthSystem heartsHealthSystemStatic;

    public Sprite heart0Sprite;
    public Sprite heart1Sprite;
    public Sprite heart2Sprite;
    public Sprite heart3Sprite;
    public Sprite heart4Sprite;
    public GameObject DeathUI;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
        heartsHealthSystem = new HeartsHealthSystem(3);
        SetHeartsHealthSystem(heartsHealthSystem);
    }

    /*
     * Sets up the visual for the health system with the amount of health given in the health system
     */
    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        this.heartsHealthSystem = heartsHealthSystem;
        heartsHealthSystemStatic = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        int row = 0;
        int col = 0;
        int colMax = 10;
        float rowColSize = 30f;

        for (int i = 0; i < heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            Vector2 heartAnchoredPosition = new Vector2(col * rowColSize, -row * rowColSize);
            CreateHeartImage(heartAnchoredPosition).SetHeartFraments(heart.GetFragmentAmount());

            col++;
            if (col >= colMax)
            {
                row++;
                col = 0;
            }
        }

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        die();
    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        // Hearts health system was healed. Refreshes the hearts UI. 
        RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        // Hearts health system was damaged. Refreshes the hearts UI. 
        RefreshAllHearts();
    }

    private void die()
    {
        PlayerController.isGamePaused = true;
        DeathUI.SetActive(true);
        Time.timeScale = 0;
    }

    /*
     * Changes the visual health depending on how much health the player has left
     */
    private void RefreshAllHearts()
    {
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();

        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartsHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFraments(heart.GetFragmentAmount());
        }
    }

 
    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        // Creates a game Object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));

        // Set as child of this transform
        heartGameObject.transform.SetParent(transform);
        heartGameObject.transform.localPosition = Vector3.zero;
        heartGameObject.transform.localScale = Vector3.one;

        // Locate and Size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);

        // Set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart4Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
    }


    // Class that represents a single Heart
    public class HeartImage
    {
        private int fragments;
        private Image heartImage;
        private HeartsHealthVisual2 heartsHealthVisual;

        public HeartImage(HeartsHealthVisual2 heartsHealthVisual, Image heartImage)
        {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
        }

        public void SetHeartFraments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0: heartImage.sprite = heartsHealthVisual.heart0Sprite; break;
                case 1: heartImage.sprite = heartsHealthVisual.heart1Sprite; break;
                case 2: heartImage.sprite = heartsHealthVisual.heart2Sprite; break;
                case 3: heartImage.sprite = heartsHealthVisual.heart3Sprite; break;
                case 4: heartImage.sprite = heartsHealthVisual.heart4Sprite; break;
            }
        }

        public int GetFragmentAmount()
        {
            return fragments;
        }

        public void AddHeartVisualFragment()
        {
            SetHeartFraments(fragments + 1);
        }
    }
}
