using UnityEngine;

public class WindController : MonoBehaviour
{
    // プレイヤー制御スクリプトへの参照
    public PlayerController playerController;
    public float windChangeInterval = 5f; // 風が変わる時間間隔
    private float nextWindChangeTime = 0f; // 次に風が変わる時間
    private float currentWindStrength = 0f; // 現在の風の強さ
    private Vector2 windDirection; // 風の方向

    void Update()
    {
        // 一定間隔で風の強さと方向をランダムに変更
        if (Time.time > nextWindChangeTime)
        {
            currentWindStrength = Random.Range(-0.3f, 0.3f); // ランダムに-0.3〜0.3の範囲で風を設定
            windDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); // ランダムに風の方向を設定
            windDirection.Normalize(); // 方向ベクトルを正規化
            nextWindChangeTime = Time.time + windChangeInterval; // 次に風が変わる時間を設定
        }

        // プレイヤーに風の影響を反映
        playerController.windEffect = currentWindStrength;
    }

    // OnGUIメソッドで風の情報を表示
    void OnGUI()
{
    // 画面左上に風の強さと方向を表示（日本語と数字）
    GUI.Label(new Rect(500, 10, 200, 20), "風の強さ: " + currentWindStrength.ToString("F2"));
    GUI.Label(new Rect(500, 30, 200, 20), "風の方向: " + windDirection.ToString("F2"));
}
}
