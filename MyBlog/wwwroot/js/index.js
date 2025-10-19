const canvas = document.getElementById("starfield");
const ctx = canvas.getContext("2d");

let stars = [];
let shootingStars = [];
let trail = [];

function resizeCanvas() {
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
}
window.addEventListener("resize", resizeCanvas);
resizeCanvas();

function createStars(count) {
    stars = [];
    for (let i = 0; i < count; i++) {
        stars.push({
            x: Math.random() * canvas.width,
            y: Math.random() * canvas.height,
            radius: Math.random() * 1.5,
            alpha: Math.random(),
            speed: Math.random() * 0.02 + 0.005,
        });
    }
}

function drawBackground() {
    ctx.fillStyle = "rgba(0, 0, 10, 0.4)";
    ctx.fillRect(0, 0, canvas.width, canvas.height);
}

// === Hiệu ứng sao băng chuột nâng cấp (Noway Signature) ===
const maxTrail = 60;
let lastX = 0, lastY = 0;

window.addEventListener("mousemove", (e) => {
    const dx = e.clientX - lastX;
    const dy = e.clientY - lastY;
    const dist = Math.sqrt(dx * dx + dy * dy);

    // chỉ thêm điểm khi di chuyển đủ xa để mượt
    if (dist > 2) {
        trail.push({
            x: e.clientX,
            y: e.clientY,
            alpha: 1,
            angle: Math.atan2(dy, dx),
        });
        lastX = e.clientX;
        lastY = e.clientY;
    }

    if (trail.length > maxTrail) trail.shift();
});

function drawMouseTrail() {
    if (trail.length < 2) return;

    ctx.save();
    ctx.lineWidth = 3;
    ctx.lineCap = "round";
    ctx.lineJoin = "round";

    for (let i = 0; i < trail.length - 1; i++) {
        const t1 = trail[i];
        const t2 = trail[i + 1];

        // Gradient chuyển màu mượt mà
        const hue = (i * 10 + performance.now() / 10) % 360;
        const grad = ctx.createLinearGradient(t1.x, t1.y, t2.x, t2.y);
        grad.addColorStop(0, `hsla(${hue}, 100%, 70%, ${t1.alpha})`);
        grad.addColorStop(1, `hsla(${(hue + 60) % 360}, 100%, 60%, 0)`);

        ctx.strokeStyle = grad;
        ctx.beginPath();
        ctx.moveTo(t1.x, t1.y);
        ctx.lineTo(t2.x, t2.y);
        ctx.stroke();

        t1.alpha -= 0.02;
    }

    ctx.restore();
    while (trail.length && trail[0].alpha <= 0) trail.shift();
}

function drawStars() {
    drawBackground();

    // Sao nền
    for (const s of stars) {
        ctx.beginPath();
        ctx.arc(s.x, s.y, s.radius, 0, 2 * Math.PI);
        ctx.fillStyle = `rgba(255, 255, 255, ${s.alpha})`;
        ctx.fill();
        s.alpha += s.speed;
        if (s.alpha > 1 || s.alpha < 0) s.speed = -s.speed;
    }

    // Sao băng tự nhiên
    for (const star of shootingStars) {
        ctx.beginPath();
        ctx.moveTo(star.x, star.y);
        ctx.lineTo(star.x - star.len * star.dx, star.y - star.len * star.dy);
        ctx.strokeStyle = `rgba(255, 255, 255, ${star.alpha})`;
        ctx.lineWidth = 2;
        ctx.stroke();

        star.x += star.dx * star.speed;
        star.y += star.dy * star.speed;
        star.alpha -= 0.01;

        if (star.alpha <= 0) shootingStars.splice(shootingStars.indexOf(star), 1);
    }

    // Sao băng chuột
    drawMouseTrail();

    requestAnimationFrame(drawStars);
}

function randomShootingStar() {
    if (Math.random() < 0.03) {
        const x = Math.random() * canvas.width;
        const y = Math.random() * canvas.height * 0.3;
        const len = Math.random() * 80 + 30;
        const speed = Math.random() * 10 + 6;
        shootingStars.push({
            x,
            y,
            len,
            speed,
            alpha: 1,
            dx: Math.random() * 0.5 + 0.5,
            dy: Math.random() * 0.5 + 0.5,
        });
    }
}

createStars(200);
setInterval(randomShootingStar, 200);
drawStars();

// Gọi API và hiển thị danh sách bài viết
fetch("/api/home/posts")
    .then((res) => {
        if (!res.ok) {
            throw new Error(`HTTP error! status: ${res.status}`);
        }
        return res.json();
    })
    .then((posts) => {
        const container = document.getElementById("post-list");
        if (!posts || !posts.length) {
            container.innerHTML = `
                <div style="text-align: center; padding: 2rem; color: #80b3ff;">
                    <h3>Chưa có bài viết nào</h3>
                    <p>Hãy quay lại sau để xem các bài viết mới!</p>
                </div>
            `;
            return;
        }

        container.innerHTML = posts
            .map(
                (p) => `
        <div class="post-card fade-up" onclick="viewPost('${p.slug}')">
          <img src="${p.thumbnailUrl || 'https://via.placeholder.com/400x200/1e90ff/ffffff?text=No+Image'}" 
               alt="${p.title}" 
               onerror="this.src='https://via.placeholder.com/400x200/1e90ff/ffffff?text=No+Image'">
          <h3>${p.title}</h3>
          <p><strong>👤 Tác giả:</strong> ${p.author}</p>
          <div class="post-meta">
            <span class="post-date">📅 ${new Date(p.createdAt).toLocaleDateString('vi-VN')}</span>
            <span class="post-category">${p.category}</span>
          </div>
        </div>
      `
            )
            .join("");

        // Thêm hiệu ứng fade-up khi scroll
        addScrollAnimation();
    })
    .catch((err) => {
        console.error("Lỗi tải dữ liệu:", err);
        document.getElementById("post-list").innerHTML = `
            <div style="text-align: center; padding: 2rem; color: #ff6b6b;">
                <h3>Lỗi tải dữ liệu</h3>
                <p>Không thể tải danh sách bài viết. Vui lòng thử lại sau.</p>
            </div>
        `;
    });

// Hàm xem chi tiết bài viết
function viewPost(slug) {
    console.log(`Xem bài viết: ${slug}`);
    alert(`Bạn đã click vào bài viết: ${slug}`);
}

// Thêm hiệu ứng fade-up khi scroll
function addScrollAnimation() {
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
            }
        });
    }, {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    });

    document.querySelectorAll('.fade-up').forEach(el => {
        observer.observe(el);
    });
}

// Toggle menu
document.addEventListener('DOMContentLoaded', () => {
    const menu = document.querySelector('.top-left-menu');
    const icon = document.querySelector('.menu-icon');
    icon.addEventListener('click', () => {
        menu.classList.toggle('active');
    });
});
