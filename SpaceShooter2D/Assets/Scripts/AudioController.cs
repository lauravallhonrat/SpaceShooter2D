using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    environmentSound,
    laserSound,
    explosionSound,
    mainMenuSound,
    speedUpSound,
    tripleShootSound,
    shieldSound,
    extraLifeSound,
    hitSound,
    tripleShotLaserSound,
    finalBossPhase1,
    finalBossPhase2,
    finalBossPhase3,
    ending
}

public class AudioController : MonoBehaviour
{

    [Header("Audios")]

    [SerializeField]
    AudioSource environmentSound;

    [SerializeField]
    AudioSource laserSound;

    [SerializeField]
    AudioSource explosionSound;

    [SerializeField]
    AudioSource mainMenuSound;

    [SerializeField]
    AudioSource speedUpSound;

    [SerializeField]
    AudioSource tripleShootSound;

    [SerializeField]
    AudioSource tripleShootLaserSound;

    [SerializeField]
    AudioSource shieldSound;

    [SerializeField]
    AudioSource extraLifeSound;

    [SerializeField]
    AudioSource hitSound;

    [SerializeField]
    AudioSource finalBossPhase1;

    [SerializeField]
    AudioSource finalBossPhase2;

    [SerializeField]
    AudioSource finalBossPhase3;

    [SerializeField]
    AudioSource ending;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PlaySound(Sounds sound)
    {

        //ENVIRONMENT
        if (sound == Sounds.environmentSound)
        {
            environmentSound.Play();
            return;
        }

        //LASER
        if (sound == Sounds.laserSound)
        {
            laserSound.Play();
            return;
        }

        if (sound == Sounds.tripleShotLaserSound)
        {
            tripleShootLaserSound.Play();
            return;
        }

        //EXPLOSION
        if (sound == Sounds.explosionSound)
        {
            explosionSound.Play();
            return;
        }

        //MAIN MENU  
        if (sound == Sounds.mainMenuSound)
        {
            mainMenuSound.Play();
            return;
        }

        //POWER UPS
        if (sound == Sounds.extraLifeSound)
        {
            extraLifeSound.Play();
            return;
        }

        if (sound == Sounds.shieldSound)
        {
            shieldSound.Play();
            return;
        }

        if (sound == Sounds.speedUpSound)
        {
            speedUpSound.Play();
            return;
        }

        if (sound == Sounds.tripleShootSound)
        {
            tripleShootSound.Play();
            return;
        }

        //DAMAGE
        if (sound == Sounds.hitSound)
        {
            hitSound.Play();
            return;
        }

        //FINAL BOSS
        if (sound == Sounds.finalBossPhase1)
        {
            finalBossPhase1.Play();
            return;
        }

        if (sound == Sounds.finalBossPhase2)
        {
            finalBossPhase2.Play();
            return;
        }

        if (sound == Sounds.finalBossPhase3)
        {
            finalBossPhase3.Play();
            return;
        }

        //ENDING
        if (sound == Sounds.ending)
        {
            ending.Play();
            return;
        }
    }

    public void StopSound(Sounds sound)
    {
        //ENVIRONMENT
        if (sound == Sounds.environmentSound)
        {
            environmentSound.Stop();
            return;
        }

        //LASER
        if (sound == Sounds.laserSound)
        {
            laserSound.Stop();
            return;
        }

        if (sound == Sounds.tripleShotLaserSound)
        {
            tripleShootLaserSound.Stop();
            return;
        }

        //EXPLOSION
        if (sound == Sounds.explosionSound)
        {
            explosionSound.Stop();
            return;
        }

        //MAIN MENU  
        if (sound == Sounds.mainMenuSound)
        {
            mainMenuSound.Stop();
            return;
        }

        //POWER UPS
        if (sound == Sounds.extraLifeSound)
        {
            extraLifeSound.Stop();
            return;
        }

        if (sound == Sounds.shieldSound)
        {
            shieldSound.Stop();
            return;
        }

        if (sound == Sounds.speedUpSound)
        {
            speedUpSound.Stop();
            return;
        }

        if (sound == Sounds.tripleShootSound)
        {
            tripleShootSound.Stop();
            return;
        }

        //DAMAGE
        if (sound == Sounds.hitSound)
        {
            hitSound.Stop();
            return;
        }

        //FINAL BOSS
        if (sound == Sounds.finalBossPhase1)
        {
            finalBossPhase1.Stop();
            return;
        }

        if (sound == Sounds.finalBossPhase2)
        {
            finalBossPhase2.Stop();
            return;
        }

        if (sound == Sounds.finalBossPhase3)
        {
            finalBossPhase3.Stop();
            return;
        }

        //ENDING
        if (sound == Sounds.ending)
        {
            ending.Stop();
            return;
        }
    }
}