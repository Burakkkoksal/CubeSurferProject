using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyMenu : MonoBehaviour
{
    private string policyKey = "policy";
    // Start is called before the first frame updates
    void Start()
    {
        var accepted = PlayerPrefs.GetInt(policyKey, 0);
        if (accepted==1)
            return;

        SimpleGDPR.ShowDialog(new TermsOfServiceDialog().
                SetTermsOfServiceLink("https://developer.huawei.com/consumer/en/doc/app/privacy-label").
                SetPrivacyPolicyLink("https://developer.huawei.com/consumer/en/doc/app/privacy-label"),
                onMenuClosed);
    }

    private void onMenuClosed()
    {
        Debug.LogWarning("Policy Accepted");
        PlayerPrefs.SetInt(policyKey, 1);

    }
}
