
using UnityEngine;
using JMRSDK.InputModule;
using System;
using UnityEngine.UI;
using System.Collections;

public class JioInputManager : MonoBehaviour, ISelectClickHandler, IManipulationHandler, ISwipeHandler
{
    public static JioInputManager Instance;
    public GameObject JIOInputManager;
    JMRInputManager jmrInputManager;
    public event Action<float> SelectClick;
    public event Action UserSwipedLeft, UserSwipedRight;
    public event Action Touched;
   
    public void OnSelectClicked(SelectClickEventData eventData)
    {

       
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
       
        jmrInputManager = JIOInputManager.GetComponent<JMRInputManager>();
        jmrInputManager.AddGlobalListener(gameObject);
    }




    // Update is called once per frame
    void Update()
    {

        ProcesskeyboardInputs();
        ProcessJioInputs();
 
    }
    private void ProcesskeyboardInputs()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            if (Touched != null)
            {
                Touched();
            }
           
        }
    }
    private void ProcessJioInputs()
    {
        Vector2 TouchData = JMRInteraction.GetTouch();
        if(TouchData.x>0 || TouchData.y > 0)
        {
            if (Touched != null)
            {
                Touched();
            }
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

}

