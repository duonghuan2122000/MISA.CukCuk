<template>
    <div class="navbar">
        <div class="navbar-content">
            <div class="navbar-item">
                <div class="navbar-item-icon">
                    <i class="fas fa-chart-pie"></i>
                </div>
                <div class="navbar-item-text">
                    Tổng quan
                </div>
            </div>
            <div class="navbar-item">
                <div class="navbar-item-icon">
                    <i class="fas fa-chart-bar"></i>
                </div>
                <div class="navbar-item-text">
                    Báo cáo
                </div>
            </div>
            <div class="navbar-item active">
                <div class="navbar-item-icon">
                    <i class="fas fa-users"></i>
                </div>
                <div class="navbar-item-text">
                    Danh sách khách hàng
                </div>
            </div>
        </div>
    </div>
</template>

<style>
.navbar {
    height: calc(100vh - 56px);
    width: 226px;
    border-right: 1px solid #e5e5e5;
}

.navbar .navbar-item {
    height: 40px;
    display: flex;
    align-items: center;
    cursor: pointer;
}

.navbar .navbar-item-icon {
    margin-left: 16px;
    margin-right: 16px;
}

.navbar .navbar-item-icon * {
    font-size: 16px;
}

/* Navbar item active */
.navbar .navbar-item.active {
    background-color: green;
}

.navbar .navbar-item.active .navbar-item-icon *,
.navbar .navbar-item.active .navbar-item-text {
    color: #fff;
}

/* Navbar item focus */
.navbar .navbar-item:not(.active):hover {
    background-color: #ccc;
}
</style>