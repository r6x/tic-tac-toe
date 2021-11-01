using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateId : MonoBehaviour
{
    public bool empty = true;

    public char symbol = 'n';

    private Button button;

    private Games gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Canvas").GetComponent<Games>();
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(Starting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Starting()
    {
        gm.Paint(button);
    }
}
