
using UnityEngine;
using JMRSDK.InputModule;
using System;
using UnityEngine.UI;
using System.Collections;

public class JioInputManager : MonoBehaviour, ISelectClickHandler, IManipulationHandler, ISwipeHandler, ITouchHandler
{
    public static JioInputManager Instance;
    public GameObject JIOInputManager;
    JMRInputManager jmrInputManager;
    public event Action<float> SelectClick;
    public event Action UserSwipedLeft, UserSwipedRight;
    public event Action RightTouched, LeftTouched, TouchStopped;
   




    public void OnSelectClicked(SelectClickEventData eventData)
    {

        Debug.Log("On Select Ckicked");
    }

    public void OnSelectDown(SelectEventData eventData)
    {

    }

    public void OnSelectUp(SelectEventData eventData)
    {

    }

    private void Awake()
    {
        Instance = this;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        JIOInputManager = GameObject.Find("InputManager");
        jmrInputManager = JIOInputManager.GetComponent<JMRInputManager>();
        jmrInputManager.AddGlobalListener(gameObject);
    }




    // Update is called once per frame
    void Update()
    {
        if (JIOInputManager == null)
        {
            JIOInputManager = JIOInputManager = GameObject.Find("InputManager");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RightTouched();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftTouched();
        }


    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
    
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {

        
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {

    }
   

    public void OnSwipeLeft(SwipeEventData eventData, float value)
    {
        UserSwipedLeft();
    }

    public void OnSwipeRight(SwipeEventData eventData, float value)
    {
        UserSwipedRight();
    }

    public void OnSwipeUp(SwipeEventData eventData, float value)
    {

    }

    public void OnSwipeDown(SwipeEventData eventData, float value)
    {

    }

    public void OnSwipeStarted(SwipeEventData eventData)
    {

    }

    public void OnSwipeUpdated(SwipeEventData eventData, Vector2 swipeData)
    {

    }

    public void OnSwipeCompleted(SwipeEventData eventData)
    {

    }

    public void OnSwipeCanceled(SwipeEventData eventData)
    {

    }

    public void OnTouchStart(TouchEventData eventData, Vector2 TouchData)
    {
    
    }

    public void OnTouchStop(TouchEventData eventData, Vector2 TouchData)
    {
        TouchStopped();
    }

    public void OnTouchUpdated(TouchEventData eventData, Vector2 TouchData)
    {

    }
}

