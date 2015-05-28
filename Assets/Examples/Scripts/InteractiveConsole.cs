using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public sealed class InteractiveConsole : ConsoleBase
{
    #region FB.ActivateApp() example

    private void CallFBActivateApp()
    {
        FB.ActivateApp();
        Callback(new FBResult("Check Insights section for your app in the App Dashboard under \"Mobile App Installs\""));
    }

    #endregion

    #region FB.AppRequest() Friend Selector

    public string FriendSelectorTitle = "";
    public string FriendSelectorMessage = "Derp";
    private string[] FriendFilterTypes = new string[] { "None (default)", "app_users", "app_non_users" };
    private int FriendFilterSelection = 0;
    private List<string> FriendFilterGroupNames = new List<string>();
    private List<string> FriendFilterGroupIDs = new List<string>();
    private int NumFriendFilterGroups = 0;
    public string FriendSelectorData = "{}";
    public string FriendSelectorExcludeIds = "";
    public string FriendSelectorMax = "";

    private void CallAppRequestAsFriendSelector()
    {
        // If there's a Max Recipients specified, include it
        int? maxRecipients = null;
        if (FriendSelectorMax != "")
        {
            try
            {
                maxRecipients = Int32.Parse(FriendSelectorMax);
            }
            catch (Exception e)
            {
                status = e.Message;
            }
        }

        // include the exclude ids
        string[] excludeIds = (FriendSelectorExcludeIds == "") ? null : FriendSelectorExcludeIds.Split(',');

        // Filter groups
        List<object> FriendSelectorFilters = new List<object>();
        if (FriendFilterSelection > 0)
        {
            FriendSelectorFilters.Add(FriendFilterTypes[FriendFilterSelection]);
        }
        if (NumFriendFilterGroups > 0)
        {
            for (int i = 0; i < NumFriendFilterGroups; i++)
            {
                FriendSelectorFilters.Add(
                    new FBAppRequestsFilterGroup(
                        FriendFilterGroupNames[i],
                        FriendFilterGroupIDs[i].Split(',').ToList()
                    )
                );
            }
        }

        FB.AppRequest(
            FriendSelectorMessage,
            null,
            (FriendSelectorFilters.Count > 0) ? FriendSelectorFilters : null,
            excludeIds,
            maxRecipients,
            FriendSelectorData,
            FriendSelectorTitle,
            Callback
        );
    }
    #endregion

    #region FB.AppRequest() Direct Request

    public string DirectRequestTitle = "";
    public string DirectRequestMessage = "Herp";
    private string DirectRequestTo = "";

    private void CallAppRequestAsDirectRequest()
    {
        if (DirectRequestTo == "")
        {
            throw new ArgumentException("\"To Comma Ids\" must be specificed", "to");
        }
        FB.AppRequest(
            DirectRequestMessage,
            DirectRequestTo.Split(','),
            null,
            null,
            null,
            "",
            DirectRequestTitle,
            Callback
        );
    }

    #endregion

    #region FB.Feed() example

    public string FeedToId = "";
    public string FeedLink = "";
    public string FeedLinkName = "";
    public string FeedLinkCaption = "";
    public string FeedLinkDescription = "";
    public string FeedPicture = "";
    public string FeedMediaSource = "";
    public string FeedActionName = "";
    public string FeedActionLink = "";
    public string FeedReference = "";
    public bool IncludeFeedProperties = false;
    private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

    private void CallFBFeed()
    {
        Dictionary<string, string[]> feedProperties = null;
        if (IncludeFeedProperties)
        {
            feedProperties = FeedProperties;
        }
        FB.Feed(
            toId: FeedToId,
            link: FeedLink,
            linkName: FeedLinkName,
            linkCaption: FeedLinkCaption,
            linkDescription: FeedLinkDescription,
            picture: FeedPicture,
            mediaSource: FeedMediaSource,
            actionName: FeedActionName,
            actionLink: FeedActionLink,
            reference: FeedReference,
            properties: feedProperties,
            callback: Callback
        );
    }

    #endregion

    #region FB.Canvas.Pay() example

    public string PayProduct = "";

    private void CallFBPay()
    {
        FB.Canvas.Pay(PayProduct);
    }

    #endregion

    #region FB.API() example

    public string ApiQuery = "";

    private void CallFBAPI()
    {
        FB.API(ApiQuery, Facebook.HttpMethod.GET, Callback);
    }

    #endregion

    #region FB.GetDeepLink() example

    private void CallFBGetDeepLink()
    {
        FB.GetDeepLink(Callback);
    }

    #endregion

    #region FB.AppEvent.LogEvent example

    public void CallAppEventLogEvent()
    {
        FB.AppEvents.LogEvent(
            Facebook.FBAppEventName.UnlockedAchievement,
            null,
            new Dictionary<string,object>() {
                { Facebook.FBAppEventParameterName.Description, "Clicked 'Log AppEvent' button" }
            }
        );
        Callback(new FBResult(
                "You may see results showing up at https://www.facebook.com/insights/" +
                FB.AppId +
                "?section=AppEvents"
            )
        );
    }

    #endregion

    #region FB.Canvas.SetResolution example

    public string Width = "800";
    public string Height = "600";
    public bool CenterHorizontal = true;
    public bool CenterVertical = false;
    public string Top = "10";
    public string Left = "10";

    public void CallCanvasSetResolution()
    {
        int width;
        if (!Int32.TryParse(Width, out width))
        {
            width = 800;
        }
        int height;
        if (!Int32.TryParse(Height, out height))
        {
            height = 600;
        }
        float top;
        if (!float.TryParse(Top, out top))
        {
            top = 0.0f;
        }
        float left;
        if (!float.TryParse(Left, out left))
        {
            left = 0.0f;
        }
        if (CenterHorizontal && CenterVertical)
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.CenterHorizontal());
        }
        else if (CenterHorizontal)
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.CenterHorizontal());
        }
        else if (CenterVertical)
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.Left(left));
        }
        else
        {
            FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.Left(left));
        }
    }

    #endregion

    #region GUI

    override protected void Awake()
    {
        base.Awake();

        FeedProperties.Add("key1", new[] { "valueString1" });
        FeedProperties.Add("key2", new[] { "valueString2", "http://www.facebook.com" });
    }

    private void FriendFilterArea() {
#if UNITY_WEBPLAYER
        GUILayout.BeginHorizontal();
#endif
        GUILayout.Label("Filters:");
        FriendFilterSelection =
            GUILayout.SelectionGrid(FriendFilterSelection,
                                    FriendFilterTypes,
                                    3,
                                    GUILayout.MinHeight(buttonHeight));
#if UNITY_WEBPLAYER
        GUILayout.EndHorizontal();

        // Filter groups are not supported on mobile so no need to display
        // these buttons if not in web player.
        if (NumFriendFilterGroups > 0)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Filter group name:", GUILayout.MaxWidth(150));
            GUILayout.Label("IDs (comma separated):");
            GUILayout.EndHorizontal();

            int deleteGroup = -1;
            for (int i = 0; i < FriendFilterGroupNames.Count(); i++)
            {
                GUILayout.BeginHorizontal();
                FriendFilterGroupNames[i] = GUILayout.TextField(FriendFilterGroupNames[i], GUILayout.MaxWidth(150));
                FriendFilterGroupIDs[i] = GUILayout.TextField(FriendFilterGroupIDs[i]);
                if (GUILayout.Button("del", GUILayout.ExpandWidth(false)))
                {
                    deleteGroup = i;
                }
                GUILayout.EndHorizontal();
            }
            if (deleteGroup >= 0)
            {
                NumFriendFilterGroups--;
                FriendFilterGroupNames.RemoveAt(deleteGroup);
                FriendFilterGroupIDs.RemoveAt(deleteGroup);
            }
            GUILayout.EndVertical();
        }

        if (Button("Add filter group..."))
        {
            FriendFilterGroupNames.Add("");
            FriendFilterGroupIDs.Add("");
            NumFriendFilterGroups++;
        }
