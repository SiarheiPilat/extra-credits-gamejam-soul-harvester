using TMPro;
using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    bool activated;
    RectTransform position;
    public float speed;
    Vector2 m_initialPosition;
    Vector2 m_goal = new Vector2(0.0f, -65.0f);

    void Start()
    {
        position = GetComponent<RectTransform>();
        m_initialPosition = position.anchoredPosition;
    }

    public void PopMessage(string message)
    {
        activated = false;
        position.anchoredPosition = m_initialPosition;
        messageText.text = message;
        m_goal = new Vector2(0.0f, -65.0f);
        activated = true;
    }

    void Update()
    {
        if (activated)
        {
            position.anchoredPosition = Vector2.Lerp(position.anchoredPosition, m_goal, Time.deltaTime * speed);
            //if((position.anchoredPosition.y - m_goal.y) > -1.0f)
            //{
            //    position.anchoredPosition = Vector2.Lerp(position.anchoredPosition, m_initialPosition, Time.deltaTime * speed);
            //}
            if(position.anchoredPosition.y < -63.0f)
            {
                m_goal = m_initialPosition;
            }
        }
    }
}
