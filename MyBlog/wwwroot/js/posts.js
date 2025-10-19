// wwwroot/js/posts.js

// Gọi API và hiển thị danh sách bài viết
document.addEventListener("DOMContentLoaded", () => {
    const container = document.getElementById("post-list");

    fetch("/api/home/posts")
        .then((res) => {
            if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
            return res.json();
        })
        .then((posts) => {
            // Xóa nội dung mặc định "Đang tải bài viết..."
            container.innerHTML = "";

            if (!posts || posts.length === 0) {
                container.innerHTML = `
                    <div style="text-align: center; padding: 2rem; color: #80b3ff;">
                        <h3>Chưa có bài viết nào</h3>
                        <p>Hãy quay lại sau để xem các bài viết mới!</p>
                    </div>
                `;
                return;
            }

            // Hiển thị danh sách bài viết
            posts.forEach((p) => {
                const card = document.createElement("div");
                card.classList.add("post-card", "fade-up");

                card.innerHTML = `
                    <img src="${p.thumbnailUrl || '/images/default.jpg'}" 
                         alt="${p.title}" 
                         onerror="this.src='/images/default.jpg'">

                    <h3>${p.title}</h3>
                    <p><strong>👤 Tác giả:</strong> ${p.author}</p>
                    <div class="post-meta">
                        <span class="post-date">📅 ${new Date(p.createdAt).toLocaleDateString('vi-VN')}</span>
                        <span class="post-category">${p.category}</span>
                    </div>
                `;

                // Thêm sự kiện click
                card.addEventListener("click", () => {
                    alert(`Xem bài viết: ${p.title}`);
                });

                container.appendChild(card);
            });

            // Thêm hiệu ứng xuất hiện khi cuộn
            addScrollAnimation();
        })
        .catch((err) => {
            console.error("❌ Lỗi tải dữ liệu:", err);
            container.innerHTML = `
                <div style="text-align: center; padding: 2rem; color: #ff6b6b;">
                    <h3>Lỗi tải dữ liệu</h3>
                    <p>Không thể tải danh sách bài viết. Vui lòng thử lại sau.</p>
                </div>
            `;
        });
});

// Hiệu ứng fade-up khi cuộn
function addScrollAnimation() {
    const observer = new IntersectionObserver(
        (entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    entry.target.classList.add("visible");
                }
            });
        },
        {
            threshold: 0.1,
            rootMargin: "0px 0px -50px 0px",
        }
    );

    document.querySelectorAll(".fade-up").forEach((el) => {
        observer.observe(el);
    });
}
