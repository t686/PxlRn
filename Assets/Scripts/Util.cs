using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.MiniJSON;

public class Util : ScriptableObject
{
    public static string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
    {
        string url = string.Format("/{0}/picture", facebookID);
        string query = width != null ? "&width=" + width.ToString() : "";
        query += height != null ? "&height=" + height.ToString() : "";
        query += type != null ? "&type=" + type : "";
        if (query != "") url += ("?g" + query);
        return url;
    }

    public static void FriendPictureCallback(FBResult result)
    {
        if (result.Error != null)
        {
            Debug.LogError(result.Error);
            return;
        }

        //GameStateManager.FriendTexture = result.Texture;
    }

    public static Dictionary<string, string> RandomFriend(List<object> friends)
    {
        var fd = ((Dictionary<string, object>)(friends[Random.Range(0, friends.Count - 1)]));
        var friend = new Dictionary<string, string>();
        friend["id"] = (string)fd["id"];
        friend["first_name"] = (string)fd["first_name"];
        return friend;
    }

    public static Dictionary<string, string> DeserializeJSONProfile(string response)
    {
        var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
        object nameH;
        var profile = new Dictionary<string, string>();
        if (responseObject.TryGetValue("first_name", out nameH))
        {
            profile["first_name"] = (string)nameH;
        }
        return profile;
    }
    
    public static List<object> DeserializeScores(string response) 
    {

        var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
        object scoresh;
        var scores = new List<object>();
        if (responseObject.TryGetValue ("data", out scoresh)) 
        {
            scores = (List<object>) scoresh;
        }

        return scores;
    }

    public static List<object> DeserializeJSONFriends(string response)
    {
        var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
        object friendsH;
        var friends = new List<object>();
        if (responseObject.TryGetValue("friends", out friendsH))
        {
            friends = (List<object>)(((Dictionary<string, object>)friendsH)["data"]);
        }
        return friends;
    }
    
    
    
    public static void DrawActualSizeTexture (Vector2 pos, Texture texture, float scale = 1.0f)
    {
        Rect rect = new Rect (pos.x, pos.y, texture.width * scale , texture.height * scale);
        GUI.DrawTexture(rect, texture);
    }
    public static void DrawSimpleText (Vector2 pos, GUIStyle style, string text)
    {
        Rect rect = new Rect (pos.x, pos.y, Screen.width, Screen.height);
        GUI.Label (rect, text, style);
    }
}
