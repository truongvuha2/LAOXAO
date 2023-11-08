var banners = document.querySelectorAll('.content'); // Chọn tất cả các thẻ banner
var currentBannerIndex = 0; // Chỉ mục của banner hiện tại

function showNextBanner() {
    // Ẩn thẻ banner hiện tại
    banners[currentBannerIndex].style.display = 'none';

    // Tăng chỉ mục để hiển thị thẻ banner tiếp theo
    currentBannerIndex = (currentBannerIndex + 1) % banners.length;

    // Hiển thị thẻ banner tiếp theo
    banners[currentBannerIndex].style.display = 'block';
}

// Ẩn tất cả các thẻ banner trừ thẻ đầu tiên
for (var i = 1; i < banners.length; i++) {
    banners[i].style.display = 'none';
}

// Đặt thời gian chuyển đổi banner (ví dụ: 5 giây)
setInterval(showNextBanner, 5000);


let pop_song_left = document.getElementById('pop_song_left')
let pop_song_right = document.getElementById('pop_song_right')
let pop_song = document.getElementsByClassName('pop_song')[0];

pop_song_right.addEventListener('click', () => {
    pop_song.scrollLeft += 330;
})
pop_song_left.addEventListener('click', () => {
    pop_song.scrollLeft -= 330;
})



var audioPlayer = new Audio();
var masterPlayIconPlay = document.getElementById('masterPlayIconPlay');
var masterPlayIconPause = document.getElementById('masterPlayIconPause');
var currentSongId = null;

document.addEventListener('keyup', function (event) {
    if (event.code === 'Space') {
        // Người dùng nhấn nút Space, gọi hàm togglePlayPause
        togglePlayPause();
    } else if (event.code === 'ArrowUp') {
        // Người dùng nhấn mũi lên, tăng âm lượng
        increaseVolume();
    } else if (event.code === 'ArrowDown') {
        // Người dùng nhấn mũi xuống, giảm âm lượng
        decreaseVolume();
    } else if (event.code === 'ArrowRight') {
        // Người dùng nhấn mũi trên, tua tiến bài hát
        seekForward();
    } else if (event.code === 'ArrowLeft') {
        // Người dùng nhấn mũi trái, tua lùi bài hát
        seekBackward();
    }
});

function seekForward() {
    if (audioPlayer.currentTime < audioPlayer.duration) {
        audioPlayer.currentTime += 10;
    }
}

function seekBackward() {
    if (audioPlayer.currentTime > 0) {
        audioPlayer.currentTime -= 10;
    }
}

function increaseVolume() {
    if (audioPlayer.volume < 1) {
        audioPlayer.volume += 0.1;
        updateVolumeUI(audioPlayer.volume);
    }
}

function decreaseVolume() {
    if (audioPlayer.volume > 0) {
        audioPlayer.volume -= 0.1;
        updateVolumeUI(audioPlayer.volume);
    }
}

function playAudio(audioUrl, songId, imgUrl, title) {
    if (currentSongId !== null) {
        // Hủy đăng ký sự kiện "timeupdate" của bài hát trước
        audioPlayer.removeEventListener('timeupdate', updateTimeDisplay);
        pauseAudio(currentSongId);
    }

    audioPlayer.src = audioUrl;
    audioPlayer.play();

    masterPlayIconPlay.style.display = "none";
    masterPlayIconPause.style.display = "inline";
   
    currentSongId = songId;

    // Lưu trạng thái của bài hát hiện tại và nút "Play" và "Pause" của nó
    currentSongId = songId;
    $.ajax({
        url: "/Songs/IsFavorite", // Đảm bảo rằng URL đúng
        type: "get",
        data: {
            id: currentSongId,
        },
        success: function (result) {
            // Update the page with the new data
            document.getElementById("like").style.display = "block"; // Đã sửa lỗi ở đây (documemt -> document)
            document.getElementById("unlike").style.display = "none"; // Đã sửa lỗi ở đây (documemt -> document)
        },
        error: function (result) {
            // Update the page with the new data
            document.getElementById("like").style.display = "none"; // Đã sửa lỗi ở đây (documemt -> document)
            document.getElementById("unlike").style.display = "block"; // Đã sửa lỗi ở đây (documemt -> document)
        }
    });
    // Cập nhật hình ảnh trong thẻ master_play
    var masterPlayImage = document.getElementById("masterPlayImage");
    masterPlayImage.src = imgUrl;

    document.getElementById("title").textContent = title;

    // Tìm nút "Play" và "Pause" cho bài hát hiện tại
    var playButton = document.querySelector(".playListPlay[data-song-id='" + songId + "']");
    var pauseButton = document.querySelector(".playListPause[data-song-id='" + songId + "']");

    // Hiển thị nút "Pause" và ẩn nút "Play" cho bài hát hiện tại
    playButton.style.display = "none";
    pauseButton.style.display = "inline";

    var waveElement = document.querySelector(".master_play .wave");
    waveElement.classList.add("active1");

    // Đăng ký sự kiện "timeupdate" để cập nhật thời gian
    audioPlayer.addEventListener('timeupdate', updateTimeDisplay);

    var song = {
        audioUrl: audioUrl,
        songId: songId,
        imgUrl: imgUrl,
        title: title,
        artist: "Artist Name" // Bạn có thể thay đổi thành nghệ sĩ thực tế
    };

    // Thêm bài hát vào danh sách bài hát đã phát trước đó
    addSongToRecentlyPlayed(song);
}

