using UnityEngine;

public class WindController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject windParticleObject; // パーティクルGameObjectへの参照
    public float windChangeInterval = 9f; // 次の風の変化までの間隔
    private float nextWindChangeTime = 0f;
    private float currentWindStrength = 0f;
    private Vector2 windDirection;
    private float windDuration = 5f; // 風の持続時間
    private float windEndTime = 0f; // 風が止まる時間

    void Update()
    {
        // 風の持続時間が終了したら風の強さをリセット
        if (Time.time > windEndTime)
        {
            currentWindStrength = 0f;
            playerController.windEffect = currentWindStrength;
            windParticleObject.SetActive(false); // パーティクルを非表示
        }

        // 風の変更タイミングかどうかを確認
        if (Time.time > nextWindChangeTime)
        {
            // ランダムに風の強さと方向を設定
            currentWindStrength = Random.Range(-0.1f, 0.1f);
            windDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            windDirection.Normalize();

            // 風の角度に応じてパーティクルの向きを設定
            float angle = Mathf.Atan2(windDirection.y, windDirection.x) * Mathf.Rad2Deg;
            if (angle > 0)
            {
                windParticleObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (angle < 0)
            {
                windParticleObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                windParticleObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            windParticleObject.SetActive(true); // パーティクルを表示
            windEndTime = Time.time + windDuration; // 風が止む時間を設定
            nextWindChangeTime = Time.time + windChangeInterval; // 次の風の変更タイミングを設定
        }

        // 風の影響をPlayerControllerに反映
        playerController.windEffect = currentWindStrength;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(500, 10, 200, 20), "風の強さ: " + currentWindStrength.ToString("F2"));
        GUI.Label(new Rect(500, 30, 200, 20), "風の方向: " + windDirection.ToString("F2"));
    }
}
