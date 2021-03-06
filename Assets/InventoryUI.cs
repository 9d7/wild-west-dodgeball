using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private int numOfChambers = 6;
    private int index = 0;
    private bool isMoving;
    [SerializeField] private Transform chamberTransform;
    [SerializeField] private ProjectileTypes projectileTypes;
    private ProjectileType[] inventory;
    private string[] inventoryKey;
    [SerializeField] private Image[] projectileImages;

    private float degreesPerChamber;
    // Start is called before the first frame update
    void Awake()
    {
        degreesPerChamber = 360f / numOfChambers;
        inventory = new ProjectileType[6];
        inventoryKey = new string[6];
    }

    public void PickUp(string key)
    {
        projectileImages[index].enabled = true;
        inventory[index] = projectileTypes[key].GetValueOrDefault();
        inventoryKey[index] = key;
        projectileImages[index].sprite = inventory[index].sprite;
    }

    public Sprite GetCurrentSprite()
    {
        return projectileImages[index].sprite;
    }

    public string UseCurrent()
    {
        if (projectileImages[index].enabled)
        {
            projectileImages[index].enabled = false;
            projectileImages[index].sprite = null;
            return inventoryKey[index];
        }

        return null;
    }


    public void UpSlot()
    {
        if (isMoving)
        {
            return;
        }

        index = index == 5 ? 0 : index + 1;
        StartCoroutine(RotateDegrees(-60f, 0.3f));
    }

    public void DownSlot()
    {
        if (isMoving)
        {
            return;
        }

        index = index == 0 ? 5 : index - 1;
        StartCoroutine(RotateDegrees(60f, 0.3f));
    }

    private IEnumerator RotateDegrees(float degrees, float time)
    {
        isMoving = true;
        float startDegrees = chamberTransform.rotation.eulerAngles.z;
        float goalDegrees = chamberTransform.rotation.eulerAngles.z + degrees;
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            chamberTransform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startDegrees, goalDegrees, t / time));
            yield return new WaitForEndOfFrame();
        }
        chamberTransform.rotation = Quaternion.Euler(0, 0, goalDegrees);
        isMoving = false;
    }
}
