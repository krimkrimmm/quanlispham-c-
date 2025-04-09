// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    // Kiểm tra xem có lưu trữ trạng thái audio không
    var audioStatus = sessionStorage.getItem("audioStatus");

// Nếu không có trạng thái, hoặc trạng thái là "playing", cho phép audio tự động phát
if (!audioStatus || audioStatus === "playing") {
        var audio = document.getElementById("backgroundAudio");
audio.play();

// Lưu trạng thái "playing"
sessionStorage.setItem("audioStatus", "playing");
    }

// Bắt sự kiện trước khi chuyển trang
window.addEventListener("beforeunload", function () {
        var audio = document.getElementById("backgroundAudio");

// Lưu trạng thái của audio (playing/paused)
sessionStorage.setItem("audioStatus", audio.paused ? "paused" : "playing");
    });
});
