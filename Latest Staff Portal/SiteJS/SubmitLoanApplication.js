


var SubmitLoansApplication = function () {
    var reason = $("#txtReason").val();
    var productType = $("#txtLoanProductType").val();
    var appDate = $("#txtDate").val();
    var amount = $("#txtAmountRequested").val();
    var attachment = $("#attachments").val();

    // Allowed file types
    var allowedFileTypes = ['image/jpeg', 'image/png', 'application/pdf', 'application/msword', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'];

    if (attachment === "") {
        Swal.fire('Warning', "Please Attach Supporting Documents", 'warning');
        return;
    }

    if (reason === "") {
        Swal.fire('Warning', "Input reason for application", 'warning');
        return;
    } else if (amount === "") {
        Swal.fire('Warning', "Enter Amount Requested", 'warning');
        return;
    } else if (appDate === "") {
        Swal.fire('Warning', "Input Date", 'warning');
        return;
    } else {
        var LoanApplicationCard = {
            ApplicationDate: appDate,
            LoanProductType: productType,
            AmountRequested: amount,
            Reason: reason
        };
        var filename, base64String, filetype;
        var files = document.getElementById('attachments').files;
        var file = files[0];

        if (files.length) {
            if (!allowedFileTypes.includes(file.type)) {
                Swal.fire('Warning', 'Invalid file format. Please upload JPEG, PNG, PDF, DOC, or DOCX files only.', 'warning');
                return;
            }
            if (file.size > 10000000) {
                Swal.fire('Warning', 'Please only files less than 10MB allowed. Thanks!!', 'warning');
                return;
            } else {
                var blob = file.slice();
                filetype = file.type;
                filename = file.name;
                var reader = new FileReader();
                reader.onloadend = function (evt) {
                    if (evt.target.readyState === FileReader.DONE) { // DONE == 2
                        var cont = evt.target.result;
                        base64String = getB64Str(cont);

                        ShowProgress();
                        $.ajax({
                            type: "POST",
                            url: "/WelfareManagement/SubmitLoansApplication",
                            data: JSON.stringify({
                                NewApp: LoanApplicationCard,
                                FileName: filename,
                                FileType: filetype,
                                FileContent: base64String
                            }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data.success) {
                                    window.location = data.message;
                                } else {
                                    Swal.fire('Warning', data.message, 'warning');
                                }
                            },
                            error: function (xhr, status, error) {
                                Swal.fire('Error', xhr.responseText, 'error');
                            },
                            complete: function () {
                                HideProgress();
                            }
                        });
                    }
                };
                reader.readAsArrayBuffer(blob);
            }
        }
    }
};



function getB64Str(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}