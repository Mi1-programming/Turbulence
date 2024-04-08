using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TutorialPage
{
    [Multiline] public string data;
    public Sprite image;
}
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialPage[] pages;
    [SerializeField] private UnityEvent<string> sendData;
    [SerializeField] private UnityEvent<Sprite> sendImage;
    [SerializeField] private UnityEvent onPageChange;

    private int currentPage;
    private int CurrentPage {
        get { return currentPage; }
        set {
            currentPage = value;
            onPageChange.Invoke();
        }
    }

    private void RefreshView()
    {
        TutorialPage page = pages[currentPage];
        sendData.Invoke(page.data);
        sendImage.Invoke(page.image);
    }
    public void Cycle(int amount)
    {
        CurrentPage = Mathf.Max(CurrentPage + amount, 0) % pages.Length;
        RefreshView();
    }
    private void Start()
    {
        RefreshView();
    }
}
