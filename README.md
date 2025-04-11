# 🛍SpeakerHub - Website Cửa Hàng Bán Đồ Công Nghệ

SpeakerHub là một nền tảng thương mại điện tử giúp người dùng dễ dàng tìm kiếm, mua sắm các sản phẩm công nghệ. Dự án được xây dựng nhằm mô phỏng hệ thống quản lý bán hàng trực tuyến với các chức năng hiện đại và giao diện thân thiện.

---

## 🚀 Công Nghệ Sử Dụng

- Backend: C# 
- Frontend: HTML/CSS, JS, Bootstrap  
- Database: MySQL
- Bảo mật: Spring Security  
- Công cụ khác: Git

---

## Tính Năng Chính

### Người dùng
- Đăng ký tài khoản
  ![image](https://github.com/user-attachments/assets/69a8004f-450d-4b0b-9ae6-111082f7b5da)
  
- Đăng nhập
  ![image](https://github.com/user-attachments/assets/4e26b592-fedd-434b-acbe-3fed000eb8df)

- Đăng xuất hệ thống
  ![image](https://github.com/user-attachments/assets/295f489f-f2df-40bf-b08e-01eea2695037)

- Tìm kiếm sản phẩm theo tên hoặc danh mục
  ![image](https://github.com/user-attachments/assets/615a7f25-82d0-4e0a-a59c-96fce03b86e8)

- Thêm sản phẩm vào giỏ hàng
  ![image](https://github.com/user-attachments/assets/621aa6c0-2e15-4a64-95bf-b286bfbe2cdd)

- Quản lý giỏ hàng: cập nhật số lượng, xóa sản phẩm
  ![image](https://github.com/user-attachments/assets/407e7c06-daa2-47d3-93c4-c8ce7eee0aec)
  
- Đặt hàng và theo dõi đơn hàng
![image](https://github.com/user-attachments/assets/5d8bf994-66b5-441f-b58d-637d937f8c28)

### Quản trị viên
- Quản lý sản phẩm:
  - Xem danh sách sản phẩm
  - Tìm kiếm, thêm mới, chỉnh sửa, xóa sản phẩm
  - Kích hoạt / khóa sản phẩm

- Quản lý danh mục sản phẩm và tin tức:
  - Thêm, sửa, xóa, kích hoạt / khóa danh mục

- Quản lý đơn hàng:
  - Xem danh sách đơn hàng
  - Tìm kiếm đơn theo ngày
  - Cập nhật trạng thái đơn hàng (chờ xử lý, đang giao, hoàn tất,...)

---

## Giao Diện Minh Họa

> _Thêm ảnh chụp giao diện thực tế của hệ thống tại đây (trang chủ, giỏ hàng, quản lý sản phẩm,...)_

---

## Cài Đặt & Chạy Ứng Dụng

```bash
# 1. Clone repository
git clone https://github.com/tenban/speakerhub.git
cd speakerhub

# 2. Cấu hình database trong file application.properties

# 3. Chạy ứng dụng
./mvnw spring-boot:run
```

