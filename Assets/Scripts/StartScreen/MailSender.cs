using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MailSender : MonoBehaviour
{
    public void SendEmail()
    {
        string email = "weegames@gmail.com";
        string subject = MyEscapeURL("I HAVE A SUGGESTION");

        Application.OpenURL("mailto:" + email + "?subject=" + subject);
    }
    string MyEscapeURL(string URL)
    {
        return UnityWebRequest.EscapeURL(URL).Replace("+", "%20");
    }
        
}
