var modal, loading;

function ShowProgress() {
    // Create the modal loader
    modal = document.createElement("DIV");
    modal.className = "modalLoader";
    document.body.appendChild(modal);

    // Get the loading element and show it
    loading = document.getElementsByClassName("loading")[0];
    if (loading) {
        loading.style.display = "block";
        
        // Center the loading element
        var top = Math.max(window.innerHeight / 2 - loading.offsetHeight / 2, 0);
        var left = Math.max(window.innerWidth / 2 - loading.offsetWidth / 2, 0);
        loading.style.top = top + "px";
        loading.style.left = left + "px";
    }
}

function HideProgress() {
    // Remove modal loader if it exists
    if (modal && modal.parentNode) {
        document.body.removeChild(modal);
        modal = null; // Clear reference after removal
    }

    // Hide the loading element
    if (loading) {
        loading.style.display = "none";
    }

    // Also hide any other loader if present
    $("#divLoader").hide();
}
