using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {
    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;


	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();
            

            foreach (UnityEngine.Touch touch in Input.touches) {
                Vector2 touchPoint = GetComponent<Camera>().ScreenToWorldPoint(touch.position);
                
 
                Collider2D hit = Physics2D.OverlapPoint(touchPoint, touchInputMask);
                if (hit) {

                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began) {
                        recipient.SendMessage("OnTouchDown", touchPoint, SendMessageOptions.DontRequireReceiver);
                       
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        recipient.SendMessage("OnTouchUp", touchPoint,SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary) {
                        recipient.SendMessage("OnTouchStay", touchPoint, SendMessageOptions.DontRequireReceiver);
                    }
                    if(touch.phase == TouchPhase.Moved) {
                        recipient.SendMessage("OnTouchMoved", touchPoint, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled) {
                        recipient.SendMessage("OnTouchExit", touchPoint, SendMessageOptions.DontRequireReceiver);
                    }
                    

                }

                foreach (GameObject g in touchesOld) {
                    if (!touchList.Contains(g)) {
                        g.SendMessage("OnTouchExit", touchPoint, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
	}

}
