function initMap() {
    var map = new BMap.Map("map");

    // 设置地图中心点（天安门）
    var centerPoint = new BMap.Point(116.404, 39.915);
    map.centerAndZoom(centerPoint, 4);

    map.addControl(new BMap.NavigationControl());

    // 自定义图标
    var myIcon = new BMap.Icon("https://img.icons8.com/emoji/48/round-pushpin-emoji.png",
        new BMap.Size(32, 32),
        { anchor: new BMap.Size(16, 32) }
    );

    // 需要标记的地点
    var locations = [
        { lng: 116.404, lat: 39.915, name: "北京天安门", count: 100, seriesNumber: "T-001" },
        { lng: -77.0365, lat: 38.8977, name: "美国白宫", count: 50, seriesNumber: "W-002" },
        { lng: 2.3364, lat: 48.8606, name: "法国卢浮宫", count: 75, seriesNumber: "L-003" }
    ];

    locations.forEach(function (location) {
        var point = new BMap.Point(location.lng, location.lat);
        var marker = new BMap.Marker(point, { icon: myIcon });
        map.addOverlay(marker);

        // 在标记点旁边显示信息
        var label = new BMap.Label(
            `${location.name}<br>电量: ${location.count}%<br>编号: ${location.seriesNumber}`,
            { position: point, offset: new BMap.Size(20, -20) }
        );
        label.setStyle({
            color: "black",
            fontSize: "12px",
            backgroundColor: "rgba(255,255,255,0.7)",
            border: "1px solid gray",
            padding: "5px",
            borderRadius: "5px",
            whiteSpace: "pre-line", // 允许换行
            width: "100px"
        });
        map.addOverlay(label);
    });
}

window.onload = initMap;
