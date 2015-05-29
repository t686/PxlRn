using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ConsoleBase : MonoBehaviour
{
    protected string status = "Ready";

    protected string lastResponse = "";
    public GUIStyle textStyle = new GUIStyle();
    protected Texture2D lastResponseTexture;

    protected Vector2 scrollPosition = Vector2.zero;
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
    protected int buttonHeight = 60;
    protected int mainWindowWidth = Screen.width - 30;
    protected int mainWindowFullWidth = Screen.width;
#else
    protected int buttonHeight = 24;
    protected int mainWindowWidth = 500;
    protected int mainWindowFullWidth = 530;
#endif

    virtual protected void Awake()
    {
        textStyle.alignment = TextAnchor.UpperLeft;
        textStyle.wordWrap = true;
        textStyle.padding = new RectOffset(10, 10, 10, 10);
        textStyle.stretchHeight = true;
        textStyle.stretchWidth = false;
    }

    protected bool Button(string label)
    {
        return GUILayout.Button(
            label,
            GUILayout.MinHeight(buttonHeight),
            GUILayout.MaxWidth(mainWindowWidth)
        );
    }

    protected void LabelAndTextField(string label, ref string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.MaxWidth(150));
        text = GUILayout.TextField(text);
        GUILayout.EndHorizontal();
    }

    protected bool IsHorizontalLayout()
    {
        #if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
        return Screen.orientation == ScreenOrientation.Landscape;
        #else
        return true;
        #endif
    }

    protected int TextWindowHeight
    {
        get
        {
            #if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
            return IsHorizontalLayout() ? Screen.height : 85;
            #else
            return Screen.height;
            #endif
        }
    }


    protected void Callback(FBResult result)
    {
        lastResponseTexture = null;
        // Some platforms return the empty string instead of null.
        if (!String.IsNullOrEmpty (result.Error))
        {
            lastResponse = "Error Response:\n" + result.Error;
        }
        else if (!String.IsNullOrEmpty (result.Text))
        {
            lastResponse = "Success Response:\n" + result.Text;
        }
        else if (result.Texture != null)
        {
            lastResponseTexture = result.Texture;
            lastResponse = "Success Response: texture\n";
        }
        else
        {
            lastResponse = "Empty Response\n";
        }
    }

    protected void AddCommonFooter()
    {
        var textAreaSize = GUILayoutUtility.GetRect(640, TextWindowHeight);

        #if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
        GUI.TextArea(
            textAreaSize,
            string.Format(
                " AppId: {0} \n UserId: {1}\n IsLoggedIn: {2}\n {3}",
                FB.AppId,
                FB.UserId,
                FB.IsLoggedIn,
                lastResponse
            ), textStyle);
        #else
        GUI.TextArea(
            textAreaSize,
            string.Format(
                " AppId: {0} \n Facebook Dll: {1} \n UserId: {2}\n IsLoggedIn: {3}\n AccessToken: {4}\n AccessTokenExpiresAt: {5}\n {6}",
                FB.AppId,
                "Loaded Successfully",
                FB.UserId,
                FB.IsLoggedIn,
                FB.AccessToken,
                FB.AccessTokenExpiresAt,
                lastResponse
            ), textStyle);
        #endif

        if (lastResponseTexture != null)
        {
            var texHeight = textAreaSize.y + 200;
            if (Screen.height - lastResponseTexture.height < texHeight)
            {
                texHeight = Screen.height - lastResponseTexture.height;
            }
            GUI.Label(new Rect(textAreaSize.x + 5, texHeight, lastResponseTexture.width, lastResponseTexture.height), lastResponseTexture);
        }
    }

    protected void AddCommonHeader()
    {
        if (IsHorizontalLayout())
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
        }
        GUILayout.Space(5);
        GUILayout.Box("Status: " + status, GUILayout.MinWidth(mainWindowWidth));

        #if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            scrollPosition.y += Input.GetTouch(0).deltaPosition.y;
        }
        #endif
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinWidth(mainWindowFullWidth));

        GUILayout.BeginVertical();
        GUI.enabled = !FB.IsInitialized;
        if (Button("FB.Init"))
        {
            CallFBInit();
            status = "FB.Init() called with " + FB.AppId;
        }

        GUILayout.BeginHorizontal();

        GUI.enabled = FB.IsInitialized;
        if (Button("Login"))
        {
            CallFBLogin();
            status = "Login called";
        }

        GUI.enabled = FB.IsLoggedIn;
        if (Button("Get publish_actions"))
        {
            CallFBLoginForPublish();
            status = "Login (for publish_actions) called";
        }

        #if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
        if (Button("Logout"))
        {
            CallFBLogout();
            status = "Logout called";
        }
        #endif
        GUI.enabled = FB.IsInitialized;
        GUILayout.EndHorizontal();
    }

    #region FB.Init() example

    private void CallFBInit()
    {
        FB.Init(OnInitComplete, OnHideUnity);
    }

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Is game showing? " + isGameShown);
    }

    #endregion

    #region FB.Login() example

    private void CallFBLogin()
    {
        FB.Login("public_profile,email,user_friends", LoginCallback);
    }

    private void CallFBLoginForPublish()
    {
        // It is generally good behavior to split asking for read and publish
        // permissions rather than ask for them all at once.
        //
        // In your own game, consider postponing this call until the moment
        // you actually need it.
        FB.Login("publish_actions", LoginCallback);
    }

    void LoginCallback(FBResult result)
    {
        if (result.Error != null)
            lastResponse = "Error Response:\n" + result.Error;
        else if (!FB.IsLoggedIn)
        {
            lastResponse = "Login cancelled by Player";
        }
        else
        {
            lastResponse = "Login was successful!";
        }
    }

    private void CallFBLogout()
    {
        FB.Logout();
    }
    #endregion

}
