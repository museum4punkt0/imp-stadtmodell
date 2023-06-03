using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyMaster : MonoBehaviour
{
    public float movementSpeed;
    public Vector3 velocity;
    public bool go;
    public bool movingToTarget;
    public bool landing;
    public float landingProximity = 3f;
    public Transform target;
    public ParticleSystem trailParticles;
    public ParticleSystem longTrail;
    public ParticleSystem shortTrail;
    public ButterflyRandomMovement randomMover;
    public bool lockMeIn;
    public float remainingTime;
    public int targetIndex = 0;
    float startScale;
    float targetScale;
    Vector3 startTravelPos;
    

    public void SwitchToLongTrail()
    {
        var particlesRunning = trailParticles.emission.enabled;
        shortTrail.Stop();
        if (particlesRunning) longTrail.Play();
        trailParticles = longTrail;
    }

    public void SwitchToShortTrail()
    {
        var particlesRunning = trailParticles.emission.enabled;
        longTrail.Stop();
        if (particlesRunning) shortTrail.Play();
        trailParticles = shortTrail;
    }
    // Start is called before the first frame update
    void Start()
    {
        longTrail.Stop();
        shortTrail.Stop();
        trailParticles = shortTrail; 

    }

    public void FeedSpot(ButterflySpot spot)
    {
        movementSpeed = spot.movementSpeed;
        landingProximity = spot.landingProximity;
        transform.position = spot.transform.position;
        transform.localScale = Vector3.one * spot.scale;
        randomMover.FeedSpot(spot);
        SwitchToShortTrail();
        trailParticles.Play();
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }


    public void SetLayerOrder(int number)
    {
        foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = number;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            lockMeIn = false;
            randomMover.State = ButteflyState.MovingTowardsGoal;
            go = false;
            randomMover.animator.SetTrigger("Go");
            movingToTarget = true;
            trailParticles.Play();
            velocity = (target.position - transform.position).normalized * movementSpeed;
        }
        if (movingToTarget || landing)
        { 
        if (Vector3.Distance(target.position, transform.position) > .01f)
        {
            var lerp = (transform.position - startTravelPos).magnitude / (target.transform.position - startTravelPos).magnitude;
                var scale = Mathf.Lerp(startScale, targetScale, lerp);
                transform.localScale = Vector3.one * scale;
            transform.position += velocity * Time.deltaTime;
            if (Vector3.Distance(target.position, transform.position) < landingProximity && movingToTarget)
            {
                    //transform.position = target.position;
                movingToTarget = false;
                    lockMeIn = true;
                    landing = true;
                randomMover.Land();
                SwitchToShortTrail();
                    if (randomMover.stateOnReachTarget == ButteflyState.Landed)
                    Invoke("StopParticles", 1f);
                
            }
        }
        else
        {
                landing = false;
            velocity = Vector3.zero;
        }
        if (lockMeIn)
        {
               transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 0.5f);

        }
        }
    }

    public void StartParticles()
    {
        trailParticles.Play();
    }

    public void StopParticles()
    {
        trailParticles.Stop();
    }

    public void GoToTarget(Transform gotoTransform)
    {
        SwitchToLongTrail();
    }

    private void GoToTargetInternal(ButterflySpot gotoSpot)
    {
        startTravelPos = transform.position;
        target.position = gotoSpot.transform.position;
        startScale = transform.localScale.x;
        targetScale = gotoSpot.scale;
        movementSpeed = gotoSpot.movementSpeed;
        landingProximity = gotoSpot.landingProximity;
        randomMover.stateOnReachTarget = gotoSpot.stateOnReachTarget;
        go = true;
    }

    public void GoToTargetWithShortTrail(ButterflySpot gotoSpot)
    {
        SwitchToShortTrail();
        GoToTargetInternal(gotoSpot);
    }

    public void GoToTarget(ButterflySpot gotoSpot)
    {
        SwitchToLongTrail();
        GoToTargetInternal(gotoSpot);
    }

    public void SetStateOnLand(int state)
    {
        randomMover.stateOnReachTarget = (ButteflyState)state;
    }
}
