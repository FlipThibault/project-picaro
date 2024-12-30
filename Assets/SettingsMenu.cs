using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunSettings : MonoBehaviour
{
    public TextMeshProUGUI output;
    public TMP_InputField partySize;

    public void SetPartySize()
    {
        output.text = partySize.text;
    }


}
