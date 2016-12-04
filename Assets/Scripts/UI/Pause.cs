using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {

    Toggle tgl;

    void Start()
    {
        tgl = GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(OnTggle);
    }

    public void OnTggle(bool selected)
    {
        Time.timeScale = selected ? 0 : 1;
    }
}
