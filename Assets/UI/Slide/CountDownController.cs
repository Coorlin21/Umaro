using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CountDownController : MonoBehaviour
{
    [SerializeField] private int CountDownTime = 3;
    [SerializeField] private TextMeshProUGUI textMesh;
    public StartControl startControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        StartCoroutine(countDown());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator countDown()
    {
        int t = CountDownTime;
        while(t > 0)
        {
            textMesh.text = t.ToString();
            yield return new WaitForSeconds(1);
            t--;
        }
        startControl.GameStart();
        gameObject.SetActive(false);
        yield break;
    }
}
