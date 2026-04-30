using UnityEngine;
using UnityEngine.EventSystems;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Sprite[] cursorSprites;
    private SpriteRenderer spriteRenderer;
    private GameObject currentUIObject;

    [SerializeField] private float offset;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        currentUIObject = eventSystem.currentSelectedGameObject;
        gameObject.transform.position = new Vector2 (currentUIObject.transform.position.x - offset, currentUIObject.transform.position.y);
        if (currentUIObject.name == "TitleAnt")
        {
            spriteRenderer.sprite = cursorSprites[1];
            offset = 2f;
        }
        else
        {
            spriteRenderer.sprite = cursorSprites[0];
            if (gameObject.name == "StartScreenCursor")
            {
                offset = 5f;
            }

            if (gameObject.name == "LevelSelectCursor")
            {
                offset = 4f;
            }

            if (currentUIObject.name.Contains("W-"))
            {
                offset = 1f;
            }

            if (currentUIObject.tag == "OptionsButton")
            {
                offset = 6f;
            }

            if (currentUIObject.tag == "LanguageButton")
            {
                offset = 2f;
            }
        }
    }
}
