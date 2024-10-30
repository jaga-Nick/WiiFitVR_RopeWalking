using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyCon : MonoBehaviour
{
    public Transform pointA;   // 移動開始地点
    public Transform pointB;   // 移動終了地点
    public float speed = 2f;   // 移動速度

    private Vector3 target;    // 現在の移動先

    void Start()
    {
        // 初期の移動先を設定
        target = pointB.position;
    }

    void Update()
    {
        // 現在の位置からターゲットへの移動
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // ターゲットに到達した場合、移動先を切り替え、向きを反転
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // 移動先を反転
            target = (target == pointA.position) ? pointB.position : pointA.position;

            // 向きの反転
            Vector3 scale = transform.localScale;
            scale.x *= -1;  // x軸方向のスケールを反転
            transform.localScale = scale;
        }
    }
}
