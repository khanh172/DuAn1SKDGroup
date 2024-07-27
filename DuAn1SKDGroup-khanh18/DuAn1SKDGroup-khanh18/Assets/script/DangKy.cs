using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DangKy : MonoBehaviour
{
    public TMP_InputField Username; // Trường nhập tên người dùng
    public TMP_InputField Password; // Trường nhập mật khẩu
    public TextMeshProUGUI thongbao; // Hiển thị thông báo
    public Button dangKyButton; // Nút đăng ký

    void Start()
    {
        if (dangKyButton != null)
        {
            dangKyButton.onClick.AddListener(DangKyButton); // Gán sự kiện nhấn nút cho phương thức DangKyButton
        }
        else
        {
            Debug.LogError("Dang Ky Button not assigned!"); // Thông báo lỗi nếu biến nút không được gán
        }
    }

    public void DangKyButton()
    {
        if (string.IsNullOrWhiteSpace(Username.text) || string.IsNullOrWhiteSpace(Password.text))
        {
            thongbao.text = "Vui lòng điền đầy đủ thông tin!";
            return;
        }

        Debug.Log($"Username: {Username.text}, Password: {Password.text}");
        StartCoroutine(DangKyCoroutine()); // Bắt đầu Coroutine để xử lý đăng ký
    }

    private IEnumerator DangKyCoroutine()
    {
        // Khởi tạo form dữ liệu
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("user", Username.text); // Thêm trường tên người dùng
        dataForm.AddField("passwd", Password.text); // Thêm trường mật khẩu

        // Tạo kết nối đến PHP script
        using (UnityWebRequest www = UnityWebRequest.Post("https://fpl.expvn.com/dangky.php", dataForm))
        {
            // Chờ yêu cầu hoàn tất
            yield return www.SendWebRequest();

            // Kiểm tra kết quả yêu cầu
            if (www.result != UnityWebRequest.Result.Success)
            {
                // Xử lý lỗi kết nối
                Debug.LogError($"Kết nối không thành công: {www.error}");
                thongbao.text = $"Kết nối không thành công: {www.error}";
            }
            else
            {
                // Xử lý phản hồi từ server
                string response = www.downloadHandler.text.Trim(); // Lấy phản hồi và loại bỏ khoảng trắng

                switch (response)
                {
                    case "exist":
                        thongbao.text = "Tài khoản đã tồn tại";
                        break;
                    case "OK":
                        thongbao.text = "Đăng ký thành công";
                        break;
                    case "ERROR":
                        thongbao.text = "Đăng ký không thành công";
                        break;
                    default:
                        thongbao.text = $"Không kết nối được tới server: {response}";
                        break;
                }
            }
        }
    }
}
