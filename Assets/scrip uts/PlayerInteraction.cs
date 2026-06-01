using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Object")]
    public GameObject handObject;
    public GameObject keyItem;
    public Transform chestObject;

    [Header("Pengaturan")]
    public float interactDistance = 4f;
    public string nextSceneName = "Level2";

    private bool chestOpened = false;

    void Start()
    {
        if (keyItem != null)
        {
            keyItem.SetActive(false);
        }
    }

    void Update()
    {
        if (chestObject == null) return;

        float distance = Vector3.Distance(transform.position, chestObject.position);

        if (distance <= interactDistance && !chestOpened)
        {
            Debug.Log("Tekan E untuk membuka peti");

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenChest();
            }
        }
    }

    void OpenChest()
    {
        chestOpened = true;

        if (handObject != null)
        {
            handObject.SetActive(true);
        }

        if (keyItem != null)
        {
            keyItem.transform.position = chestObject.position + new Vector3(0, 1.2f, 0);
            keyItem.SetActive(true);
        }

        Debug.Log("Level 1 berhasil! Lanjut ke Level 2");

        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneName);
    }
}