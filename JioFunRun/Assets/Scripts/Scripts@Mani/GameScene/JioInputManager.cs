
using UnityEngine;
using JMRSDK.InputModule;
using System;
using UnityEngine.UI;
using System.Collections;

public class JioInputManager : MonoBehaviour, ISelectClickHandler, IManipulationHandler, ISwipeHandler,ISelectHandler
{
    public static JioInputManager Instance;
    public GameObject JIOInputManager;
    JMRInputManager jmrInputManager;
    public event Action<float> SelectClick;
    public event Action UserSwipedLeft, UserSwipedRight;
<<<<<<< Updated upstream:JioFunRun/Assets/Scripts/Scripts@Mani/GameScene/JioInputManager.cs
    public event Action Touched;
=======
    public event Action Touched,StayIdle;
    private bool isSelectDown = false;
>>>>>>> Stashed changes:JioFunRun/Assets/Scripts/GameScene/JioInputManager.cs
   
    public void OnSelectClicked(SelectClickEventData eventData)
    {

       
    }

    public void OnSelectDown(SelectEventData eventData)
    {
        isSelectDown = true;
    }

    public void OnSelectUp(SelectEventData eventData)
    {
        isSelectDown = false;
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

<<<<<<< Updated upstream:JioFunRun/Assets/Scripts/Scripts@Mani/GameScene/JioInputManager.cs



    // Update is called once per frame
    void Update()
    {

        ProcesskeyboardInputs();
        ProcessJioInputs();
 
=======
    void Update()
    {

       // ProcesskeyboardInputs();
        ProcessJioInputs(); 
>>>>>>> Stashed changes:JioFunRun/Assets/Scripts/GameScene/JioInputManager.cs
    }
    private void ProcesskeyboardInputs()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            if (Touched != null)
            {
                Touched();
<<<<<<< Updated upstream:JioFunRun/Assets/Scripts/Scripts@Mani/GameScene/JioInputManager.cs
            }
           
=======
            }      
           
        }
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (StayIdle != null)
            {
                StayIdle();
            }
>>>>>>> Stashed changes:JioFunRun/Assets/Scripts/GameScene/JioInputManager.cs
        }

        
    }
    private void ProcessJioInputs()
    {
<<<<<<< Updated upstream:JioFunRun/Assets/Scripts/Scripts@Mani/GameScene/JioInputManager.cs
        Vector2 TouchData = JMRInteraction.GetTouch();
        if(TouchData.x>0 || TouchData.y > 0)
=======
   /*     Vector2 TouchData = JMRInteraction.GetTouch();
        if(TouchData.sqrMagnitude > 0)
        {           
                    
        }
        else
>>>>>>> Stashed changes:JioFunRun/Assets/Scripts/GameScene/JioInputManager.cs
        {
                StayIdle();       
        }*/

        if(isSelectDown)
        {
            if(Touched != null)
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
      //  UserSwipedLeft();
    }

    public void OnSwipeRight(SwipeEventData eventData, float value)
    {
      //  UserSwipedRight();
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

