// QR Field Monitoring System - Professional UI Navigation Interaction
document.addEventListener("DOMContentLoaded", function () {
    const sidebarToggle = document.getElementById("sidebarToggle");
    const appWrapper = document.getElementById("app-wrapper");
    const backdrop = document.getElementById("sidebarBackdrop");

    if (sidebarToggle && appWrapper) {
        sidebarToggle.addEventListener("click", function () {
            appWrapper.classList.toggle("sidebar-open");
            appWrapper.classList.toggle("sidebar-collapsed");
        });
    }

    // Dismiss sidebar when clicking outside on mobile devices
    if (backdrop && appWrapper) {
        backdrop.addEventListener("click", function () {
            appWrapper.classList.remove("sidebar-open");
        });
    }
});
