using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    // プレイヤーの移動速度、ジャンプ力、バランスの回復速度、重心の揺れの強さ
    public float moveSpeed = 5f; // プレイヤーの前後移動速度
    public float jumpForce = 5f; // プレイヤーのジャンプ力
    public float balanceRecoverySpeed = 1f; // バランスが自然に戻る速さ
    public float wobbleStrength = 0.5f; // 重心の揺れの強さ
    private Vector3 respawnPoint;
    public static bool isGoal = false;
    public static int DeathCount = 0;

    public Rigidbody rb; // プレイヤーのRigidbody
    private float balance = 0f; // プレイヤーの現在の重心の傾き
    private float wobble = 0f; // 重心の揺れの値
    private bool isGrounded = true; // 地面に接しているかどうか
    private bool isRope = false; // ロープの上にいるかどうか
    private Vector3 rotationCenterOffset = new Vector3(0, -1, 0); // プレイヤーの足元を回転の中心にするためのオフセット
    private Vector3 targetPos = new Vector3(0, 0, 0); // プレイヤーの足元を回転の中心にするためのオフセット

    // 外部からの風の影響
    public float windEffect = 0f; // 風の影響度を外部のスクリプトから受け取る変数


    public Texture2D horizonTexture; // 姿勢指示器の背景画像
    public Texture2D airplaneTexture; // 中央の飛行機を描くための画像

    public static PlayerController instance;

    void Start()
    {
        respawnPoint = transform.position;  // 初期位置を初期復活地点とする
        isGoal = false;
        DeathCount = 0;
    }

    void Update()
    {
        // プレイヤーの回転を足元を中心にする（左右に10度まで傾く）
        Quaternion rotation = Quaternion.Euler(0, 0, balance * 10f); 
        rb.MoveRotation(rotation); // 回転を適用
    }

    void FixedUpdate()
    {
        // プレイヤーの前後移動（Vertical入力に基づく）
        float moveInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move); // 移動処理

        // ジャンプ処理（地面にいる時のみジャンプが可能）
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // ジャンプの力を加える
            isGrounded = false; // 地面から離れる
        }

        // 重心のバランス調整（左右の入力に基づいてバランスを調整）
        float balanceInput = Input.GetAxis("Horizontal") * -1; // 左右入力を取得
        balance += balanceInput * Time.deltaTime * 2; // 入力に応じて重心を変化

        // ロープ上にいるときの揺れと風の影響
        if (isRope)
        {
            if (moveInput != 0)
            {
                // 歩いているときの揺れを強化
                wobble = Mathf.Sin(Time.time * wobbleStrength) * 0.06f + windEffect * 0.003f;
            }
            else
            {
                // 静止時の揺れ（ロープ上）
                wobble = Mathf.Sin(Time.time * wobbleStrength) * 0.03f + windEffect * 0.001f;
            }
        }
        else
        {
            // ロープ外での揺れ（通常時）
            wobble = Mathf.Sin(Time.time * wobbleStrength) * 0.0f + windEffect * 0.0f;
        }

        balance += wobble; // 揺れをバランスに加える

        // バランスが一定範囲を超えたら、プレイヤーを倒す処理
        if (balance < -1.5f || balance > 1.5f)
        {
            Debug.Log("倒れました！"); // ゲームオーバー処理をここに追加
        }

        // バランスが自然に回復する処理（Lerpで滑らかにゼロに戻す）
        balance = Mathf.Lerp(balance, 0, Time.deltaTime * balanceRecoverySpeed);
    }

    // 地面またはロープに接触した際に呼ばれる処理
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Rope"))
        {
            isGrounded = true; // 地面またはロープに接触している
            targetPos = col.transform.position;
            targetPos.x = 0;
        }

        if (col.gameObject.CompareTag("Rope"))
        {
            isRope = true; // ロープに乗っている状態
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Checkpoint"))
        {
            respawnPoint = col.transform.position;  // チェックポイント更新
            Debug.Log("チェックポイント更新");
        }

        if (col.CompareTag("GameOverZone"))
        {
            DeathCount++;
            Respawn();
        }

        if (col.CompareTag("Goal"))
        {
            isGoal = true;
            PlayerPrefs.SetInt("Death", DeathCount);
            SceneManager.instance.GameResult();
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        //transform.position = Vector3.Lerp(transform.position, targetPos, 1f * Time.deltaTime);
    }

    // ロープから離れた時の処理
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Rope"))
        {
            isRope = false; // ロープから離れた
        }
    }

    // 姿勢指示器を描画
    void OnGUI()
    {
        float tiltAngle = balance * 30f; // 傾きを30度の範囲に変換

        // 中心点を取得（画面の中央）
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        // 姿勢指示器の背景（地平線）の描画
        GUIUtility.RotateAroundPivot(tiltAngle, new Vector2(centerX, centerY)); // 傾きに応じて回転
        GUI.DrawTexture(new Rect(centerX - 100, centerY - 100, 200, 200), horizonTexture); // 地平線テクスチャを描画
        GUI.matrix = Matrix4x4.identity; // 回転をリセット

        // 中央の飛行機のシンボルを描画
        GUI.DrawTexture(new Rect(centerX - 50, centerY - 50, 100, 100), airplaneTexture); // 飛行機のテクスチャを描画
    }
}
