using JMRSDK.Toolkit.UI;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using UnityEngine.SceneManagement;

public class JioAuthManager : MonoBehaviour
{
    [Header("Register")]
    public GameObject RegisterPanel;
    public JMRUIPrimaryInputField UserNameIF;
    public JMRUIPrimaryInputField EmailIF;
    public JMRUIPrimaryInputField PassWordIF;
    public JMRUIPrimaryInputField ConfirmPasswordIF;
    public TMP_Text warningRegisterText;

    [Header("Login")]
    public GameObject LoginPanel;
    public JMRUIPrimaryInputField LoginEmailIF;
    public JMRUIPrimaryInputField LoginPassWordIF;
    public TMP_Text  warningLoginText;

    [Header("ForgotPassword")]
    public GameObject ForgotPasswordPanel;
    public JMRUIPrimaryInputField ForgotEmailIF;
    public TMP_Text warningForgotText;
    public GameObject LogButton;

    [Header("GameModeSelection")]
    public GameObject GameModeSeletionPanel;

    
   


    public string DisplayName;
    private bool isNewlyCreated;
    string MyPlayFabId;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoginPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Registration Of User
    public void OnRegisterButtonClicked()
    {
        DisplayName = UserNameIF.Text;
        if (UserNameIF.Text == "")
        {
            StartCoroutine(DisplayRegisterWarningMsg(" UserName cannot be empty "));
            return;
        }
        if (EmailIF.Text == "")
        {
            StartCoroutine(DisplayRegisterWarningMsg(" Email cannot be empty "));
            return;
        }
        if (PassWordIF.Text.Length < 6)
        {
            StartCoroutine(DisplayRegisterWarningMsg(" Passwords must be atleast 6 characters long "));
            return;
        }
        if (ConfirmPasswordIF.Text != PassWordIF.Text)
        {
            StartCoroutine(DisplayRegisterWarningMsg(" Passwords does not Match "));
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = UserNameIF.Text,
            Username = UserNameIF.Text,
            Email = EmailIF.Text,
            Password = PassWordIF.Text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailed);
    }

    private void OnRegisterFailed(PlayFabError obj)
    {
        if (obj.ErrorMessage == "The display name entered is not available.")
        {
            StartCoroutine(DisplayRegisterWarningMsg(" UserName already exists. Please use a different one "));
        }
        else if (obj.ErrorMessage == "Email address not available")
        {
            StartCoroutine(DisplayRegisterWarningMsg("Email address already exists. Please use a different one"));
        }
        else if (obj.ErrorMessage == "Cannot resolve destination host")
        {
            StartCoroutine(DisplayLoginWarningMsg(" Please check your internet connection."));
        }
        else
        {
            StartCoroutine(DisplayRegisterWarningMsg(obj.ErrorMessage + " . Please Try again"));
        }
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
    {
        PlayerPrefs.SetString("Email", EmailIF.Text);
        PlayerPrefs.SetString("DisplayName", UserNameIF.Text);
        PlayerPrefs.SetString("Password", PassWordIF.Text);
        StartCoroutine(DisplayRegisterWarningMsg(" Registration Successfully"));
       // CreateUserDataInPlayfab();
        isNewlyCreated = true;
       // loginMenu.instance.OnRegistrationComplete();
        StoreEmailAndPassword(EmailIF.Text, PassWordIF.Text);
        ActivatePanels(LoginPanel);
    }
    private void StoreEmailAndPassword(string Email, string Password)
    {
        if (!PlayerPrefs.HasKey(Email))
        {
            PlayerPrefs.SetString(Email, Password);
        }
    }
    public IEnumerator DisplayRegisterWarningMsg(string msg)
    {
        warningRegisterText.text = msg;
        yield return new WaitForSeconds(3);
        warningRegisterText.text = "";
    }
    public void OnRegisterLoginButtonClicked()
    {
        ActivatePanels(LoginPanel);
    }
   
    #endregion

    #region Login

    public void OnLoginButtonClicked()
    {
        StartCoroutine(DisplayLoginWarningMsg(" Connecting To Server.... "));
        var request = new LoginWithEmailAddressRequest
        {
            Email = LoginEmailIF.Text,
            Password = LoginPassWordIF.Text

        };
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailed);
    }

    private void LoginFailed(PlayFabError obj)
    {
        if (obj.ErrorMessage == "Cannot resolve destination host")
        {
            StartCoroutine(DisplayLoginWarningMsg(" Please check your internet connection."));
        }
        else if (obj.ErrorMessage == "User not found")
        {
            StartCoroutine(DisplayLoginWarningMsg(" Email id does not exist"));
        }
        else if (obj.ErrorMessage == "Invalid email address or password")
        {
            StartCoroutine(DisplayLoginWarningMsg(" Email id or password is incorrect"));
        }
        else
        {
            StartCoroutine(DisplayLoginWarningMsg(obj.ErrorMessage));
        }
      //  LoginFailedEvent();
    }

