using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public sealed class GameGroupsConsole : ConsoleBase
{
    #region Gamer Groups example

    public string GamerGroupName = "Test group";
    public string GamerGroupDesc = "Test group for testing.";
    public string GamerGroupPrivacy = "closed";
    public string GamerGroupAdmin = "";
    public string GamerGroupCurrentGroup = "";

    void GroupCreateCB(FBResult result)
    {
        Callback (result);

        if(result.Text != null)
        {
            var parameters = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.Text);
            if (parameters.ContainsKey ("id"))
                GamerGroupCurrentGroup = (string)parameters ["id"];
        }
    }

    void GroupDeleteCB(FBResult result)
    {
        Callback (result);
        GamerGroupCurrentGroup = "";
    }

    void GetAllGroupsCB(FBResult result)
    {
        if(!String.IsNullOrEmpty(result.Text))
        {
            lastResponse = result.Text;
            var parameters = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.Text);
            if (parameters.ContainsKey ("data"))
            {
                var dataArray = (List<object>)parameters ["data"];

                if (dataArray.Count > 0)
                {
                    var firstGroup = (Dictionary<string, object>)dataArray[0];
                    GamerGroupCurrentGroup = (string)firstGroup ["id"];

                }
            }
        }
        if (!String.IsNullOrEmpty(result.Error))
        {
            lastResponse = result.Error;
        }
    }

    private void CallFbGetAllOwnedGroups()
    {
       FB.API (FB.AppId + "/groups", Facebook.HttpMethod.GET, GetAllGroupsCB);
    }

    private void CallFbGetUserGroups()
    {
        FB.API ("/me/groups?parent=" + FB.AppId, Facebook.HttpMethod.GET, Callback);
    }

    private void CallCreateGroupDialog()
    {
        FB.GameGroupCreate (
            GamerGroupName,
            GamerGroupDesc,
            GamerGroupPrivacy,
            GroupCreateCB);
    }

    private void CallJoinGroupDialog()
    {
        FB.GameGroupJoin (
            GamerGroupCurrentGroup,
            GroupCreateCB);
    }

    private void CallFbPostToGamerGroup()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict["message"] = "herp derp a post";

        FB.API (GamerGroupCurrentGroup + "/feed", Facebook.HttpMethod.POST, Callback, dict);
    }

    #endregion

    #region GUI

    void OnGUI()
    {
        AddCommonHeader ();

        GUI.enabled = FB.IsLoggedIn;

        if (Button("Get All App Managed Groups"))
        {
            CallFbGetAllOwnedGroups ();
        }

        LabelAndTextField("Group Name", ref GamerGroupName);
        LabelAndTextField("Group Description", ref GamerGroupDesc);
        LabelAndTextField("Group Privacy", ref GamerGroupPrivacy);

        if (Button("Call Create Group Dialog"))
        {
            CallCreateGroupDialog ();
        }

        LabelAndTextField ("Group To Join", ref GamerGroupCurrentGroup);
        if (Button ("Call Join Group Dialog"))
        {
            CallJoinGroupDialog ();
        }

        if (Button("Get Gamer Groups Logged in User Belongs to"))
        {
            CallFbGetUserGroups ();
        }

        GUILayout.Space(10);

        if (Button("Make Group Post As User"))
        {
            CallFbPostToGamerGroup ();
        }

        GUILayout.Space(10);

        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        if (IsHorizontalLayout())
        {
            GUILayout.EndVertical();
        }
        GUI.enabled = true;

        AddCommonFooter();

        if (IsHorizontalLayout())
        {
            GUILayout.EndHorizontal();
        }
    }

    #endregion
}
