function calculateWeightedScore(jointAgreedTargetId, targetId, weightId, outputId) {
    var jointAgreedTarget = parseFloat($(jointAgreedTargetId).val()) || 0;
    var target = parseFloat($(targetId).val()) || 0;
    var weight = parseFloat($(weightId).val()) || 0;

    if (target > 0) {
        var weightedScore = (jointAgreedTarget / target) * weight;
        $(outputId).val(weightedScore.toFixed(2));
    } else {
        $(outputId).val("");
    }
}



function showModalWithPartialView(url, modalId, bodyId, heading) {
    ShowProgress();
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {

            $(bodyId).html(data);
            $(modalId).modal('show');
            /*add header*/
            $(modalId).find('.modal-title').html(heading);
            HideProgress();
        },
        error: function (xhr, status, error) {
            console.error("Error loading partial view:", status, error);
            Swal.fire("Failed to load the content. Please try again later.");
        }
    });
}