    private void LoginSuccess(LoginResult obj)
    {
        MyPlayFabId = obj.PlayFabId;
        StartCoroutine(DisplayLoginWarningMsg(" Logged in Successfully"));
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result => {
            PlayerPrefs.SetString("UserID", result.AccountInfo.PlayFabId);
            this.DisplayName = result.AccountInfo.Username;
            PlayerPrefs.SetString("Email", LoginEmailIF.Text);
            PlayerPrefs.SetString("DisplayName", this.DisplayName);
            PlayerPrefs.SetString("Password", LoginPassWordIF.Text);
            
            StoreEmailAndPassword(LoginEmailIF.Text, LoginPassWordIF.Text);
        },
            fail => {
                Debug.Log(fail.GenerateErrorReport());
            }

            );
        ActivatePanels(GameModeSeletionPanel);
        
    }
    public void OnLoginMenuSignUpButtonClicked()
    {
        ActivatePanels(RegisterPanel);
    }

    public IEnumerator DisplayLoginWarningMsg(string msg)
    {
        warningLoginText.text = msg;
        yield return new WaitForSeconds(3);
        warningLoginText.text = "";
    }
    public void OnForgotPasswordButtonClicked()
    {
        ActivatePanels(ForgotPasswordPanel);
        LogButton.SetActive(false);
    }
    #endregion

    #region Forgot Password
    public void OnBackButtonClicked()
    {
        ActivatePanels(LoginPanel);
    }
    public void OnRecoverPasswordClicked()
    {
        if (!string.IsNullOrEmpty(ForgotEmailIF.Text))
        {
            if (PlayerPrefs.HasKey(ForgotEmailIF.Text))
            {
                string LoginPassword = PlayerPrefs.GetString(ForgotEmailIF.Text);
                SendEmail(ForgotEmailIF.Text, LoginPassword);
            }
            else
            {
                StartCoroutine(DisplayForgotWarningMsg(" Email is Incorrect "));
            }
        }
        else
        {
            StartCoroutine(DisplayForgotWarningMsg(" Enter Your Email "));
        }
    }
    public void OnForgotPanelLoginButtonClicked()
    {
        ActivatePanels(LoginPanel);
    }
    public void SendEmail(string ToAddress, string LostPassword)
    {
        string FROM = "visionxr.opc@gmail.com";
        string FROMNAME = "JIO FunRun Game";
        string TO = ToAddress;
        string SMTP_USERNAME = "AKIASW4VFOUVW7SURYU6";
        string SMTP_PASSWORD = "BEgY25hYTVQw2jRJ0fDukQAohL2VK+kH7Gx8ObHJcXrS";
        string CONFIGSET = "VisionXR";
        string HOST = "email-smtp.ap-south-1.amazonaws.com";
        int PORT = 587;
        string SUBJECT =
            "VRCarrom Info";
        string BODY =
            "<H1>Your password is</H1>" +
            "<p>" + LostPassword + "</p>";
        MailMessage message = new MailMessage();
        message.IsBodyHtml = true;
        message.From = new MailAddress(FROM, FROMNAME);
        message.To.Add(new MailAddress(TO));
        message.Subject = SUBJECT;
        message.Body = BODY;
        message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);
        using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
        {
            client.Credentials =
                new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
            client.EnableSsl = true;
            try
            {
                Debug.Log("Attempting to send email...");
                client.Send(message);
                StartCoroutine(DisplayForgotWarningMsg(" Password has been sent to your registered email "));
                LogButton.SetActive(true);
            }
            catch (Exception ex)
            {
                Debug.Log("The email was not sent.");
                StartCoroutine(DisplayForgotWarningMsg(ex.Message));
            }
        }
    }
    public IEnumerator DisplayForgotWarningMsg(string msg)
    {
        warningForgotText.text = msg;
        yield return new WaitForSeconds(5);
        warningForgotText.text = "";
    }
    #endregion
    #region Game ModeSelection Panel
    public void OnSinglePlayerButtonClicked()
    {

    }
    public void OnMultiplayerButtonClicked()
    {
        SceneManager.LoadSceneAsync(1);
        StartCoroutine(Wait(2));
    }
    #endregion

    public void ActivatePanels(GameObject PanelToBeActivated)
   {
        RegisterPanel.SetActive(PanelToBeActivated.name.Equals(RegisterPanel.name));
        LoginPanel.SetActive(PanelToBeActivated.name.Equals(LoginPanel.name));
        ForgotPasswordPanel.SetActive(PanelToBeActivated.name.Equals(ForgotPasswordPanel.name));
        GameModeSeletionPanel.SetActive(PanelToBeActivated.name.Equals(GameModeSeletionPanel.name));
   }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Find("JioNetworkManager").GetComponent<JioNetworkmanager>().PlayerNameInput.text = this.DisplayName;
    }
}
