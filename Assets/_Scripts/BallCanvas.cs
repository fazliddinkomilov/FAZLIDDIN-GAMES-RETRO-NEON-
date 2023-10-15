using TMPro;
using UnityEngine;

public class BallCanvas : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI text2;
    public GameObject Green;
    public GameObject Red;
    public int ball;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ball<0)
        {
            text.text = ball.ToString();
            text2.text = ball.ToString();
        }
        else
        {
            text.text = "+" + ball.ToString();
            text2.text = ball.ToString();
        }

    }
}
