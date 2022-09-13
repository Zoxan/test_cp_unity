using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button button;
    public Button button2;
    public Button button3;

    void Start()
    {
        button.onClick.AddListener(OnClick);
        button2.onClick.AddListener(OnClick2);
        button3.onClick.AddListener(OnClick3);
    }

    private void OnClick()
    {
        Debug.Log("===Unity: OnClick 1");
        AndroidJavaClass unityActivityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        Debug.Log("===Unity: OnClick 2");
        AndroidJavaClass cpActivityClass = new AndroidJavaClass("com.example.unity.CloudPaymentsActivityKt");
        Debug.Log("===Unity: OnClick 3");
        AndroidJavaObject unityActivity = unityActivityClass.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log("===Unity: OnClick 4");
        cpActivityClass.CallStatic("CreateCPActivity", "1234", unityActivity);
        Debug.Log("===Unity: OnClick 5");
    }
    
    private void OnClick2()
    {
        Debug.Log("===Unity2: OnClick 1");
        AndroidJavaClass cpActivityClass = new AndroidJavaClass("com.example.unity.CloudPaymentsActivityKt");
        cpActivityClass.CallStatic("debug1", "mmm1", "mmmm2");
        Debug.Log("===Unity2: OnClick 2");
    }
    
    private void OnClick3()
    {
        Debug.Log("===Unity3: OnClick 1");
        AndroidJavaClass cpActivityClass = new AndroidJavaClass("com.example.unity.CloudPaymentsActivityKt");
        cpActivityClass.CallStatic("debug2", "mmm1", "mmmm2");
        Debug.Log("===Unity3: OnClick 2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
