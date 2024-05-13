using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class MultipleImageDetection : MonoBehaviour
{
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;
    [SerializeField] private GameObject prefab3;

    ARTrackedImageManager trackedImageManager;

    private Dictionary<ARTrackedImage, GameObject> prefabInstances = new Dictionary<ARTrackedImage, GameObject>();

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnChanged;
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var addedImage in eventArgs.added)
        {
            SpawnPrefab(addedImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (!prefabInstances.ContainsKey(updatedImage))
            {
                SpawnPrefab(updatedImage);
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            RemovePrefab(removedImage);
        }
    }

    private void SpawnPrefab(ARTrackedImage trackedImage)
    {
        GameObject prefab = GetPrefab(trackedImage.referenceImage.name);
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
            prefabInstances.Add(trackedImage, instance);
        }
    }

    private void RemovePrefab(ARTrackedImage trackedImage)
    {
        if (prefabInstances.TryGetValue(trackedImage, out GameObject instance))
        {
            Destroy(instance);
            prefabInstances.Remove(trackedImage);
        }
    }

    private GameObject GetPrefab(string referenceImageName)
    {
        switch (referenceImageName)
        {
            case "Image1":
                return prefab1;
            case "Image2":
                return prefab2;
            case "Image3":
                return prefab3;
            default:
                return null;
        }
    }
}
