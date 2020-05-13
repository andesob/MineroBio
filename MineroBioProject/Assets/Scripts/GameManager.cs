using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int totalMoney;
    private int HPamount;

    private bool hasPistol;
    private bool hasSniper;
    private bool tutorialFinished;
    private bool level1Finished;
    private bool level2Finished;
    private bool level3Finished;


    // Start is called before the first frame update
    void Start()
    {
        totalMoney = 0;
        HPamount = 12;

        hasPistol = false;
        hasSniper = false;
        tutorialFinished = false;
        level1Finished = false;
        level2Finished = false;
    }


    public void PickUpPistol() => hasPistol = true;

    public void PickUpSniper() => hasSniper = true;

    public void FinishedTutorial() => tutorialFinished = true;

    public void FinishedLevel1() => level1Finished = true;

    public void FinishedLevel2() => level2Finished = true;

    public void FinishedLevel3() => level3Finished = true;

    public void SetHealth(int currHealth) => HPamount = currHealth;

    public int GetHealth() => HPamount;

    public bool IsLevel1Finished() => level1Finished;

    public bool IsLevel2Finished() => level2Finished;

    public bool IsLevel3Finished() => level3Finished;

    public bool IsTutorialFinished() => tutorialFinished;

    public bool HasPistol() => hasPistol;

    public bool HasSniper() => hasSniper;

    public bool HasWeapon() => hasPistol || hasSniper;

}