function pauseAudio(songId) {
    audioPlayer.pause();
    masterPlayIconPlay.style.display = "inline";
    masterPlayIconPause.style.display = "none";
    currentSongId = null;

    // Tìm nút "Play" và "Pause" cho bài hát hiện tại
    var playButton = document.querySelector(".playListPlay[data-song-id='" + songId + "']");
    var pauseButton = document.querySelector(".playListPause[data-song-id='" + songId + "']");

    // Hiển thị nút "Play" và ẩn nút "Pause" cho bài hát hiện tại
    playButton.style.display = "inline";
    pauseButton.style.display = "none";

    var waveElement = document.querySelector(".master_play .wave");
    waveElement.classList.remove("active1");

    // Hủy đăng ký sự kiện "timeupdate"
    audioPlayer.removeEventListener('timeupdate', updateTimeDisplay);
}

function updateTimeDisplay() {
    let currentStart = document.getElementById('currentStart');
    let currentEnd = document.getElementById('currentEnd');

    let music_curr = audioPlayer.currentTime;
    let music_dur = audioPlayer.duration;

    if (!isNaN(music_dur)) {
        let min1 = Math.floor(music_curr / 60);
        let sec1 = Math.floor(music_curr % 60);

        if (sec1 < 10) {
            sec1 = `0${sec1}`;
        }

        let min2 = Math.floor(music_dur / 60);
        let sec2 = Math.floor(music_dur % 60);

        if (sec2 < 10) {
            sec2 = `0${sec2}`;
        }

        currentStart.innerText = `${min1}:${sec1}`;
        currentEnd.innerText = `${min2}:${sec2}`;
    }

    let seek = document.getElementById('seek');
    let bar2 = document.getElementById('bar2');
    let dot = document.getElementsByClassName('dot')[0];

    let progressBar = parseInt((music_curr / music_dur) * 100);
    seek.value = progressBar;
    let seekbar = seek.value;
    bar2.style.width = `${seekbar}%`;
    dot.style.left = `${seekbar}%`;

    seek.addEventListener('change', () => {
        audioPlayer.currentTime = seek.value * audioPlayer.duration / 100;
    })
}

var volumeSlider = document.getElementById('vol');

volumeSlider.addEventListener('input', function () {
    var volume = volumeSlider.value / 100;
    audioPlayer.volume = volume;

    // Cập nhật UI cho thanh âm lượng
    updateVolumeUI(volume);
});

function updateVolumeUI(volume) {
    var volBar = document.querySelector('.vol_bar');
    var volDot = document.querySelector('.dot#vol_dot');

    // Cập nhật thanh thanh âm lượng và vị trí của dấu chấm
    volBar.style.width = volume * 100 + '%';
    volDot.style.left = volume * 100 + '%';

    // Cập nhật biểu tượng âm lượng (nếu cần)
    var volIcon = document.getElementById('vol_icon');
    if (volume === 0) {
        volIcon.className = 'bi bi-volume-mute-fill';
    } else if (volume < 0.5) {
        volIcon.className = 'bi bi-volume-down-fill';
    } else {
        volIcon.className = 'bi bi-volume-up-fill';
    }
}

