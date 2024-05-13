using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Image_Detection : MonoBehaviour
{
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;
    [SerializeField] private GameObject prefab3;

    ARTrackedImageManager trackedImageManager;

    private GameObject prefab1Instance;
    private GameObject prefab2Instance;
    private GameObject prefab3Instance;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable() => trackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => trackedImageManager.trackedImagesChanged -= OnChanged;


    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            if (newImage.referenceImage.name == "Image1")
            {
                prefab1Instance = Instantiate(prefab1, newImage.transform.position, newImage.transform.rotation);
            }
            else if (newImage.referenceImage.name == "Image2")
            {
                prefab2Instance = Instantiate(prefab2, newImage.transform.position, newImage.transform.rotation);
            }
            else if (newImage.referenceImage.name == "Image3")
            {
                prefab3Instance = Instantiate(prefab3, newImage.transform.position, newImage.transform.rotation);
            }
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage.referenceImage.name == "Image1")
            {
                prefab1Instance.transform.position = updatedImage.transform.position;
                prefab1Instance.transform.rotation = updatedImage.transform.rotation;
            }
            else if (updatedImage.referenceImage.name == "Image2")
            {
                prefab2Instance.transform.position = updatedImage.transform.position;
                prefab2Instance.transform.rotation = updatedImage.transform.rotation;
            }
            else if (updatedImage.referenceImage.name == "Image3")
            {
                prefab3Instance.transform.position = updatedImage.transform.position;
                prefab3Instance.transform.rotation = updatedImage.transform.rotation;
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            if (removedImage.referenceImage.name == "Image1")
            {
                Destroy(prefab1Instance);
                prefab1Instance = null;
            }
            else if (removedImage.referenceImage.name == "Image2")
            {
                Destroy(prefab2Instance);
                prefab2Instance = null;
            }
            else if (removedImage.referenceImage.name == "Image3")
            {
                Destroy(prefab3Instance);
                prefab3Instance = null;
            }
        }
    }
}
