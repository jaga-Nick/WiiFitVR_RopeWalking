using UnityEngine;

public class WindController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject windParticleObject; // パーティクルGameObjectへの参照を追加
    public float windChangeInterval = 5f;
    private float nextWindChangeTime = 0f;
    private float currentWindStrength = 0f;
    private Vector2 windDirection;

    void Update()
    {
        if (Time.time > nextWindChangeTime)
        {
            currentWindStrength = Random.Range(-0.1f, 0.1f);
            windDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            windDirection.Normalize();
            nextWindChangeTime = Time.time + windChangeInterval;
            
            // 風の角度を取得
            float angle = Mathf.Atan2(windDirection.y, windDirection.x) * Mathf.Rad2Deg;

            // 角度に応じてY軸回転を設定
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
        }

        playerController.windEffect = currentWindStrength;
    }


    void OnGUI()
    {
        GUI.Label(new Rect(500, 10, 200, 20), "風の強さ: " + currentWindStrength.ToString("F2"));
        GUI.Label(new Rect(500, 30, 200, 20), "風の方向: " + windDirection.ToString("F2"));
    }
}