function togglePlayPause() {
    if (audioPlayer.paused) {
        audioPlayer.play();
        masterPlayIconPlay.style.display = "none";
        masterPlayIconPause.style.display = "inline";
    } else {
        audioPlayer.pause();
        masterPlayIconPlay.style.display = "inline";
        masterPlayIconPause.style.display = "none";
    }
}

var extractedSongs = [];

fetch('/Songs/GetSongs')
    .then(response => response.json())
    .then(data => {
        // Lưu danh sách bài hát
        extractedSongs = data.map(song => {
            return {
                audioUrl: song.filePath,
                songId: song.id,
                imgUrl: song.imgUrl,
                title: song.title
            };
        });

        // Hiển thị danh sách bài hát trong bảng console
        console.log('Danh sách bài hát:', extractedSongs);
    })
    .catch(error => {
        console.error('Lỗi khi tải danh sách bài hát:', error);
    });
// Định nghĩa biến currentSongIndex và khởi tạo giá trị ban đầu (ví dụ là 0)
var currentSongIndex = 0;
function playNextSong() {
    changeSong(1); // Chuyển đến bài hát tiếp theo
}

function playPreviousSong() {
    changeSong(-1); // Chuyển đến bài hát trước đó
}

function changeSong(direction) {
    if (currentSongId !== null) {
        // Tìm bài hát hiện tại trong danh sách bằng songId
      
        var currentSong = extractedSongs.find(song => song.songId === currentSongId);

        if (currentSong) {
            // Tìm chỉ mục của bài hát hiện tại trong danh sách
            var currentIndex = extractedSongs.indexOf(currentSong);

            // Tìm chỉ mục của bài hát mới (tiếp theo hoặc trước đó)
            var newIndex = currentIndex + direction;

            // Kiểm tra xem chỉ mục mới có nằm trong phạm vi danh sách không
            if (newIndex < 0) {
                // Nếu chỉ mục mới là một số âm, quay lại bài cuối cùng
                newIndex = extractedSongs.length - 1;
            } else if (newIndex >= extractedSongs.length) {
                // Nếu chỉ mục mới vượt quá số lượng bài hát, quay lại bài đầu tiên
                newIndex = 0;
            }

            // Lấy bài hát mới
            var newSong = extractedSongs[newIndex];

            // Chuyển đến bài hát mới
            playAudio(newSong.audioUrl, newSong.songId, newSong.imgUrl, newSong.title);
            currentSongId = newSong.songId; // Cập nhật currentSongId
        } else {
            console.log("Current song not found in the list.");
        }
    }
}
function playRandomSong() {
    if (extractedSongs.length > 0) {
        // Chọn một chỉ mục ngẫu nhiên trong danh sách bài hát
        var randomIndex = Math.floor(Math.random() * extractedSongs.length);

        // Lấy bài hát ngẫu nhiên
        var randomSong = extractedSongs[randomIndex];

        // Chuyển đến bài hát ngẫu nhiên
        playAudio(randomSong.audioUrl, randomSong.songId, randomSong.imgUrl, randomSong.title);
        currentSongId = randomSong.songId; // Cập nhật currentSongId
    } else {
        console.log("No songs in the list.");
    }
}

// Lắng nghe sự kiện click trên menu items
document.getElementById('playlistMenuItem').addEventListener('click', function () {
    displaySongs('playlistSongs'); // Hiển thị danh sách bài hát từ Playlist
});

document.getElementById('lastListeningMenuItem').addEventListener('click', function () {
    displaySongs('lastListeningSongs'); // Hiển thị danh sách bài hát từ Last Listening
});

document.getElementById('recommendedMenuItem').addEventListener('click', function () {
    displaySongs('recommendedSongs'); // Hiển thị danh sách bài hát từ Recommended
});

// Hàm hiển thị danh sách bài hát tương ứng
function displaySongs(listId) {
    // Ẩn tất cả các danh sách bài hát
    document.getElementById('playlistSongs').style.display = 'none';
    document.getElementById('lastListeningSongs').style.display = 'none';
    document.getElementById('recommendedSongs').style.display = 'none';

    // Hiển thị danh sách bài hát được chọn
    document.getElementById(listId).style.display = 'block';
}


// Khởi tạo một danh sách trống để lưu trạng thái danh sách bài hát đã phát
var recentlyPlayedSongs = [];
var maxRecentlyPlayedSongs = 5; // Giới hạn số lượng bài hát đã phát trước đó

