document.addEventListener("DOMContentLoaded", function () {
    var toggleButton = document.querySelector(".toggle-button");
    var body = document.querySelector("body");

    toggleButton.addEventListener("click", function () {
        body.classList.toggle("dark-mode");
        body.classList.toggle("light-mode");

        // Lưu trạng thái hiện tại vào localStorage để giữ nguyên chế độ khi người dùng tải lại trang
        var mode = body.classList.contains("dark-mode") ? "dark" : "light";
        localStorage.setItem("theme", mode);
    });

    // Kiểm tra nếu đã lưu trạng thái chế độ từ trước (ví dụ khi người dùng đổi chế độ và tải lại trang)
    var savedTheme = localStorage.getItem("theme");
    if (savedTheme === "dark") {
        body.classList.add("dark-mode");
    } else {
        body.classList.add("light-mode");
    }
});