#endif
    }

    void OnGUI()
    {
        AddCommonHeader ();

        if (Button("Publish Install"))
        {
            CallFBActivateApp();
            status = "Install Published";
        }

        GUI.enabled = FB.IsLoggedIn;
        GUILayout.Space(10);
        LabelAndTextField("Title (optional): ", ref FriendSelectorTitle);
        LabelAndTextField("Message: ", ref FriendSelectorMessage);
        LabelAndTextField("Exclude Ids (optional): ", ref FriendSelectorExcludeIds);
        FriendFilterArea();
        LabelAndTextField("Max Recipients (optional): ", ref FriendSelectorMax);
        LabelAndTextField("Data (optional): ", ref FriendSelectorData);
        if (Button("Open Friend Selector"))
        {
            try
            {
                CallAppRequestAsFriendSelector();
                status = "Friend Selector called";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
        }
        GUILayout.Space(10);
        LabelAndTextField("Title (optional): ", ref DirectRequestTitle);
        LabelAndTextField("Message: ", ref DirectRequestMessage);
        LabelAndTextField("To Comma Ids: ", ref DirectRequestTo);
        if (Button("Open Direct Request"))
        {
            try
            {
                CallAppRequestAsDirectRequest();
                status = "Direct Request called";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
        }
        GUILayout.Space(10);
        LabelAndTextField("To Id (optional): ", ref FeedToId);
        LabelAndTextField("Link (optional): ", ref FeedLink);
        LabelAndTextField("Link Name (optional): ", ref FeedLinkName);
        LabelAndTextField("Link Desc (optional): ", ref FeedLinkDescription);
        LabelAndTextField("Link Caption (optional): ", ref FeedLinkCaption);
        LabelAndTextField("Picture (optional): ", ref FeedPicture);
        LabelAndTextField("Media Source (optional): ", ref FeedMediaSource);
        LabelAndTextField("Action Name (optional): ", ref FeedActionName);
        LabelAndTextField("Action Link (optional): ", ref FeedActionLink);
        LabelAndTextField("Reference (optional): ", ref FeedReference);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Properties (optional)", GUILayout.Width(150));
        IncludeFeedProperties = GUILayout.Toggle(IncludeFeedProperties, "Include");
        GUILayout.EndHorizontal();
        if (Button("Open Feed Dialog"))
        {
            try
            {
                CallFBFeed();
                status = "Feed dialog called";
            }
            catch (Exception e)
            {
                status = e.Message;
            }
        }
        GUILayout.Space(10);

#if UNITY_WEBPLAYER
        LabelAndTextField("Product: ", ref PayProduct);
        if (Button("Call Pay"))
        {
            CallFBPay();
        }
        GUILayout.Space(10);
#endif

        LabelAndTextField("API: ", ref ApiQuery);
        if (Button("Call API"))
        {
            status = "API called";
            CallFBAPI();
        }
        GUILayout.Space(10);
        if (Button("Take & upload screenshot"))
        {
            status = "Take screenshot";

            StartCoroutine(TakeScreenshot());
        }

        if (Button("Get Deep Link"))
        {
            CallFBGetDeepLink();
        }

        GUI.enabled = true;
        if (Button("Log FB App Event"))
        {
            status = "Logged FB.AppEvent";
            CallAppEventLogEvent();
        }

#if UNITY_WEBPLAYER
        GUI.enabled = FB.IsInitialized;
        GUILayout.Space(10);

        LabelAndTextField("Game Width: ", ref Width);
        LabelAndTextField("Game Height: ", ref Height);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Center Game:", GUILayout.Width(150));
        CenterVertical = GUILayout.Toggle(CenterVertical, "Vertically");
        CenterHorizontal = GUILayout.Toggle(CenterHorizontal, "Horizontally");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        LabelAndTextField("or set Padding Top: ", ref Top);
        LabelAndTextField("set Padding Left: ", ref Left);
        GUILayout.EndHorizontal();
        if (Button("Set Resolution"))
        {
            status = "Set to new Resolution";
            CallCanvasSetResolution();
        }
        GUI.enabled = true;
#endif

        GUILayout.Space(10);

        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        if (IsHorizontalLayout())
        {
            GUILayout.EndVertical();
        }

        AddCommonFooter();

        if (IsHorizontalLayout())
        {
            GUILayout.EndHorizontal();
        }
    }


    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        var width = Screen.width;
        var height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        byte[] screenshot = tex.EncodeToPNG();

        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
        wwwForm.AddField("message", "herp derp.  I did a thing!  Did I do this right?");

        FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
    }

    #endregion
}