// Hàm để thêm một bài hát vào danh sách đã phát trước đó
function addSongToRecentlyPlayed(song) {
    // Lọc bài hát với cùng songId ra khỏi danh sách
    recentlyPlayedSongs = recentlyPlayedSongs.filter(function (item) {
        return item.songId !== song.songId;
    });

    // Thêm bài hát mới vào đầu danh sách
    recentlyPlayedSongs.unshift(song);

    // Giới hạn danh sách chỉ lấy 5 bài gần nhất
    if (recentlyPlayedSongs.length > maxRecentlyPlayedSongs) {
        recentlyPlayedSongs = recentlyPlayedSongs.slice(0, maxRecentlyPlayedSongs);
    }

    // Lưu danh sách vào Local Storage
    localStorage.setItem('recentlyPlayedSongs', JSON.stringify(recentlyPlayedSongs));
    console.log('Danh sách bài hát:', recentlyPlayedSongs);
}

// Lấy đối tượng "lastListeningSongs" theo ID
var lastListeningSongsContainer = document.getElementById("lastListeningSongs");

// Lấy danh sách bài hát đã phát trước đó từ Local Storage (nếu có)
var recentlyPlayedSongs = JSON.parse(localStorage.getItem('recentlyPlayedSongs')) || [];

// Lặp qua danh sách bài hát đã phát và tạo các phần tử HTML
recentlyPlayedSongs.forEach(function (song) {
    // Tạo một phần tử li để chứa thông tin của bài hát
    var songItem = document.createElement("li");
    songItem.className = "songItem";

    // Tạo các phần tử HTML để hiển thị thông tin bài hát (img, title, artist, và các nút)
    var img = document.createElement("img");
    img.src = song.imgUrl;
    img.alt = song.title;

    var h5 = document.createElement("h5");
    h5.textContent = song.title;

    var subtitle = document.createElement("div");
    subtitle.className = "subtitle";
    subtitle.textContent = song.artist;

    var playButton = document.createElement("i");
    playButton.className = "bi playListPlay bi-play-circle-fill";
    playButton.setAttribute("data-song-id", song.songId);
    playButton.onclick = function () {
        // Gọi hàm playAudio để phát bài hát khi người dùng nhấn nút play
        playAudio(song.audioUrl, song.songId, song.imgUrl, song.title);
    };

    // Thêm các phần tử vào phần tử li
    h5.appendChild(subtitle);
    songItem.appendChild(img);
    songItem.appendChild(h5);
    songItem.appendChild(playButton);

    // Thêm phần tử li vào đối tượng "lastListeningSongs"
    lastListeningSongsContainer.appendChild(songItem);
});

// Lấy danh sách tất cả các thẻ h4 trong menu
var menuItems = document.querySelectorAll("header .menu_side .playlist h4");

// Lắng nghe sự kiện click trên từng thẻ h4
menuItems.forEach(function (menuItem) {
    menuItem.addEventListener("click", function () {
        // Loại bỏ lớp 'active' từ tất cả các thẻ h4
        menuItems.forEach(function (item) {
            item.classList.remove("active");
        });

        // Thêm lớp 'active' cho thẻ h4 hiện tại
        menuItem.classList.add("active");
    });
});

function likeSong(username) {
    if (currentSongId != null) {
        $.ajax({
            url: "/Songs/AddFavorite", // Đảm bảo rằng URL đúng
            type: "get",
            data: {
                id: currentSongId
            },
            success: function (result) {
                // Update the page with the new data
                document.getElementById("like").style.display = "block"; // Đã sửa lỗi ở đây (documemt -> document)
                document.getElementById("unlike").style.display = "none"; // Đã sửa lỗi ở đây (documemt -> document)
            }
        });
    }
}

function unlikeSong(username) {
    if (currentSongId != null) {
        $.ajax({
            url: "/Songs/RemoveFavorite", // Đảm bảo rằng URL đúng
            type: "get",
            data: {
                id: currentSongId
            },
            success: function (result) {
                // Update the page with the new data
                document.getElementById("like").style.display = "none"; // Đã sửa lỗi ở đây (documemt -> document)
                document.getElementById("unlike").style.display = "block"; // Đã sửa lỗi ở đây (documemt -> document)
            }
        });
    }
}
