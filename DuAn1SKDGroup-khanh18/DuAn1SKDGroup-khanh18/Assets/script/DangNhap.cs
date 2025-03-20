using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DangNhapTaiKhoan : MonoBehaviour
{
    public TMP_InputField user;         // Trường nhập tên người dùng
    public TMP_InputField passwd;       // Trường nhập mật khẩu
    public TextMeshProUGUI thongbao;    // Hiển thị thông báo
    public Button dangNhapButton;       // Nút đăng nhập

    void Start()
    {
        // Kiểm tra nếu nút đăng nhập đã được gán
        if (dangNhapButton != null)
        {
            // Gán sự kiện nhấn nút cho phương thức DangNhapButton
            dangNhapButton.onClick.AddListener(DangNhapButton);
        }
        else
        {
            Debug.LogError("Dang Nhap Button not assigned!"); // Thông báo lỗi nếu biến nút không được gán
        }
    }

    public void DangNhapButton()
    {
        // Kiểm tra nếu các trường nhập liệu không trống
        if (string.IsNullOrWhiteSpace(user.text) || string.IsNullOrWhiteSpace(passwd.text))
        {
            thongbao.text = "Vui lòng điền đầy đủ thông tin!";
            return;
        }

        // Ghi log thông tin để kiểm tra
        Debug.Log($"Username: {user.text}, Password: {passwd.text}");

        // Bắt đầu coroutine để thực hiện đăng nhập
        StartCoroutine(DangNhapCoroutine());
    }

    private IEnumerator DangNhapCoroutine()
    {
        // Khởi tạo form dữ liệu
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("user", user.text);     // Thêm trường tên người dùng
        dataForm.AddField("passwd", passwd.text); // Thêm trường mật khẩu

        // Tạo kết nối đến PHP script
        using (UnityWebRequest www = UnityWebRequest.Post("https://fpl.expvn.com/dangnhap.php", dataForm))
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

                if (response == "empty")
                {
                    thongbao.text = "Vui lòng nhập đầy đủ thông tin đăng nhập";
                }
                else if (string.IsNullOrEmpty(response))
                {
                    thongbao.text = "Tài khoản hoặc mật khẩu không chính xác";
                }
                else if (response.Contains("Lỗi"))
                {
                    thongbao.text = "Không kết nối được tới server";
                }
                else
                {
                    thongbao.text = "Đăng nhập thành công";
                    PlayerPrefs.SetString("token", response); // Lưu token vào PlayerPrefs
                }
            }
        }
    }
}
