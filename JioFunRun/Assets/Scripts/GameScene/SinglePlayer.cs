using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayer : MonoBehaviour
{
    public static SinglePlayer instance;
    public float MoveSpeed;
    public Animator PlayerAnimator;
    private Vector3 InitPos;
    public AudioSource Hit;
    Vector3 OriginalPos;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        JioInputManager.Instance.Touched += OnTouchReceived;
        JioInputManager.Instance.StayIdle += OnIdleReceived;
        InitPos = transform.position;
        OriginalPos = transform.position;
    }

    private void OnTouchReceived()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        PlayerAnimator.SetBool("Walk", true);

    }
    private void OnIdleReceived()
    {
        PlayerAnimator.SetBool("Walk", false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ColliderObj")
        {
            PlayDeadAnim();
        }
        if(collision.gameObject.tag == "OffsetTrigger")
        {
            CamFollow.instance.ChangeOffset(collision.gameObject.GetComponent<OffsetTriggerChange>().NewTrigger);
        }

        if(collision.gameObject.tag == "CheckPointTrigger")
        {
            InitPos = collision.collider.transform.position;
        }
        if(collision.gameObject.tag == "WinTrigger")
        {
            JioInputManager.Instance.Touched -= OnTouchReceived;
            JioInputManager.Instance.StayIdle -= OnIdleReceived;
            PlayerAnimator.SetBool("Walk", false);
            PlayerAnimator.SetBool("Dance", true);
            CamFollow.instance.ChangeOffset(6);
            StartCoroutine(RestartLevel());
        }
    }  

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(13);
        PlayerAnimator.SetBool("Dance", false);
        SceneManager.LoadSceneAsync(0);
    }
    public void PlayDeadAnim()
    {
        PlayerAnimator.SetBool("Death", true);
        PlayerAnimator.SetBool("Walk", false);
        JioInputManager.Instance.Touched -= OnTouchReceived;
        JioInputManager.Instance.StayIdle -= OnIdleReceived;
        Hit.Play();
        GetComponent<BoxCollider>().enabled = false;
    }

    public void ResetPosition()
    {
        GetComponent<BoxCollider>().enabled = true;
        PlayerAnimator.SetBool("Death", false);
        JioInputManager.Instance.Touched += OnTouchReceived;
        JioInputManager.Instance.StayIdle += OnIdleReceived;
        transform.position = InitPos;
        CamFollow.instance.Reset();
    }
}
