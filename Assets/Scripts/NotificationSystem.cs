using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPrefab = null;
    [SerializeField] Transform notificationParent = null;
    [SerializeField] float notificationDuration = 5;
    [SerializeField] float fadeDuration = 5;

    private void OnEnable()
    {
        DialogueChoice.OnChoiceFailed += DialogueChoice_OnChoiceFailed;
    }

    private void OnDisable()
    {
        DialogueChoice.OnChoiceFailed -= DialogueChoice_OnChoiceFailed;
    }

    private void DialogueChoice_OnChoiceFailed(string error)
    {
        CreateNotification(error);
    }

    private void CreateNotification(string message)
    {
        TextMeshProUGUI notification = Instantiate(textPrefab, notificationParent);
        notification.text = message;
        StartCoroutine(RemoveNotification(notification));
    }

    IEnumerator RemoveNotification(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(notificationDuration);
        float time = 0;
        Color startColor = text.color;
        Color targetColor = text.color;
        targetColor.a = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            text.color = Color.Lerp(startColor, targetColor, time / fadeDuration);
            yield return null;
        }
        Destroy(text.gameObject);
    }
}