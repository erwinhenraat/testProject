using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateToDistance : MonoBehaviour
{
    [SerializeField] private Transform objectToAnimate;
    [SerializeField] private Transform target;
    [SerializeField] private float duration = 1f;

    private Vector2 startPoint;
    private Vector2 startScale;
    private bool isLerping = false;
    private float elapsed = 0;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        isLerping = false;
        elapsed = 0;
        Debug.Log("duration : " + duration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isLerping) {
            TriggerAnimation();
        }
        if (isLerping) {
            if (direction > 0)
            {
                elapsed += Time.deltaTime;
            }
            else 
            {
                elapsed -= Time.deltaTime;           
            }
            
            //normalised = (value-minimum) / (maximum - minimum);
            float elapsedNormalised = (elapsed - 0) / (duration - 0);
            
            //Move Object to target
            MoveToTarget(elapsedNormalised);

            //Scale object to target, starting scale of sprite in unity must be 1!
            //ScaleToTarget(elapsedNormalised);

            
            if (direction == 1 && elapsedNormalised >= 1) {
                //start retracting
                direction = -1;

            }
            if (direction < 0 && elapsedNormalised <= 0) {
                isLerping = false;
            }               
                
        }
    }
    void TriggerAnimation()
    {
        direction = 1;
        elapsed = 0;
        isLerping = true;
        startPoint = objectToAnimate.position;
        startScale = objectToAnimate.localScale;
        FaceTarget();
    }
    void ScaleToTarget(float progress) {        
        Vector2 distance = startPoint - (Vector2)target.transform.position;
        //scale object to position
        Vector2 scale = Vector2.Lerp(startScale, new Vector2(startScale.x , distance.magnitude), progress);
        objectToAnimate.localScale = scale;
    }
    
    void MoveToTarget(float progress)
    {
        //Move object position
        Vector2 movement = Vector2.Lerp(startPoint, target.transform.position, progress);
        objectToAnimate.transform.position = movement;
    }
    void FaceTarget() {
        //objectToAnimate will rotate towards target
        Vector2 dist = new Vector2(
            target.transform.position.x - objectToAnimate.transform.position.x,
            target.transform.position.y - objectToAnimate.transform.position.y
            );

        
        float degrees = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg - 90;
        objectToAnimate.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }
   
}
