using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ImageAnimate : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float framesPerSprite = 4;
    [SerializeField] Image image;
    [HideInInspector] public bool isAnimating = false;
    int currentIndex = 0;
    int spriteCount;
    
    int frameCount = 0;

    int CurrentIndex
    {
        get { return currentIndex; }
        set
        {
            currentIndex = value;
            if (currentIndex >= spriteCount) currentIndex = currentIndex = 0;
            if (image != null) image.sprite = sprites[currentIndex];
        }
    }
    void Start()
    {
        image = GetComponent<Image>();
        currentIndex = 0;
        spriteCount = sprites.Length;
        isAnimating = true;
    }
    void Update()
    {
        if (isAnimating)
        {
            frameCount++;
            if(frameCount >= framesPerSprite)
            {
                frameCount = 0;
                CurrentIndex++;
            }
        }
    }
}
